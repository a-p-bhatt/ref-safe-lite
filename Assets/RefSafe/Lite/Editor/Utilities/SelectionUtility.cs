using UnityEditor;
using UnityEngine;

namespace RefSafe.Lite
{
    /// <summary>
    /// Utility for selecting objects in the Unity Editor.
    /// Sets the active selection and pings the object for visibility.
    /// </summary>
    internal static class SelectionUtility
    {
        /// <summary>
        /// Select a GameObject by its instance ID.
        /// Sets it as the active selection in the Inspector and pings it.
        /// </summary>
        /// <param name="instanceId">Unity instance ID of the object to select.</param>
        public static void SelectObject(int instanceId)
        {
            Object obj = EditorUtility.InstanceIDToObject(instanceId);
            if (obj == null)
                return;

            Selection.activeObject = obj;

            // Also ping to make it visible in hierarchy
            if (obj is GameObject)
            {
                EditorGUIUtility.PingObject(obj);
            }
        }
    }
}
