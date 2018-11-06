using UnityEngine;
using UnityEditor;

namespace UModules.Editor
{
    /// <summary>
    /// Property drawer to display a value but disable editing
    /// </summary>
    /// <module>UM_Basic</module>
    [CustomPropertyDrawer(typeof(ReadonlyAttribute))]
    public class ReadonlyAttributeDrawer : PropertyDrawer
    {
        /// <summary>
        /// Draw the property
        /// </summary>
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginDisabledGroup(true);
            EditorGUI.PropertyField(position, property, label, true);
            EditorGUI.EndDisabledGroup();
        }

        /// <summary>
        /// Get the full property height
        /// </summary>
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorGUI.GetPropertyHeight(property, label, true);
        }
    }
}
