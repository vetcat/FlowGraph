using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace FlowGraph.Editor
{
    public class BehaviorTreeEditor : EditorWindow
    {
        [SerializeField]
        private VisualTreeAsset m_VisualTreeAsset = default;

        private BehaviorTreeView _treeView;
        private InspectorView _inspectorView;

        [MenuItem("FlowGraph/BehaviorTreeEditor")]
        public static void OpenWindow()
        {
            BehaviorTreeEditor wnd = GetWindow<BehaviorTreeEditor>();
            wnd.titleContent = new GUIContent("BehaviorTreeEditor");
        }

        public void CreateGUI()
        {
            // Each editor window contains a root VisualElement object
            VisualElement root = rootVisualElement;
        
            var visualTree =
                AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/FlowGraph/Editor/BehaviorTreeEditor.uxml");
            visualTree.CloneTree(root);
        
            var styleSheet =
                AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/FlowGraph/Editor/BehaviorTreeEditor.uss");
            root.styleSheets.Add(styleSheet);

            _treeView = root.Q<BehaviorTreeView>();
            _inspectorView = root.Q<InspectorView>();
        }

        private void OnSelectionChange()
        {
            var tree = Selection.activeObject as BehaviorTree;
            if (tree)
            {
                Debug.LogError($"{GetType().Name} OnSelectionChange select BehaviorTree");
                _treeView.PopulateView(tree);
            }
        }
    }
}
