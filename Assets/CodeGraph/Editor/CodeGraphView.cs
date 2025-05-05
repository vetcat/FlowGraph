using CodeGraph.Runtime;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace CodeGraph.Editor
{
    public class CodeGraphView : GraphView
    {
        private SerializedObject m_serializedObject;
        private CodeGraphEditorWindow m_window;
        public CodeGraphEditorWindow window => m_window;
        
        public CodeGraphView(SerializedObject serializedObject, CodeGraphEditorWindow win)
        {
            m_serializedObject = serializedObject;
            m_window = win;
            
            var backGround = new GridBackground();
            backGround.name = "Grid";
            Add(backGround);
            
            this.AddManipulator(new ContentDragger());
            this.AddManipulator(new SelectionDragger());
            this.AddManipulator(new RectangleSelector());
            this.AddManipulator(new ClickSelector());
            
            var styleSheet =
                AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/CodeGraph/Editor/USS/CodeGraphEditor.uss");
            styleSheets.Add(styleSheet);
        }

        public void Add(CodeGraphNode node)
        {
            Debug.LogError($"[CodeGraphView] Add node");
        }
    }
}