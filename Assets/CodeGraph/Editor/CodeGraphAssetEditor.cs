using UnityEditor;
using UnityEngine;

namespace CodeGraph.Editor
{
    [CustomEditor(typeof(CodeGraphAsset))]
    public class CodeGraphAssetEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            if (GUILayout.Button("Edit"))
            {
                CodeGraphEditorWindow.Open((CodeGraphAsset)target);
            }
        }
    }
}