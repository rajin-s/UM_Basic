/*
    UModules::ExtendedBehaviour

    by: Rajin Shankar
    part of: UModules::Basic

    available to use according to UM_Basic/LICENSE
 */

using UnityEngine;

namespace UModules
{
    /// <summary>
    /// Basic usability extensions to MonoBehaviour
    /// </summary>
    /// <module>UM_Basic</module>
    public abstract class ExtendedBehaviour : MonoBehaviour
    {
        /// <summary>
        /// Cached Transform component, if it has been gotten yet
        /// </summary>
        /// <access>private Transform</access>
        private Transform _transform;
        /// <summary>
        /// Redefined transform property to get Transform component on first reference, then return cached value
        /// </summary>
        /// <access>new public Transform</access>
        new public Transform transform { get { return Get(ref _transform); } }

        /// <summary>
        /// Child classes can implement initialization here
        /// </summary>
        /// <access>public virtual void</access>
        public virtual void Initialize() { }

        /// <summary>
        /// Call initialize on Awake
        /// </summary>
        /// <access>protected void</access>
        protected void Awake()
        {
            Initialize();
        }

        /// <summary>
        /// Get a cached component value if it has been set, otherwise set the given variable using GetComponent. Used to implement call-by-need variables.
        /// </summary>
        /// <param name="target" type="ref T:Component">A reference to the variable to check/set (type is infered from variable type)</param>
        /// <param name="warnIfNotFound" type="bool">Should a warning be printed if the component couldn't be found?</param>
        /// <returns>Component attached to this object that is the same type as the target variable</returns>
        /// <example>
        /// private BoxCollider _box;
        /// public BoxCollider Box { get { return Get(_box); } }
        /// </example>
        public T Get<T>(ref T target, bool warnIfNotFound = true) where T : Component
        {
            if (target)
            {
                return target;
            }
            else
            {
                target = GetComponent<T>();
                if (warnIfNotFound && target == null) { Warnf("Could not get {0}!", typeof(T).ToString()); }
                return target;
            }
        }

        /// <summary>
        /// Get a cached component value if it has been set, otherwise set the given variable using GetComponentInChildren. Used to implement call-by-need variables.
        /// </summary>
        /// <param name="target" type="ref T:Component">A reference to the variable to check/set (type is infered from variable type)</param>
        /// <param name="warnIfNotFound" type="bool">Should a warning be printed if the component couldn't be found?</param>
        /// <returns>Component attached to this object or one of its children that is the same type as the target variable</returns>
        /// <example>
        /// private BoxCollider _box;
        /// public BoxCollider Box { get { return GetInChildren(_box); } }
        /// </example>
        public T GetInChildren<T>(ref T target, bool warnIfNotFound = true) where T : Component
        {
            if (target)
            {
                return target;
            }
            else
            {
                target = GetComponentInChildren<T>();
                if (warnIfNotFound && target == null) { Warnf("Could not get {0} in children!", typeof(T).ToString()); }
                return target;
            }
        }

        /// <summary>
        /// Get a cached component value if it has been set, otherwise set the given variable using GetComponentInParent. Used to implement call-by-need variables.
        /// </summary>
        /// <param name="target" type="ref T:Component">A reference to the variable to check/set (type is infered from variable type)</param>
        /// <param name="warnIfNotFound" type="bool">Should a warning be printed if the component couldn't be found?</param>
        /// <returns>Component attached to this object or one of its ancestors that is the same type as the target variable</returns>
        /// <example>
        /// private BoxCollider _box;
        /// public BoxCollider Box { get { return GetInParent(_box); } }
        /// </example>
        public T GetInParent<T>(ref T target, bool warnIfNotFound = true) where T : Component
        {
            if (target)
            {
                return target;
            }
            else
            {
                target = GetComponentInParent<T>();
                if (warnIfNotFound && target == null) { Warnf("Could not get {0} in parent!", typeof(T).ToString()); }
                return target;
            }
        }

        /// <summary>
        /// Debug log function that automatically formats output.
        /// Adds class name to start of message, and caller name on second line
        /// </summary>
        /// <access>public void</access>
        /// <param name="format" type="string"></param>
        /// <param name="args" type="params object[]"></param>
        public void Printf(string format, params object[] args)
        {
            Debug.Log(string.Format("{0} > {1}\nCaller: {2}", GetType().ToString(), string.Format(format, args), name));
        }

        /// <summary>
        /// Debug log Warning function that automatically formats output.
        /// Adds class name to start of message, and caller name on second line
        /// </summary>
        /// <access>public void</access>
        /// <param name="format" type="string"></param>
        /// <param name="args" type="params object[]"></param>
        public void Warnf(string format, params object[] args)
        {
            Debug.LogWarning(string.Format("{0} > {1}\nCaller: {2}", GetType().ToString(), string.Format(format, args), name));
        }
    }
}
