using System.Collections.Generic;
using UnityEngine;

namespace FlowGraph
{
    public abstract class CompositeNode : Node
    {
        [HideInInspector] public List<Node> children = new();
    }
}