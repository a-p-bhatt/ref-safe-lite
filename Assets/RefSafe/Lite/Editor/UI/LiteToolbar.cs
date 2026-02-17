using System;
using UnityEditor;
using UnityEngine;

namespace RefSafe.Lite
{
    /// <summary>
    /// Professional toolbar with scan button, clear button, and statistics row.
    /// Statistics row shows total count + severity breakdown with colored indicators.
    /// </summary>
    public sealed class LiteToolbar
    {
        private readonly Action _onScan;
        private readonly Action _onClear;
        private readonly ScanState _scanState;
        private readonly ScanResult _scanResult;

        public LiteToolbar(
            Action onScan,
            Action onClear,
            ScanState scanState,
            ScanResult scanResult)
        {
            _onScan = onScan;
            _onClear = onClear;
            _scanState = scanState;
            _scanResult = scanResult;
        }

        public void Draw()
        {
            // ── Primary Toolbar ───────────────────────────────────────
            EditorGUILayout.BeginHorizontal(EditorStyles.toolbar);

            // Scan button
            GUI.enabled = !_scanState.IsScanning;
            if (GUILayout.Button("▶ Scan Open Scenes", EditorStyles.toolbarButton, GUILayout.MinWidth(140)))
            {
                _onScan?.Invoke();
            }

            // Pro Teaser: Entire Project
            GUI.enabled = false;
            var proContent = new GUIContent("Entire Project (Pro)", "Unlock project-wide scanning in RefSafe Pro");
            GUILayout.Button(proContent, EditorStyles.toolbarButton, GUILayout.MinWidth(120));
            GUI.enabled = true;

            // Clear button
            GUI.enabled = _scanResult.HasIssues;
            if (GUILayout.Button("✕ Clear", EditorStyles.toolbarButton, GUILayout.Width(65)))
            {
                _onClear?.Invoke();
            }
            GUI.enabled = true;

            GUILayout.FlexibleSpace();

            // Scene count
            int loadedScenes = UnityEngine.SceneManagement.SceneManager.sceneCount;
            EditorGUILayout.LabelField(
                $"Scenes: {loadedScenes}",
                EditorStyles.miniLabel,
                GUILayout.Width(70)
            );

            EditorGUILayout.EndHorizontal();

            // ── Statistics Row (shown after scan) ─────────────────────
            if (_scanResult.HasIssues)
            {
                DrawStatisticsRow();
            }
        }

        private void DrawStatisticsRow()
        {
            EditorGUILayout.Space(3);
            EditorGUILayout.BeginHorizontal();

            // Total count
            var totalStyle = new GUIStyle(EditorStyles.miniLabel)
            {
                fontStyle = FontStyle.Bold,
                fontSize = 11
            };
            EditorGUILayout.LabelField(
                $"Total: {_scanResult.TotalIssues}",
                totalStyle,
                GUILayout.Width(75)
            );

            GUILayout.Space(10);

            // Critical
            DrawColoredStat(IssueSeverity.Critical, $"Critical: {_scanResult.CriticalCount}", 95);

            // Warning
            DrawColoredStat(IssueSeverity.Warning, $"Warning: {_scanResult.WarningCount}", 95);

            // Info
            DrawColoredStat(IssueSeverity.Info, $"Info: {_scanResult.InfoCount}", 70);

            GUILayout.FlexibleSpace();

            // Duration
            if (_scanState.HasScanned)
            {
                EditorGUILayout.LabelField(
                    $"{_scanState.ScanDurationSeconds:F2}s",
                    EditorStyles.miniLabel,
                    GUILayout.Width(45)
                );
            }

            EditorGUILayout.EndHorizontal();
            EditorGUILayout.Space(3);
        }

        private void DrawColoredStat(IssueSeverity severity, string text, float width)
        {
            EditorGUILayout.BeginHorizontal();

            // Colored icon
            Color color = SeverityColors.GetForeground(severity);
            string icon = SeverityColors.GetIcon(severity);

            GUI.color = color;
            EditorGUILayout.LabelField(icon, EditorStyles.miniLabel, GUILayout.Width(14));
            GUI.color = Color.white;

            EditorGUILayout.LabelField(text, EditorStyles.miniLabel, GUILayout.Width(width - 14));

            EditorGUILayout.EndHorizontal();
        }
    }
}
