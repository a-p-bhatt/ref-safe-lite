using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace RefSafe.Lite
{
    /// <summary>
    /// Scans all currently open and loaded scenes for missing references and scripts.
    /// Traverses every root GameObject in each scene and runs the ReferenceDetector
    /// against the full hierarchy. Tracks scan timing via ScanState.
    /// </summary>
    public sealed class OpenSceneScanner
    {
        private readonly ReferenceDetector _detector = new ReferenceDetector();

        /// <summary>
        /// Scan all open and loaded scenes. Updates the provided ScanState with timing info.
        /// </summary>
        /// <param name="state">State tracker to record scan progress and duration.</param>
        /// <returns>Aggregated scan result with all issues found across all open scenes.</returns>
        public ScanResult ScanAllOpenScenes(ScanState state)
        {
            var result = new ScanResult();
            state.BeginScan();

            var stopwatch = Stopwatch.StartNew();
            int scenesScanned = 0;

            int sceneCount = SceneManager.sceneCount;
            for (int i = 0; i < sceneCount; i++)
            {
                Scene scene = SceneManager.GetSceneAt(i);

                if (!scene.IsValid() || !scene.isLoaded)
                    continue;

                ScanScene(scene, result);
                scenesScanned++;
            }

            stopwatch.Stop();
            state.EndScan((float)stopwatch.Elapsed.TotalSeconds, scenesScanned);

            return result;
        }

        private void ScanScene(Scene scene, ScanResult result)
        {
            string sceneName = string.IsNullOrEmpty(scene.name) ? "Untitled" : scene.name;
            string scenePath = scene.path ?? string.Empty;

            GameObject[] rootObjects = scene.GetRootGameObjects();

            foreach (GameObject root in rootObjects)
            {
                ScanResult hierarchyResult = _detector.ValidateHierarchy(
                    root, sceneName, scenePath
                );
                result.AddIssues(hierarchyResult.Issues);
            }
        }
    }
}
