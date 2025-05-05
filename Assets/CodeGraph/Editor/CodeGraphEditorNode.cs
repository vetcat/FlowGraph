using UnityEditor.Experimental.GraphView;

namespace CodeGraph.Editor
{
    public class CodeGraphEditorNode : Node
    {
        public CodeGraphEditorNode()
        {
            AddToClassList("code-graph-node");
        }
    }
}