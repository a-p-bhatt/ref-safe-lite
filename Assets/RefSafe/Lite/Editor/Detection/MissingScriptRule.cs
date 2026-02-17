using System.Collections.Generic;
using UnityEngine;

namespace RefSafe.Lite
{
    /// <summary>
    /// Detects GameObjects with missing MonoBehaviour scripts.
    /// When a script file is deleted or renamed, the component slot becomes null
    /// while still occupying a slot in GetComponents(). This rule catches those cases.
    /// Severity: Critical — missing scripts break prefabs and cause runtime errors.
    /// </summary>
    public sealed class MissingScriptRule : IValidationRule
    {
        public string RuleName => "Missing Script Detection";

        public IEnumerable<ScanIssue> Validate(
            GameObject gameObject,
            string sceneName,
            string scenePath)
        {
            if (gameObject == null)
                yield break;

            Component[] components = gameObject.GetComponents<Component>();

            for (int i = 0; i < components.Length; i++)
            {
                // A null entry in GetComponents<Component>() means the script is missing.
                // The component slot exists but the MonoBehaviour class cannot be found.
                if (components[i] != null)
                    continue;

                string hierarchyPath = HierarchyUtility.GetHierarchyPath(gameObject.transform);

                var location = new ReferenceLocation(
                    sceneName: sceneName,
                    scenePath: scenePath,
                    gameObjectName: gameObject.name,
                    hierarchyPath: hierarchyPath,
                    componentName: string.Empty,
                    fieldName: string.Empty,
                    gameObjectInstanceId: gameObject.GetInstanceID()
                );

                yield return new ScanIssue(
                    type: IssueType.MissingScript,
                    severity: SeverityClassifier.Classify(IssueType.MissingScript),
                    message: $"Missing script on '{gameObject.name}' (component index {i})",
                    location: location
                );
            }
        }
    }
}
