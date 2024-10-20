using UnityEngine;

namespace FlowGraph
{
    public abstract class Node : ScriptableObject
    {
        public ENodeState state = ENodeState.Running;
        public bool started;
        public string guid;
        
        public ENodeState Update()
        {
            if (!started)
            {
                started = true;
                OnStart();
            }

            state = OnUpdate();

            if (state == ENodeState.Failure || state == ENodeState.Success)
            {
                started = false;
                OnStop();
            }
            return state;
        }

        protected abstract void OnStart();
        protected abstract void OnStop();
        protected abstract ENodeState OnUpdate();
    }
}
