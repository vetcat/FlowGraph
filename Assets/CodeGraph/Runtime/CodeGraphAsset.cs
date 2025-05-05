using System.Collections.Generic;
using UnityEngine;

namespace CodeGraph.Runtime
{
    [CreateAssetMenu(menuName = "Code Graph/New Graph")]
    public class CodeGraphAsset : ScriptableObject
    {
        [SerializeReference]
        private List<CodeGraphNode> m_nodes;
    }
}
