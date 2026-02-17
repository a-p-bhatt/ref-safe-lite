using System.Collections.Generic;
using UnityEngine;

namespace RefSafe.Lite
{
    /// <summary>
    /// Main detection orchestrator that runs all validation rules against GameObjects.
    /// Supports single-object validation and recursive hierarchy scanning.
    /// Stateless and reusable across multiple scan sessions.
    /// </summary>
    public sealed class ReferenceDetector
    {
        private readonly List<IValidationRule> _rules;

        /// <summary>Number of active validation rules.</summary>
        public int RuleCount => _rules.Count;

        public ReferenceDetector()
        {
            _rules = new List<IValidationRule>
            {
                new MissingScriptRule(),
                new MissingReferenceRule()
            };
        }

        // ── Single Object Validation ──────────────────────────────────

        /// <summary>
        /// Validate a single GameObject against all registered rules.
        /// </summary>
        /// <param name="gameObject">The GameObject to validate.</param>
        /// <param name="sceneName">Display name of the containing scene.</param>
        /// <param name="scenePath">Asset path of the containing scene.</param>
        /// <returns>All issues found on this object.</returns>
        public IEnumerable<ScanIssue> ValidateGameObject(
            GameObject gameObject,
            string sceneName,
            string scenePath)
        {
            if (gameObject == null)
                yield break;

            foreach (IValidationRule rule in _rules)
            {
                foreach (ScanIssue issue in rule.Validate(gameObject, sceneName, scenePath))
                {
                    yield return issue;
                }
            }
        }

        // ── Recursive Hierarchy Scan ──────────────────────────────────

        /// <summary>
        /// Validate a root GameObject and all its children recursively.
        /// Returns a complete ScanResult with all issues found in the hierarchy.
        /// </summary>
        /// <param name="root">Root GameObject to start scanning from.</param>
        /// <param name="sceneName">Display name of the containing scene.</param>
        /// <param name="scenePath">Asset path of the containing scene.</param>
        /// <returns>Aggregated scan result for the entire hierarchy.</returns>
        public ScanResult ValidateHierarchy(
            GameObject root,
            string sceneName,
            string scenePath)
        {
            var result = new ScanResult();
            ValidateRecursive(root, sceneName, scenePath, result);
            return result;
        }

        // ── Custom Rules ──────────────────────────────────────────────

        /// <summary>
        /// Register an additional validation rule.
        /// Custom rules run after built-in rules.
        /// </summary>
        public void AddRule(IValidationRule rule)
        {
            if (rule != null)
                _rules.Add(rule);
        }

        // ── Private Helpers ───────────────────────────────────────────

        private void ValidateRecursive(
            GameObject gameObject,
            string sceneName,
            string scenePath,
            ScanResult result)
        {
            if (gameObject == null)
                return;

            // Validate this object
            foreach (ScanIssue issue in ValidateGameObject(gameObject, sceneName, scenePath))
            {
                result.AddIssue(issue);
            }

            // Recurse into all children
            foreach (Transform child in gameObject.transform)
            {
                ValidateRecursive(child.gameObject, sceneName, scenePath, result);
            }
        }
    }
}
