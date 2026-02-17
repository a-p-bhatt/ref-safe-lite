namespace RefSafe.Lite
{
    /// <summary>
    /// Immutable data class that describes exactly where a scan issue was found.
    /// Stores scene context, hierarchy path, component name, field name, and instance ID
    /// for navigation, ping, and selection functionality.
    /// </summary>
    public sealed class ReferenceLocation
    {
        /// <summary>Scene display name (e.g., "SampleScene").</summary>
        public string SceneName { get; }

        /// <summary>Full scene asset path (e.g., "Assets/Scenes/SampleScene.unity").</summary>
        public string ScenePath { get; }

        /// <summary>Name of the affected GameObject.</summary>
        public string GameObjectName { get; }

        /// <summary>Full hierarchy path (e.g., "Canvas/Panel/Button").</summary>
        public string HierarchyPath { get; }

        /// <summary>Component type name (e.g., "Image", "BoxCollider"). Empty for missing script issues.</summary>
        public string ComponentName { get; }

        /// <summary>Serialized field display name (e.g., "Sprite", "Target"). Empty for missing script issues.</summary>
        public string FieldName { get; }

        /// <summary>Unity instance ID of the affected GameObject, used for ping and selection.</summary>
        public int GameObjectInstanceId { get; }

        public ReferenceLocation(
            string sceneName,
            string scenePath,
            string gameObjectName,
            string hierarchyPath,
            string componentName,
            string fieldName,
            int gameObjectInstanceId)
        {
            SceneName = sceneName ?? string.Empty;
            ScenePath = scenePath ?? string.Empty;
            GameObjectName = gameObjectName ?? string.Empty;
            HierarchyPath = hierarchyPath ?? string.Empty;
            ComponentName = componentName ?? string.Empty;
            FieldName = fieldName ?? string.Empty;
            GameObjectInstanceId = gameObjectInstanceId;
        }

        /// <summary>
        /// Human-readable location string for display in results.
        /// </summary>
        public override string ToString()
        {
            if (!string.IsNullOrEmpty(FieldName))
                return $"{HierarchyPath} → {ComponentName}.{FieldName}";

            if (!string.IsNullOrEmpty(ComponentName))
                return $"{HierarchyPath} → {ComponentName}";

            return HierarchyPath;
        }
    }
}
