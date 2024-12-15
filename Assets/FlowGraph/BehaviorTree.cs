using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace FlowGraph
{
    [CreateAssetMenu(fileName = "BehaviorTree", menuName = "FlowGraph/BehaviorTree")]
    public class BehaviorTree : ScriptableObject
    {
        //[HideInInspector] 
        public Node rootNode;
        //[HideInInspector] 
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

        public Node CreateNode(Type type)
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

        public void AddChild(Node parent, Node child)
        {
            //todo : need work with attribute later
            if (parent is DecoratorNode decoratorNode)
            {
                decoratorNode.child = child;
            }

            if (parent is CompositeNode compositeNode)
            {
                compositeNode.children.Add(child);
            }

            if (parent is RootNode root)
            {
                root.child = child;
            }
        }
        
        public void RemoveChild(Node parent, Node child)
        {
            //todo : need work with attribute later
            if (parent is DecoratorNode decoratorNode)
            {
                decoratorNode.child = null;
            }

            if (parent is CompositeNode compositeNode)
            {
                compositeNode.children.Remove(child);
            }
            
            if (parent is RootNode root)
            {
                root.child = null;
            }
        }

        public List<Node> GetChildren(Node parent)
        {
            //todo : need work with attribute later
            var children = new List<Node>();
            
            if (parent is DecoratorNode decoratorNode && decoratorNode.child != null)
            {
                children.Add(decoratorNode.child);
            }
            
            if (parent is RootNode root && root.child != null)
            {
                children.Add(root.child);
            }

            if (parent is CompositeNode compositeNode && compositeNode.children != null)
            {
                return compositeNode.children;
            }
            
            return children;
        }

        public BehaviorTree Clone()
        {
            BehaviorTree tree = Instantiate(this);
            tree.rootNode = tree.rootNode.Clone();
            return tree;
        }
    }
}