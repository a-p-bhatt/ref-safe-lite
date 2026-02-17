using UnityEditor;
using UnityEngine;

namespace RefSafe.Lite
{
    /// <summary>
    /// Utility for pinging objects in the Unity Hierarchy and Project windows.
    /// Pinging highlights the object with a brief flash animation.
    /// </summary>
    internal static class PingUtility
    {
        /// <summary>
        /// Ping a GameObject by its instance ID.
        /// The object will flash in the Hierarchy window.
        /// </summary>
        /// <param name="instanceId">Unity instance ID of the object to ping.</param>
        public static void PingObject(int instanceId)
        {
            Object obj = EditorUtility.InstanceIDToObject(instanceId);
            if (obj != null)
            {
                EditorGUIUtility.PingObject(obj);
            }
        }
    }
}
