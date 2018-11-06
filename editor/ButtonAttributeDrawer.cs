using UnityEngine;
using UnityEditor;

namespace UModules.Editor
{
    /// <summary>
    /// Property drawer to display a ButtonAttribute
    /// </summary>
    /// <seealso cref="ButtonAttribute" />
    /// <module>UM_Basic</module>
    [CustomPropertyDrawer(typeof(ButtonAttribute))]
    public class ButtonAttributeDrawer : PropertyDrawer
    {
        /// <summary>
        /// Fixed values used in layout and drawing
        /// </summary>
        private const float buttonPadding = 4, buttonSize = 0.5f;

        /// <summary>
        /// Draw the button and property to which it is attached
        /// </summary>
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            // Get the ButtonAttribute
            ButtonAttribute button = attribute as ButtonAttribute;

            if (button.groupHorizontal) // Horizontal layout (same line)
            {
                float buttonWidth = position.width * buttonSize;
                float propertyWidth = position.width - buttonWidth - buttonPadding;

                position.width = propertyWidth;
                EditorGUI.PropertyField(position, property);

                position.x += propertyWidth + buttonPadding;
                position.width = buttonWidth;

                if (GUI.Button(position, button.displayName))
                {
                    MonoBehaviour parent = property.serializedObject.targetObject as MonoBehaviour;
                    button.Invoke(parent);
                }
            }
            else // Vertical layout (two lines)
            {
                position.height = (position.height - buttonPadding) / 2;
                if (GUI.Button(position, button.displayName))
                {
                    MonoBehaviour parent = property.serializedObject.targetObject as MonoBehaviour;
                    button.Invoke(parent);
                }
                position.y += position.height + buttonPadding;
                EditorGUI.PropertyField(position, property);
            }
        }

        /// <summary>
        /// Get the height of the whole button + property group
        /// </summary>
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            ButtonAttribute button = attribute as ButtonAttribute;

            if (button.groupHorizontal) return base.GetPropertyHeight(property, label);
            else return base.GetPropertyHeight(property, label) * 2 + buttonPadding;
        }
    }
}
