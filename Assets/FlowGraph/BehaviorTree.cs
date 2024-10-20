using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace FlowGraph
{
    [CreateAssetMenu(fileName = "BehaviorTree", menuName = "FlowGraph/BehaviorTree")]
    public class BehaviorTree : ScriptableObject
    {
        public Node rootNode;
        public ENodeState state = ENodeState.Running;

        public List<Node> nodes = new();

        public ENodeState Update()
        {
            if (rootNode.state == ENodeState.Running)
            {
                state = rootNode.Update();
            }
            
            return state;
        }

        public Node CreateNode(System.Type type)
        {
            var node = CreateInstance(type) as Node;
            node.name = type.Name;
            node.guid = GUID.Generate().ToString();
            nodes.Add(node);
            
            AssetDatabase.AddObjectToAsset(node, this);
            AssetDatabase.SaveAssets();
            
            return node;
        }

        public void DeleteNode(Node node)
        {
            nodes.Remove(node);
            AssetDatabase.RemoveObjectFromAsset(node);
            AssetDatabase.SaveAssets();
        }
    }
}