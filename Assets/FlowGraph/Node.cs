using UnityEngine;

namespace FlowGraph
{
    public abstract class Node : ScriptableObject
    {
        [HideInInspector] public ENodeState state = ENodeState.Running;
        [HideInInspector] public bool started;
        public string guid;
        [HideInInspector] public Vector2 position;
        
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
