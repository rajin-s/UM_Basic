/*
    UModules::GlobalEventAsset

    by: Rajin Shankar
    part of: UM_Basic

    available to use according to UM_Basic/LICENSE
 */

using System;
using UnityEngine;

namespace UModules
{
    /// <summary>
    /// A reusable asset representing a single communication channel.
    /// Behaviours can subscribe to a global event with some callback, or notify all subscribers to execute those callbacks.
    /// </summary>
    /// <module>UM_Basic</module>
    [CreateAssetMenu(fileName = "GlobalEvent_", menuName = "UModules/Global Event", order = 11)]
    public class GlobalEventAsset : ScriptableObject
    {
        /// <summary>
        /// Callbacks to execute that take an input parameter
        /// </summary>
        /// <access>private Action(object)</access>
        private Action<object> sendWithObject = null;
        /// <summary>
        /// Callbacks to execute that don't take any input parameters
        /// </summary>
        /// <access>private Action()</access>
        private Action sendWithoutObject = null;

        /// <summary>
        /// Subscribe to the event with a callback that takes an input parameter
        /// </summary>
        /// <access>public void</access>
        /// <param name="receiverCallback" type="Action(object)">The method to be executed when the event is sent</param>
        public void Subscribe(Action<object> receiverCallback) { sendWithObject += receiverCallback; }
        /// <summary>
        /// Unsubscribe a callback that takes an input parameter from the event
        /// </summary>
        /// <access>public void</access>
        /// <param name="receiverCallback" type="Action(object)">The method to no longer be executed when the event is sent</param>
        public void Unsubscribe(Action<object> receiverCallback) { sendWithObject -= receiverCallback; }
        /// <summary>
        /// Subscribe to the event with a callback that doesn't take an input parameter
        /// </summary>
        /// <access>public void</access>
        /// <param name="receiverCallback" type="Action()">The method to be executed when the event is sent</param>
        public void Subscribe(Action receiverCallback) { sendWithoutObject += receiverCallback; }
        /// <summary>
        /// Unsubscribe a callback that doesn't take an input parameter from the event
        /// </summary>
        /// <access>public void</access>
        /// <param name="receiverCallback" type="Action()">The method to no longer be executed when the event is sent</param>
        public void Unsubscribe(Action receiverCallback) { sendWithoutObject -= receiverCallback; }

        /// <summary>
        /// Send the event to execute registered callbacks
        /// </summary>
        /// <access>public void</access>
        /// <param name="content" type="object">Optional input parameter to send to subscribers that expect some input</param>
        public void Send(object content = null)
        {
            if (sendWithObject != null) sendWithObject(content);
            if (sendWithoutObject != null) sendWithoutObject();
        }
    }
}
