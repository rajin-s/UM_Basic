/*
    UModules::ButtonAttribute
    UModules::ReadonlyAttribute
    UModules::OnlyShowIfAttribute
    UModules::DontShowIfAttribute
    
    Future: UModules::OnlyShowWhenAttribute

    by: Rajin Shankar
    part of: UM_Basic

    available to use according to UM_Basic/LICENSE
 */

using UnityEngine;

namespace UModules
{
    /// <summary>
    /// Attribute to create a simple button that calls a method through SendMessage
    /// </summary>
    /// <module>UM_Basic</module>
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
    /// <module>UM_Basic</module>
    public class ReadonlyAttribute : PropertyAttribute { }

    /// <summary>
    /// Attribute to only display a property when a given serialized property is true
    /// </summary>
    /// <module>UM_Basic</module>
    public class OnlyShowIfAttribute : PropertyAttribute
    {
        public string propertyName;

        public OnlyShowIfAttribute(string propertyName)
        {
            this.propertyName = propertyName;
        }
    }
    /// <summary>
    /// Attribute to only display a property when a given serialized property is false
    /// </summary>
    /// <module>UM_Basic</module>
    public class DontShowIfAttribute : PropertyAttribute
    {
        public string propertyName;

        public DontShowIfAttribute(string propertyName)
        {
            this.propertyName = propertyName;
        }
    }

    // Note: Lambdas in attributes not supported in C# (yet)
    // /// <summary>
    // /// Attribute to only diplay a property when a given predicate is true
    // /// </summary>
    // /// <module>UM_Basic</module>
    // public class OnlyShowWhenAttribute : PropertyAttribute
    // {
    //     public System.Func<bool> predicate;

    //     public OnlyShowWhenAttribute(System.Func<bool> predicate)
    //     {
    //         this.predicate = predicate;
    //     }
    // }
}
