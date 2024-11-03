using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor.Experimental.GraphView;

namespace FlowGraph.Editor
{
    public class BehaviorTreeView : GraphView
    {
        public new class UxmlFactory : UxmlFactory<BehaviorTreeView, UxmlTraits>
        {
        }

        private BehaviorTree _tree;

        public BehaviorTreeView()
        {
            Insert(0, new GridBackground());

            this.AddManipulator(new ContentZoomer());
            this.AddManipulator(new ContentDragger());
            this.AddManipulator(new SelectionDragger());
            this.AddManipulator(new RectangleSelector());

            var styleSheet =
                AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/FlowGraph/Editor/BehaviorTreeEditor.uss");
            styleSheets.Add(styleSheet);
        }

        public void PopulateView(BehaviorTree tree)
        {
            _tree = tree;
            graphViewChanged -= OnGraphViewChanged;
            DeleteElements(graphElements);
            graphViewChanged += OnGraphViewChanged;
            _tree.nodes.ForEach(CreateNodeView);
            _tree.nodes.ForEach(CreateEdges);
        }

        public override List<Port> GetCompatiblePorts(Port startPort, NodeAdapter nodeAdapter)
        {
            return ports.ToList().Where(endPort => endPort.direction != startPort.direction && endPort.node != startPort.node).ToList();
        }

        private GraphViewChange OnGraphViewChanged(GraphViewChange graphViewChange)
        {
            graphViewChange.elementsToRemove?.ForEach(element =>
            {
                if (element is NodeView nodeView)
                {
                    _tree.DeleteNode(nodeView.node);
                }

                if (element is Edge edge)
                {
                    NodeView parentView = edge.output.node as NodeView;
                    NodeView childView = edge.input.node as NodeView;
                    _tree.RemoveChild(parentView.node, childView.node);
                }
            });

            graphViewChange.edgesToCreate?.ForEach(edge =>
            {
                NodeView parentView = edge.output.node as NodeView;
                NodeView childView = edge.input.node as NodeView;
                _tree.AddChild(parentView.node, childView.node);
                
            });
            
            return graphViewChange;
        }

        public override void BuildContextualMenu(ContextualMenuPopulateEvent evt)
        {
            var types = TypeCache.GetTypesDerivedFrom<Node>();
            foreach (var type in types)
            {
                if (type.BaseType?.Name == nameof(Node))
                    continue;
                evt.menu.AppendAction($"[{type.BaseType.Name}] {type.Name}", a => CreateNode(type));
            }
            
            base.BuildContextualMenu(evt);
        }

        private void CreateNode(Type type)
        {
            var node = _tree.CreateNode(type);
            CreateNodeView(node);
        }

        private void CreateNodeView(Node node)
        {
            var nodeView = new NodeView(node);
            AddElement(nodeView);
        }
        
        private void CreateEdges(Node node)
        {
            var children = _tree.GetChildren(node);
            children.ForEach(c =>
            {
                NodeView parentView = FindNodeView(node);
                NodeView childView = FindNodeView(c);

                var edge = parentView.output.ConnectTo(childView.input);
                AddElement(edge);
            });
        }

        private NodeView FindNodeView(Node node)
        {
            return GetNodeByGuid(node.guid) as NodeView;
        }
    }
}