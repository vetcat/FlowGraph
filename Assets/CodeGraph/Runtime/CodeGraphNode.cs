using System;
using UnityEngine;

namespace CodeGraph.Runtime
{
    [Serializable]
    public class CodeGraphNode
    {
        [SerializeField]
        private string m_guid;

        [SerializeField] 
        private Rect m_position;

        public string typeName;
        public string id => m_guid;
        public Rect position => m_position;

        public CodeGraphNode()
        {
            NewGuid();
        }

        private void NewGuid()
        {
            m_guid = Guid.NewGuid().ToString();
        }

        public void SetPosition(Rect pos)
        {
            m_position = pos;
        }
    }
}
