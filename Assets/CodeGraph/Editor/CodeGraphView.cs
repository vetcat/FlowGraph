using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace CodeGraph.Editor
{
    public class CodeGraphView : GraphView
    {
        private SerializedObject m_serializedObject;
        public CodeGraphView(SerializedObject serializedObject)
        {
            m_serializedObject = serializedObject;
            
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
    }
}