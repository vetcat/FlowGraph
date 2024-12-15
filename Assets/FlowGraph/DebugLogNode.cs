using UnityEngine;

namespace FlowGraph
{
    public class DebugLogNode : ActionNode
    {
        public string message;
        protected override void OnStart()
        {
            //Debug.Log($"{GetType().Name} OnStart");
        }

        protected override void OnStop()
        {
            //Debug.Log($"{GetType().Name} OnStop");
        }

        protected override ENodeState OnUpdate()
        {
            //Debug.Log($"{GetType().Name} OnUpdate message = {message}");
            Debug.Log(message);
            return ENodeState.Success;
        }
    }
}