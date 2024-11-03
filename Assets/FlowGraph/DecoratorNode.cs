using UnityEngine;

namespace FlowGraph
{
    public abstract class DecoratorNode : Node
    {
        [HideInInspector] public Node child;
    }
}