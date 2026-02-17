using System.Collections.Generic;
using UnityEngine;

namespace RefSafe.Lite
{
    /// <summary>
    /// Interface for all validation rules in the detection engine.
    /// Each rule inspects a single GameObject and yields any issues found.
    /// Rules are stateless and reusable across multiple scan sessions.
    /// </summary>
    public interface IValidationRule
    {
        /// <summary>Human-readable name of this rule for logging and display.</summary>
        string RuleName { get; }

        /// <summary>
        /// Validate a single GameObject and yield all issues found.
        /// </summary>
        /// <param name="gameObject">The GameObject to validate.</param>
        /// <param name="sceneName">Display name of the scene containing this object.</param>
        /// <param name="scenePath">Asset path of the scene (e.g., Assets/Scenes/Main.unity).</param>
        /// <returns>Zero or more scan issues found on this object.</returns>
        IEnumerable<ScanIssue> Validate(GameObject gameObject, string sceneName, string scenePath);
    }
}
