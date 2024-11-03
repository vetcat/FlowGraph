namespace FlowGraph
{
    public class RootNode : Node
    {
        public Node child;
        protected override void OnStart()
        {
        }

        protected override void OnStop()
        {
        }

        protected override ENodeState OnUpdate()
        {
            return child.Update();
        }
    }
}