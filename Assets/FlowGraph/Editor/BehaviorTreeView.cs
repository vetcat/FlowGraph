using System;
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
        }

        private GraphViewChange OnGraphViewChanged(GraphViewChange graphViewChange)
        {
            graphViewChange.elementsToRemove?.ForEach(e =>
            {
                if (e is NodeView nodeView)
                {
                    _tree.DeleteNode(nodeView.Node);
                }
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
            Debug.LogError($"{GetType().Name} CreateNode type {type.Name}");
            var node = _tree.CreateNode(type);
            CreateNodeView(node);
        }

        private void CreateNodeView(Node node)
        {
            var nodeView = new NodeView(node);
            AddElement(nodeView);
        }
    }
}