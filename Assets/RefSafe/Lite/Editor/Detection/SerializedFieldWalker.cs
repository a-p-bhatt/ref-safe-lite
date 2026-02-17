using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace RefSafe.Lite
{
    /// <summary>
    /// Static utility that iterates serialized properties of a Component
    /// to find ObjectReference fields. Provides helper methods for detecting
    /// broken (missing) references.
    /// </summary>
    internal static class SerializedFieldWalker
    {
        /// <summary>
        /// Yields all visible ObjectReference serialized properties on a component,
        /// excluding internal Unity fields like m_Script.
        /// </summary>
        /// <param name="component">The component to inspect. Must not be null.</param>
        /// <returns>Copies of each ObjectReference SerializedProperty found.</returns>
        public static IEnumerable<SerializedProperty> GetObjectReferenceFields(Component component)
        {
            if (component == null)
                yield break;

            var serializedObject = new SerializedObject(component);
            SerializedProperty iterator = serializedObject.GetIterator();
            bool enterChildren = true;

            while (iterator.NextVisible(enterChildren))
            {
                enterChildren = true;

                // Skip internal Unity script reference field —
                // missing scripts are handled separately by MissingScriptRule
                if (iterator.name == "m_Script")
                    continue;

                if (iterator.propertyType == SerializedPropertyType.ObjectReference)
                {
                    yield return iterator.Copy();
                }
            }
        }

        /// <summary>
        /// Checks if a SerializedProperty contains a missing (broken) reference.
        /// A missing reference means the field was assigned to an object (non-zero instance ID)
        /// but the target no longer exists (null value).
        /// </summary>
        public static bool IsMissingReference(SerializedProperty property)
        {
            if (property == null)
                return false;

            if (property.propertyType != SerializedPropertyType.ObjectReference)
                return false;

            // objectReferenceValue == null: the target object doesn't exist
            // objectReferenceInstanceIDValue != 0: the field WAS assigned to something
            // Together: this was a valid reference that is now broken
            return property.objectReferenceValue == null
                && property.objectReferenceInstanceIDValue != 0;
        }
    }
}
