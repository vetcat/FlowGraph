using UnityEngine;

namespace FlowGraph
{
    //[CreateAssetMenu("FlowCode", "FlowGraph/FlowCode")]
    public class BehaviorTree : ScriptableObject
    {
        public Node rootNode;
        public ENodeState state = ENodeState.Running;

        public ENodeState Update()
        {
            if (rootNode.state == ENodeState.Running)
            {
                state = rootNode.Update();
            }
            
            return state;
        }
    }
}