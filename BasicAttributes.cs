using UnityEngine;

namespace UModules
{
    /// <summary>
    /// Attribute to create a simple button that calls a method through SendMessage
    /// </summary>
    public class ButtonAttribute : PropertyAttribute
    {
        /// <summary>
        /// The name of the method to call
        /// </summary>
        public string methodName;
        /// <summary>
        /// The text to display on the button in the inspector
        /// </summary>
        public string displayName;
        /// <summary>
        /// Should the button be grouped on the same line as the property it's attached to?
        /// </summary>
        public bool groupHorizontal;

        public ButtonAttribute(string methodName, string displayName, bool groupHorizontal = true)
        {
            this.methodName = methodName;
            this.displayName = displayName;
            this.groupHorizontal = groupHorizontal;
        }
        public ButtonAttribute(string methodName, bool groupHorizontal = true) : this(methodName, methodName, groupHorizontal) { }

        /// <summary>
        /// Call SendMessage on a given target with the button's method name
        /// </summary>
        /// <param name="target">The MonoBehaviour to send the message to</param>
        public void Invoke(MonoBehaviour target)
        {
            target.SendMessage(methodName, SendMessageOptions.DontRequireReceiver);
        }
    }

    /// <summary>
    /// Attribute to display a property but disable editing
    /// </summary>
    public class ReadonlyAttribute : PropertyAttribute { }
}
