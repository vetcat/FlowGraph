using System.Collections.Generic;

namespace FlowGraph
{
    public abstract class CompositeNode : Node
    {
        public List<Node> children = new();
    }
}