using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace RefSafe.Lite
{
    /// <summary>
    /// Detects missing (broken) object references in serialized fields.
    /// A missing reference occurs when a field was assigned to an object that was
    /// subsequently deleted — the field retains a non-zero instance ID but the
    /// actual object is null.
    /// Severity: Warning — broken references may cause NullReferenceExceptions at runtime.
    /// </summary>
    public sealed class MissingReferenceRule : IValidationRule
    {
        public string RuleName => "Missing Reference Detection";

        public IEnumerable<ScanIssue> Validate(
            GameObject gameObject,
            string sceneName,
            string scenePath)
        {
            if (gameObject == null)
                yield break;

            Component[] components = gameObject.GetComponents<Component>();

            foreach (Component component in components)
            {
                // Skip null components — those are missing scripts,
                // handled by MissingScriptRule
                if (component == null)
                    continue;

                string componentTypeName = component.GetType().Name;

                foreach (SerializedProperty property in
                    SerializedFieldWalker.GetObjectReferenceFields(component))
                {
                    if (!SerializedFieldWalker.IsMissingReference(property))
                        continue;

                    string hierarchyPath = HierarchyUtility.GetHierarchyPath(gameObject.transform);

                    var location = new ReferenceLocation(
                        sceneName: sceneName,
                        scenePath: scenePath,
                        gameObjectName: gameObject.name,
                        hierarchyPath: hierarchyPath,
                        componentName: componentTypeName,
                        fieldName: property.displayName,
                        gameObjectInstanceId: gameObject.GetInstanceID()
                    );

                    yield return new ScanIssue(
                        type: IssueType.MissingReference,
                        severity: SeverityClassifier.Classify(IssueType.MissingReference),
                        message: $"Missing reference in '{property.displayName}' on {componentTypeName}",
                        location: location
                    );
                }
            }
        }
    }
}
