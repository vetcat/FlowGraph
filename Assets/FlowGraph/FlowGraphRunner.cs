using UnityEngine;

namespace FlowGraph
{
    public class FlowGraphRunner : MonoBehaviour
    {
        [SerializeField] private BehaviorTree behaviorTree;

        //todo test
        public void Start()
        {
            if (behaviorTree != null)
            {
                behaviorTree = behaviorTree.Clone();
                return;
            }

            Debug.LogError("FlowGraphRunner behaviorTree is NULL - create test tree");
            CreateTest1();
        }
        
        public void Update()
        {
            behaviorTree.Update();
        }
        
        private void CreateTest1()
        {
            behaviorTree = ScriptableObject.CreateInstance<BehaviorTree>();
            var logNode = ScriptableObject.CreateInstance<DebugLogNode>();
            logNode.message = "Hello world";
        
            behaviorTree.rootNode = logNode;
        }

        private void CreateTest2()
        {
            behaviorTree = ScriptableObject.CreateInstance<BehaviorTree>();
            var logNode = ScriptableObject.CreateInstance<DebugLogNode>();
            logNode.message = "Hello world";
            var repeatNode = ScriptableObject.CreateInstance<RepeatNode>();
            repeatNode.child = logNode;
        
            behaviorTree.rootNode = repeatNode;
        }
        
        private void CreateTest3()
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
    }
}