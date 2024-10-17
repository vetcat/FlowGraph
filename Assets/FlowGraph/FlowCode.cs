using System.Collections.Generic;
using UnityEngine;

namespace FlowGraph
{
    //[CreateAssetMenu("FlowCode", "FlowGraph/FlowCode")]
    public class FlowCode : ScriptableObject
    {
        public List<Node> Nodes => nodes;
        [SerializeField]
        private List<Node> nodes = new();

        public void AddNode(Node node)
        {
            nodes.Add(node);
        }

        public void Update()
        {
            if (nodes == null)
                return;
            
            foreach (var node in nodes)
            {
                node.Update();
            }
        }
    }
}