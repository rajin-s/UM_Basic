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
        new public Transform transform { get { return _transform ? _transform : (_transform = GetComponent<Transform>()); } }

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
    }
}
