using UnityEngine;

namespace UModules
{
    public abstract class ExtendedBehaviour : MonoBehaviour
    {
        private Transform _transform;
        new public Transform transform { get { return _transform ?? (_transform = GetComponent<Transform>()); } }

        public virtual void Initialize() { }

        protected void Awake()
        {
            Initialize();
        }

        public void Printf(string format, params object[] args)
        {
            Debug.Log(string.Format("{0} > {1}", GetType().ToString(), string.Format(format, args)));
        }
    }
}
