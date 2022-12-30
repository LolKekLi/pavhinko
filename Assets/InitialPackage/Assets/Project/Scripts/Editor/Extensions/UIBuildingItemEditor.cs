using UnityEditor;

namespace Project.Editor
{
    [CustomEditor(typeof(UIBuildingItem))]
    public class UIBuildingItemEditor : UnityEditor.UI.ButtonEditor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            
            SerializedObject so = new SerializedObject(target);
            
            SerializedProperty stringsProperty = so.FindProperty("_buildType");
            EditorGUILayout.PropertyField(stringsProperty, true);

            so.ApplyModifiedProperties(); 
        }
    }
}