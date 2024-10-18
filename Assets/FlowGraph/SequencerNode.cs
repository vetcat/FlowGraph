using System;

namespace FlowGraph
{
    public class SequencerNode : CompositeNode
    {
        private int current;
        protected override void OnStart()
        {
            current = 0;
        }

        protected override void OnStop()
        {
           
        }

        protected override ENodeState OnUpdate()
        {
            var child = children[current];
            switch (child.Update())
            {
                case ENodeState.Running:
                    return ENodeState.Running;
                case ENodeState.Success:
                    current++;
                    break;
                case ENodeState.Failure:
                    return ENodeState.Failure;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return current == children.Count ? ENodeState.Success : ENodeState.Running;
        }
    }
}