using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace RefSafe.Lite
{
    /// <summary>
    /// Professional results view with card-based layout.
    /// Features: severity-tinted card backgrounds, issue numbering,
    /// detail rows (Scene, GameObject, Component, Field, Severity),
    /// and action buttons (Select, Ping, Copy Path).
    /// No keyboard navigation — mouse-only interaction.
    /// </summary>
    public sealed class LiteResultsView
    {
        private readonly ScanResult _scanResult;
        private Vector2 _scrollPosition;
        private readonly Dictionary<string, bool> _sceneFoldouts = new Dictionary<string, bool>();
        private int _selectedIndex = -1;

        public LiteResultsView(ScanResult scanResult)
        {
            _scanResult = scanResult;
        }

        // ── Public API ────────────────────────────────────────────────

        public void Draw()
        {
            if (!_scanResult.HasIssues)
                return;

            DrawHeader();

            _scrollPosition = EditorGUILayout.BeginScrollView(_scrollPosition);

            int globalIndex = 0;
            foreach (var sceneGroup in _scanResult.GroupByScene())
            {
                DrawSceneGroup(sceneGroup.Key, sceneGroup.ToList(), ref globalIndex);
            }

            EditorGUILayout.EndScrollView();
        }

        public void Reset()
        {
            _scrollPosition = Vector2.zero;
            _sceneFoldouts.Clear();
            _selectedIndex = -1;
        }

        // ── Header ────────────────────────────────────────────────────

        private void DrawHeader()
        {
            EditorGUILayout.Space(5);

            EditorGUILayout.BeginHorizontal();

            var headerStyle = new GUIStyle(EditorStyles.label)
            {
                fontSize = 12,
                fontStyle = FontStyle.Bold,
                alignment = TextAnchor.MiddleLeft,
                padding = new RectOffset(5, 5, 3, 3)
            };
            EditorGUILayout.LabelField("SCAN RESULTS", headerStyle);

            GUILayout.FlexibleSpace();

            string countText = _scanResult.TotalIssues == 1
                ? "Found 1 issue"
                : $"Found {_scanResult.TotalIssues} issues";
            EditorGUILayout.LabelField(countText, EditorStyles.miniLabel);

            EditorGUILayout.EndHorizontal();

            // Separator
            Rect separator = GUILayoutUtility.GetRect(1, 1);
            EditorGUI.DrawRect(separator, new Color(0.5f, 0.5f, 0.5f, 0.3f));

            EditorGUILayout.Space(5);
        }

        // ── Scene Group ───────────────────────────────────────────────

        private void DrawSceneGroup(
            string sceneName,
            List<ScanIssue> issues,
            ref int globalIndex)
        {
            if (!_sceneFoldouts.ContainsKey(sceneName))
                _sceneFoldouts[sceneName] = true;

            EditorGUILayout.Space(2);

            // Scene foldout header
            string label = issues.Count == 1
                ? $"  {sceneName}  (1 issue)"
                : $"  {sceneName}  ({issues.Count} issues)";

            var foldoutStyle = new GUIStyle(EditorStyles.foldoutHeader)
            {
                fontStyle = FontStyle.Bold,
                fontSize = 12
            };

            _sceneFoldouts[sceneName] = EditorGUILayout.Foldout(
                _sceneFoldouts[sceneName], label, true, foldoutStyle
            );

            if (!_sceneFoldouts[sceneName])
            {
                globalIndex += issues.Count;
                return;
            }

            EditorGUI.indentLevel++;

            foreach (ScanIssue issue in issues)
            {
                DrawIssueCard(issue, globalIndex);
                globalIndex++;
            }

            EditorGUI.indentLevel--;
        }

        // ── Issue Card ────────────────────────────────────────────────

        private void DrawIssueCard(ScanIssue issue, int index)
        {
            bool isSelected = (index == _selectedIndex);

            // Severity-tinted card background
            Color bgColor = GetCardBackground(issue.Severity, isSelected);

            GUI.backgroundColor = bgColor;
            EditorGUILayout.BeginVertical("box");
            GUI.backgroundColor = Color.white;

            // ── Card Header: #Number + Severity Icon + Message ────────
            EditorGUILayout.BeginHorizontal();

            // Issue number
            EditorGUILayout.LabelField(
                $"#{index + 1}",
                EditorStyles.boldLabel,
                GUILayout.Width(35)
            );

            // Colored severity icon
            DrawSeverityIcon(issue.Severity);

            // Issue message
            EditorGUILayout.LabelField(
                issue.Message,
                EditorStyles.boldLabel,
                GUILayout.ExpandWidth(true)
            );

            // Selection indicator
            if (isSelected)
            {
                EditorGUILayout.LabelField("◄", EditorStyles.miniLabel, GUILayout.Width(18));
            }

            EditorGUILayout.EndHorizontal();

            EditorGUILayout.Space(4);

            // ── Details Section ───────────────────────────────────────
            DrawDetailsSection(issue);

            EditorGUILayout.Space(5);

            // ── Action Buttons ────────────────────────────────────────
            DrawActionButtons(issue, index);

            EditorGUILayout.EndVertical();

            EditorGUILayout.Space(4);

            // Click-to-select on the entire card
            Rect cardRect = GUILayoutUtility.GetLastRect();
            if (Event.current.type == EventType.MouseDown
                && cardRect.Contains(Event.current.mousePosition))
            {
                _selectedIndex = (_selectedIndex == index) ? -1 : index;
                Event.current.Use();
            }
        }

        // ── Severity Icon ─────────────────────────────────────────────

        private void DrawSeverityIcon(IssueSeverity severity)
        {
            Color color = SeverityColors.GetForeground(severity);
            string icon = severity switch
            {
                IssueSeverity.Critical => "●",
                IssueSeverity.Warning  => "◆",
                IssueSeverity.Info     => "■",
                _                      => "○"
            };

            GUI.color = color;
            EditorGUILayout.LabelField(icon, EditorStyles.label, GUILayout.Width(18));
            GUI.color = Color.white;
        }

        // ── Details Section ───────────────────────────────────────────

        private void DrawDetailsSection(ScanIssue issue)
        {
            EditorGUILayout.BeginVertical(EditorStyles.helpBox);

            DrawDetailRow("Scene", issue.Location.SceneName);
            DrawDetailRow("GameObject", issue.Location.HierarchyPath);

            if (!string.IsNullOrEmpty(issue.Location.ComponentName))
                DrawDetailRow("Component", issue.Location.ComponentName);

            if (!string.IsNullOrEmpty(issue.Location.FieldName))
                DrawDetailRow("Field", issue.Location.FieldName);

            DrawSeverityRow(issue.Severity);

            EditorGUILayout.EndVertical();
        }

        private void DrawDetailRow(string label, string value)
        {
            if (string.IsNullOrEmpty(value))
                return;

            EditorGUILayout.BeginHorizontal();

            EditorGUILayout.LabelField(
                label + ":",
                EditorStyles.miniLabel,
                GUILayout.Width(85)
            );

            // SelectableLabel lets users copy text
            EditorGUILayout.SelectableLabel(
                value,
                EditorStyles.miniLabel,
                GUILayout.Height(EditorGUIUtility.singleLineHeight)
            );

            EditorGUILayout.EndHorizontal();
        }

        private void DrawSeverityRow(IssueSeverity severity)
        {
            EditorGUILayout.BeginHorizontal();

            EditorGUILayout.LabelField(
                "Severity:",
                EditorStyles.miniLabel,
                GUILayout.Width(85)
            );

            var style = new GUIStyle(EditorStyles.miniLabel)
            {
                fontStyle = FontStyle.Bold
            };
            style.normal.textColor = SeverityColors.GetForeground(severity);

            EditorGUILayout.LabelField(
                SeverityColors.GetLabel(severity),
                style
            );

            EditorGUILayout.EndHorizontal();
        }

        // ── Action Buttons ────────────────────────────────────────────

        private void DrawActionButtons(ScanIssue issue, int index)
        {
            EditorGUILayout.BeginHorizontal();

            // Select button
            if (GUILayout.Button("► Select", GUILayout.Height(26), GUILayout.MinWidth(80)))
            {
                SelectionUtility.SelectObject(issue.Location.GameObjectInstanceId);
                _selectedIndex = index;
            }

            // Ping button
            if (GUILayout.Button("◉ Ping", GUILayout.Height(26), GUILayout.Width(80)))
            {
                PingUtility.PingObject(issue.Location.GameObjectInstanceId);
                _selectedIndex = index;
            }

            // Copy Path button
            if (GUILayout.Button("⊞ Copy Path", GUILayout.Height(26), GUILayout.Width(100)))
            {
                string path = issue.Location.HierarchyPath;
                if (!string.IsNullOrEmpty(path))
                {
                    EditorGUIUtility.systemCopyBuffer = path;
                }
            }

            GUILayout.FlexibleSpace();

            EditorGUILayout.EndHorizontal();
        }

        // ── Color Helpers ─────────────────────────────────────────────

        private Color GetCardBackground(IssueSeverity severity, bool isSelected)
        {
            if (isSelected)
                return new Color(0.2f, 0.4f, 0.8f, 0.25f);

            return severity switch
            {
                IssueSeverity.Critical => new Color(1f, 0.2f, 0.2f, 0.08f),
                IssueSeverity.Warning  => new Color(1f, 0.8f, 0f, 0.08f),
                IssueSeverity.Info     => new Color(0.2f, 0.8f, 1f, 0.08f),
                _                      => new Color(0.5f, 0.5f, 0.5f, 0.08f)
            };
        }
    }
}
