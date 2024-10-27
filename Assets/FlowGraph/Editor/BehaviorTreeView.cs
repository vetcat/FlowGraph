using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor.Experimental.GraphView;

namespace FlowGraph.Editor
{
    public class BehaviorTreeView : GraphView
    {
        public new class UxmlFactory : UxmlFactory<BehaviorTreeView, UxmlTraits> { }

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
            DeleteElements(graphElements);
            _tree.nodes.ForEach(CreateNodeView);
        }

        private void CreateNodeView(Node node)
        {
            var nodeView = new NodeView(node);
            AddElement(nodeView);
        }
    }
}