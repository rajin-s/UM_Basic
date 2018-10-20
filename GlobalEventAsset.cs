using System;
using UnityEngine;

namespace UModules
{
    /// <summary>
    /// A reusable asset representing a single communication channel.
    /// Behaviours can subscribe to a global event with some callback, or notify all subscribers to execute those callbacks.
    /// </summary>
    [CreateAssetMenu(fileName = "GlobalEvent_", menuName = "UModules/Global Event", order = 1)]
    public class GlobalEventAsset : ScriptableObject
    {
        /// <summary>
        /// Callbacks to execute that take an input parameter
        /// </summary>
        private Action<object> sendWithObject = null;
        /// <summary>
        /// Callbacks to execute that don't take any input parameters
        /// </summary>
        private Action sendWithoutObject = null;

        /// <summary>
        /// Subscribe to the event with a callback that takes an input parameter
        /// </summary>
        /// <param name="receiverCallback">The method to be executed when the event is sent</param>
        public void Subscribe(Action<object> receiverCallback) { sendWithObject += receiverCallback; }
        /// <summary>
        /// Unsubscribe a callback that takes an input parameter from the event
        /// </summary>
        /// <param name="receiverCallback">The method to no longer be executed when the event is sent</param>
        public void Unsubscribe(Action<object> receiverCallback) { sendWithObject -= receiverCallback; }
        /// <summary>
        /// Subscribe to the event with a callback that doesn't take an input parameter
        /// </summary>
        /// <param name="receiverCallback">The method to be executed when the event is sent</param>
        public void Subscribe(Action receiverCallback) { sendWithoutObject += receiverCallback; }
        /// <summary>
        /// Unsubscribe a callback that doesn't take an input parameter from the event
        /// </summary>
        /// <param name="receiverCallback">The method to no longer be executed when the event is sent</param>
        public void Unsubscribe(Action receiverCallback) { sendWithoutObject -= receiverCallback; }

        /// <summary>
        /// Send the event to execute registered callbacks
        /// </summary>
        /// <param name="content">Optional input parameter to send to subscribers that expect some input</param>
        public void Send(object content = null)
        {
            if (sendWithObject != null) sendWithObject(content);
            if (sendWithoutObject != null) sendWithoutObject();
        }
    }
}
