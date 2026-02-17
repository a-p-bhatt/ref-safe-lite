using UnityEngine;

namespace RefSafe.Lite
{
    /// <summary>
    /// Utility for building full hierarchy path strings from Transforms.
    /// Used by validation rules to create descriptive ReferenceLocation paths.
    /// </summary>
    internal static class HierarchyUtility
    {
        /// <summary>
        /// Builds the full hierarchy path from root to the given Transform.
        /// Example output: "Canvas/Panel/Button"
        /// </summary>
        /// <param name="transform">The Transform to build a path for.</param>
        /// <returns>Slash-separated hierarchy path, or empty string if transform is null.</returns>
        public static string GetHierarchyPath(Transform transform)
        {
            if (transform == null)
                return string.Empty;

            string path = transform.name;
            Transform current = transform.parent;

            while (current != null)
            {
                path = current.name + "/" + path;
                current = current.parent;
            }

            return path;
        }
    }
}
