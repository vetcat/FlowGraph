using System;

namespace CodeGraph.Runtime.Attributes
{
    public class NodeInfoAttribute : Attribute
    {
        private string m_nodeTitle;
        private string m_menuItem;

        public string title => m_nodeTitle;
        public string menuItem => m_menuItem;

        public NodeInfoAttribute(string mNodeTitle, string mMenuItem = "")
        {
            m_nodeTitle = mNodeTitle;
            m_menuItem = mMenuItem;
        }
    }
}