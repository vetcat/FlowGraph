namespace FlowGraph.Editor
{
    public class NodeView : UnityEditor.Experimental.GraphView.Node
    {
        public Node Node;
        
        public NodeView(Node node)
        {
            Node = node;
            title = node.name;
        }
    }
}
