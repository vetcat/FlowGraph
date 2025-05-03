using UnityEditor;
using UnityEngine;

namespace CodeGraph.Editor
{
    public class CodeGraphEditorWindow : EditorWindow
    {
        public CodeGraphAsset currentGraph => m_currentGraph;
        [SerializeField] private CodeGraphAsset m_currentGraph;

        [SerializeField] private CodeGraphView m_currentView;

        [SerializeField] private SerializedObject m_serializedObject;

        public static void Open(CodeGraphAsset target)
        {
            var windows = Resources.FindObjectsOfTypeAll<CodeGraphEditorWindow>();
            foreach (var w in windows)
            {
                if (w.currentGraph == target)
                {
                    w.Focus();
                    return;
                }
            }

            var window = CreateWindow<CodeGraphEditorWindow>(typeof(CodeGraphEditorWindow), typeof(SceneView));
            window.titleContent = new GUIContent($"{target.name}",
                EditorGUIUtility.ObjectContent(null, typeof(CodeGraphAsset)).image);
            window.Load(target);
        }

        public void Load(CodeGraphAsset target)
        {
            m_currentGraph = target;
            DrawGraph();
        }

        private void DrawGraph()
        {
            m_serializedObject = new SerializedObject(m_currentGraph);
            m_currentView = new CodeGraphView(m_serializedObject);
            rootVisualElement.Add(m_currentView);
        }
    }
}