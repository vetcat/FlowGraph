using UnityEngine;

namespace FlowGraph
{
    public class WaitNode : ActionNode
    {
        public float duration = 1;
        private float startTime;
        
        protected override void OnStart()
        {
            startTime = Time.time;
        }

        protected override void OnStop()
        {
           
        }

        protected override ENodeState OnUpdate()
        {
            var currentTime = Time.time - startTime;
            return currentTime > duration ? ENodeState.Success : ENodeState.Running;
        }
    }
}