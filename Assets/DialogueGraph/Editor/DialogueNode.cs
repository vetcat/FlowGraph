using UnityEditor.Experimental.GraphView;

namespace DialogueGraph.Editor
{
    public class DialogueNode : Node
    {
        public string GUID;
        public string DialogueText;
        public bool EntryPoint = false;
    }
}