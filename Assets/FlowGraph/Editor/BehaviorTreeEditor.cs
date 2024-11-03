using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace FlowGraph.Editor
{
    public class BehaviorTreeEditor : EditorWindow
    {
        [SerializeField]
        private VisualTreeAsset m_VisualTreeAsset = default;

        private BehaviorTreeView treeView;
        private InspectorView inspectorView;

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

            treeView = root.Q<BehaviorTreeView>();
            treeView.OnNodeSelected += OnNodeViewSelectedChange;
            
            inspectorView = root.Q<InspectorView>();

            treeView.focusable = true;
            treeView.StretchToParentSize();

            OnSelectionChange();
            
        }

        private void OnSelectionChange()
        {
            var tree = Selection.activeObject as BehaviorTree;
            if (tree && AssetDatabase.CanOpenAssetInEditor(tree.GetInstanceID()))
            {
                treeView.PopulateView(tree);
            }
        }

        private void OnNodeViewSelectedChange(NodeView nodeView)
        {
            inspectorView.UpdateSelection(nodeView);
        }
    }
}
