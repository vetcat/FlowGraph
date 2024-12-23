using System;
using UnityEngine;
using UnityEditor.Experimental.GraphView;

namespace FlowGraph.Editor
{
    public class NodeView : UnityEditor.Experimental.GraphView.Node
    {
        public Action<NodeView> OnNodeSelected;
        
        public Node node;
        public Port input;
        public Port output;

        public NodeView(Node node) : base("Assets/FlowGraph/Editor/NodeView.uxml")
        {
            this.node = node;
            title = node.name;
            viewDataKey = node.guid;
            style.left = node.position.x;
            style.top = node.position.y;

            CreateInputPorts();
            CreateOutputPorts();
        }

        private void CreateOutputPorts()
        {
            //todo need work from attribute
            if (node is ActionNode)
            {
              
            }
            else if (node is CompositeNode)
            {
                output = InstantiatePort(Orientation.Vertical, Direction.Output, Port.Capacity.Multi, typeof(bool));
            }
            else if (node is DecoratorNode)
            {
                output = InstantiatePort(Orientation.Vertical, Direction.Output, Port.Capacity.Single, typeof(bool));
            }
            else if (node is RootNode)
            {
                output = InstantiatePort(Orientation.Vertical, Direction.Output, Port.Capacity.Single, typeof(bool));
            }

            if (output != null)
            {
                output.name = "";
                outputContainer.Add(output);
            }
        }

        private void CreateInputPorts()
        {
            //todo need work from attribute
            if (node is ActionNode)
            {
                input = InstantiatePort(Orientation.Vertical, Direction.Input, Port.Capacity.Single, typeof(bool));
            }
            else if (node is CompositeNode)
            {
                input = InstantiatePort(Orientation.Vertical, Direction.Input, Port.Capacity.Single, typeof(bool));
            }
            else if (node is DecoratorNode)
            {
                input = InstantiatePort(Orientation.Vertical, Direction.Input, Port.Capacity.Single, typeof(bool));
            }
            else if (node is RootNode)
            {
                
            }

            if (input != null)
            {
                input.name = "";
                inputContainer.Add(input);
            }
        }

        public override void SetPosition(Rect newPos)
        {
            base.SetPosition(newPos);
            node.position.x = newPos.xMin;
            node.position.y = newPos.yMin;
        }

        public override void OnSelected()
        {
            base.OnSelected();
            OnNodeSelected?.Invoke(this);
        }
    }
}