namespace FlowGraph
{
    public class RepeatNode : DecoratorNode
    {
        protected override void OnStart()
        {
            
        }

        protected override void OnStop()
        {
           
        }

        protected override ENodeState OnUpdate()
        {
            child.Update();
            return ENodeState.Running;
        }
    }
}
