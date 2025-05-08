using System;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace DialogueGraph.Editor
{
    public class DialogueGraphView : GraphView
    {
        private readonly Vector2 _defaultNodeSize = new Vector2(150, 200);
        public DialogueGraphView()
        {
            this.AddManipulator(new ContentDragger());
            this.AddManipulator(new SelectionDragger());
            this.AddManipulator(new RectangleSelector());

            AddElement(GenerateEntryPointNode());
        }

        private Port GeneratePort(DialogueNode node, Direction portDirection,
            Port.Capacity capacity = Port.Capacity.Single)
        {
            return node.InstantiatePort(Orientation.Horizontal, portDirection, capacity, typeof(float));
        }

        private DialogueNode GenerateEntryPointNode()
        {
            var node = new DialogueNode()
            {
                title = "Start",
                GUID = Guid.NewGuid().ToString(),
                DialogueText = "ENTRYPOINT",
                EntryPoint = true
            };

            var generatedPort = GeneratePort(node, Direction.Output);
            generatedPort.portName = "Next";
            node.outputContainer.Add(generatedPort);
            
            node.RefreshExpandedState();
            node.RefreshPorts();
            
            node.SetPosition(new Rect(100, 200, 100, 150));

            return node;
        }

        public void CreateNode(string nodeName)
        {
            AddElement(CreateDialogueNode(nodeName));
        }

        public DialogueNode CreateDialogueNode(string nodeName)
        {
            var node = new DialogueNode()
            {
                title = nodeName,
                DialogueText = nodeName,
                GUID = Guid.NewGuid().ToString(),
            };

            var inputPort = GeneratePort(node, Direction.Input, Port.Capacity.Multi);
            inputPort.portName = "Input";
            node.inputContainer.Add(inputPort);
                
            node.RefreshExpandedState();
            node.RefreshPorts();
            node.SetPosition(new Rect(Vector2.zero, _defaultNodeSize));

            return node;
        }
    }
}