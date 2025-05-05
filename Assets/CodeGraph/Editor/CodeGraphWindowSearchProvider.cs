using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using CodeGraph.Runtime;
using CodeGraph.Runtime.Attributes;
using CodeGraph.Runtime.Types;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace CodeGraph.Editor
{
    public struct SearchContextElement
    {
        public object target { get; private set; }
        public string title { get; private set; }

        public SearchContextElement(object target, string title)
        {
            this.target = target;
            this.title = title;
        }
    }
    public class CodeGraphWindowSearchProvider : ScriptableObject, ISearchWindowProvider
    {
        public CodeGraphView graph;
        public VisualElement target;

        public static List<SearchContextElement> elements;
        
        public List<SearchTreeEntry> CreateSearchTree(SearchWindowContext context)
        {
            var tree = new List<SearchTreeEntry>();
            tree.Add(new SearchTreeGroupEntry(new GUIContent("Nodes"), 0));
            elements = new List<SearchContextElement>();
            Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();

            foreach (var assembly in assemblies)
            {
                foreach (var type in assembly.GetTypes())
                {
                    if (type.CustomAttributes.ToList() != null)
                    {
                        var attribute = type.GetCustomAttribute(typeof(NodeInfoAttribute));
                        if (attribute != null)
                        {
                            NodeInfoAttribute att = (NodeInfoAttribute)attribute;
                            object node = Activator.CreateInstance(type);
                            if (string.IsNullOrEmpty(att.menuItem)) continue;
                            elements.Add(new SearchContextElement(node, att.menuItem));
                        }
                    }
                }
            }

            var groups = new List<string>();

            foreach (var element in elements)
            {
                var entryTitle = element.title.Split("/");
                string groupName = "";

                for (var i = 0; i < entryTitle.Length; i++)
                {
                    groupName += entryTitle[i];
                    if (!groups.Contains(groupName))
                    {
                        tree.Add(new SearchTreeGroupEntry(new GUIContent(entryTitle[i]), i+1));
                        groups.Add(groupName);
                    }

                    groupName += "/";
                }

                SearchTreeEntry entry = new SearchTreeEntry(new GUIContent(entryTitle.Last()));
                entry.level = entryTitle.Length;
                entry.userData = new SearchContextElement(element.target, element.title);
                tree.Add(entry);
            }

            return tree;
        }

        public bool OnSelectEntry(SearchTreeEntry searchTreeEntry, SearchWindowContext context)
        {
            var windowMousePosition =
                graph.ChangeCoordinatesTo(graph, context.screenMousePosition - graph.window.position.position);

            var graphMousePosition = graph.contentViewContainer.WorldToLocal(windowMousePosition);

            SearchContextElement element = (SearchContextElement)searchTreeEntry.userData;
            CodeGraphNode node = (CodeGraphNode)element.target;
            
            node.SetPosition(new Rect(graphMousePosition, new Vector2()));
            graph.Add(node);
            return true;
        }
    }
}