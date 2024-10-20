using UnityEngine;

namespace FlowGraph
{
    public class FlowGraphRunner : MonoBehaviour
    {
        [SerializeField] private BehaviorTree behaviorTree;

        public void Start()
        {
            behaviorTree = ScriptableObject.CreateInstance<BehaviorTree>();

            var logNode1 = ScriptableObject.CreateInstance<DebugLogNode>();
            logNode1.message = "logNode.message 111";

            var logNode2 = ScriptableObject.CreateInstance<DebugLogNode>();
            logNode2.message = "logNode.message 222";

            var logNode3 = ScriptableObject.CreateInstance<DebugLogNode>();
            logNode3.message = "logNode.message 333";
            
            var waitNode = ScriptableObject.CreateInstance<WaitNode>();

            var sequencerNode = ScriptableObject.CreateInstance<SequencerNode>();
            sequencerNode.children.Add(logNode1);
            sequencerNode.children.Add(waitNode);
            sequencerNode.children.Add(logNode2);
            sequencerNode.children.Add(logNode3);

            var repeatNode = ScriptableObject.CreateInstance<RepeatNode>();
            repeatNode.child = sequencerNode;

            behaviorTree.rootNode = repeatNode;
        }

        public void Update()
        {
            behaviorTree.Update();
        }
    }
}