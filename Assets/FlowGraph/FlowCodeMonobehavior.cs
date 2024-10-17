using UnityEngine;

namespace FlowGraph
{
    public class FlowCodeMonoBehaviour : MonoBehaviour
    {
        [SerializeField]
        private FlowCode flowCode;

        public void Start()
        {
            flowCode = ScriptableObject.CreateInstance<FlowCode>();
            var logNode = ScriptableObject.CreateInstance<DebugLogNode>();
            logNode.message = "logNode.message";
            
            Debug.Log($"{GetType().Name} Start flowCode.Nodes is NULL = {flowCode.Nodes == null}");
            flowCode.AddNode(logNode);
        }

        public void Update()
        {
            flowCode.Update();
        }
    }
}