// Note: Lambdas in attributes not supported in C# (yet)
// using UnityEngine;
// using UnityEditor;

// namespace UModules.Editor
// {
//     /// <summary>
//     /// Property drawer to display a property based on a given predicate
//     /// </summary>
//     /// <module>UM_Basic</module>
//     [CustomPropertyDrawer(typeof(OnlyShowWhenAttribute))]
//     public class OnlyShowWhenAttributeDrawer : PropertyDrawer
//     {
//         /// <summary>
//         /// Draw the property if the predicate is true
//         /// </summary>
//         public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
//         {
//             OnlyShowWhenAttribute show = attribute as OnlyShowWhenAttribute;
//             if (show.predicate())
//             {
//                 EditorGUI.PropertyField(position, property);
//             }
//         }
//         /// <summary>
//         /// Get the property height (0 if not displayed)
//         /// </summary>
//         public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
//         {
//             OnlyShowWhenAttribute show = attribute as OnlyShowWhenAttribute;
//             if (show.predicate())
//             {
//                 return base.GetPropertyHeight(property, label);
//             }
//             else return 0;
//         }
//     }
// }