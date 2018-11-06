using UnityEngine;
using UnityEditor;

namespace UModules.Editor
{
    /// <summary>
    /// Property drawer to display a property based on the value of another
    /// </summary>
    /// <module>UM_Basic</module>
    [CustomPropertyDrawer(typeof(DontShowIfAttribute))]
    public class DontShowIfAttributeDrawer : PropertyDrawer
    {
        /// <summary>
        /// Check if the target property is false. 
        /// Defaults to true if property does not exist.
        /// </summary>
        /// <param name="target">The serialized object to check on</param>
        /// <returns>The negated value of the target property if it exists, true otherwise</returns>
        private bool CheckTargetProperty(SerializedObject target)
        {
            DontShowIfAttribute show = attribute as DontShowIfAttribute;
            SerializedProperty checkProperty = target.FindProperty(show.propertyName);
            return checkProperty == null || !checkProperty.boolValue;
        }

        /// <summary>
        /// Draw the property if the reference value is false
        /// </summary>
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (CheckTargetProperty(property.serializedObject))
            {
                EditorGUI.PropertyField(position, property);
            }
        }
        /// <summary>
        /// Get the property height (0 if not displayed)
        /// </summary>
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            if (CheckTargetProperty(property.serializedObject))
            {
                return EditorGUI.GetPropertyHeight(property, label);
            }
            else return 0;
        }
    }
}