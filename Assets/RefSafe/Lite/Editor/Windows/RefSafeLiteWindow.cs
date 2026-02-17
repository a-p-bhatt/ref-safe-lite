using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace RefSafe.Lite
{
    /// <summary>
    /// Main EditorWindow for RefSafe Lite.
    /// Professional design with welcome screen, severity-coded card results,
    /// statistics toolbar, and status footer.
    /// Menu: Tools → RefSafe → Lite
    /// </summary>
    public sealed class RefSafeLiteWindow : EditorWindow
    {
        private ScanResult _scanResult;
        private ScanState _scanState;
        private OpenSceneScanner _scanner;
        private LiteToolbar _toolbar;
        private LiteResultsView _resultsView;

        // ── Menu Item ─────────────────────────────────────────────────

        [MenuItem("Tools/RefSafe/Lite")]
        public static void Open()
        {
            var window = GetWindow<RefSafeLiteWindow>("RefSafe Lite");
            window.minSize = new Vector2(550, 450);
        }

        // ── Lifecycle ─────────────────────────────────────────────────

        private void OnEnable()
        {
            _scanState = new ScanState();
            _scanResult = new ScanResult();
            _scanner = new OpenSceneScanner();
            _toolbar = new LiteToolbar(StartScan, ClearResults, _scanState, _scanResult);
            _resultsView = new LiteResultsView(_scanResult);

            EditorSceneManager.sceneOpened += OnSceneOpened;
            EditorSceneManager.sceneClosed += OnSceneClosed;
        }

        private void OnDisable()
        {
            EditorSceneManager.sceneOpened -= OnSceneOpened;
            EditorSceneManager.sceneClosed -= OnSceneClosed;
        }

        // ── Main GUI ──────────────────────────────────────────────────

        private void OnGUI()
        {
            // Toolbar (always visible)
            _toolbar.Draw();

            EditorGUILayout.Space(5);

            // Content area
            if (_scanResult.HasIssues)
            {
                _resultsView.Draw();
            }
            else if (_scanState.HasScanned)
            {
                DrawNoIssuesScreen();
            }
            else
            {
                DrawWelcomeScreen();
            }

            EditorGUILayout.Space(10);

            // Footer (always visible)
            DrawFooter();
        }

        // ── Scan Actions ──────────────────────────────────────────────

        private void StartScan()
        {
            _scanResult.Clear();
            _resultsView.Reset();

            ScanResult result = _scanner.ScanAllOpenScenes(_scanState);
            _scanResult.AddIssues(result.Issues);

            Repaint();
        }

        private void ClearResults()
        {
            _scanResult.Clear();
            _scanState.Reset();
            _resultsView.Reset();
            Repaint();
        }

        // ── Welcome Screen ────────────────────────────────────────────

        private void DrawWelcomeScreen()
        {
            EditorGUILayout.BeginVertical();
            EditorGUILayout.Space(15);

            // Title
            var titleStyle = new GUIStyle(EditorStyles.largeLabel)
            {
                fontSize = 22,
                fontStyle = FontStyle.Bold,
                alignment = TextAnchor.MiddleCenter
            };
            EditorGUILayout.LabelField("RefSafe", titleStyle);

            EditorGUILayout.Space(4);

            var subtitleStyle = new GUIStyle(EditorStyles.label)
            {
                fontSize = 12,
                fontStyle = FontStyle.Italic,
                alignment = TextAnchor.MiddleCenter
            };
            EditorGUILayout.LabelField("Detect missing scripts and broken references", subtitleStyle);

            EditorGUILayout.Space(16);

            // Features section
            EditorGUILayout.LabelField("Features", EditorStyles.boldLabel);
            EditorGUILayout.Space(4);

            DrawFeatureCard("✓", "Detects missing scripts on GameObjects",
                "Critical issues that break prefabs and cause runtime errors");
            DrawFeatureCard("✓", "Finds missing object references",
                "Fields pointing to deleted or missing objects");
            DrawFeatureCard("✓", "Color-coded severity levels",
                "Critical (red) · Warning (amber) · Info (blue)");
            DrawFeatureCard("✓", "Quick selection & ping",
                "Jump to problem areas instantly in the Hierarchy");
            DrawFeatureCard("✓", "Copy hierarchy paths",
                "Easy sharing and documentation of issues");

            EditorGUILayout.Space(16);

            // Quick start hint
            EditorGUILayout.HelpBox(
                "Click '▶ Scan Open Scenes' to analyze all currently loaded scenes for issues.",
                MessageType.Info
            );

            EditorGUILayout.EndVertical();
        }

        private void DrawFeatureCard(string icon, string title, string description)
        {
            EditorGUILayout.BeginVertical(EditorStyles.helpBox);
            EditorGUILayout.LabelField($"{icon} {title}", EditorStyles.boldLabel);

            var descStyle = new GUIStyle(EditorStyles.miniLabel)
            {
                wordWrap = true
            };
            EditorGUILayout.LabelField(description, descStyle);

            EditorGUILayout.EndVertical();
            EditorGUILayout.Space(2);
        }

        // ── No Issues Screen ──────────────────────────────────────────

        private void DrawNoIssuesScreen()
        {
            GUILayout.FlexibleSpace();

            var checkStyle = new GUIStyle(EditorStyles.label)
            {
                fontSize = 28,
                alignment = TextAnchor.MiddleCenter
            };
            EditorGUILayout.LabelField("✔️", checkStyle, GUILayout.Height(40));

            EditorGUILayout.Space(4);

            var titleStyle = new GUIStyle(EditorStyles.label)
            {
                fontSize = 16,
                fontStyle = FontStyle.Bold,
                alignment = TextAnchor.MiddleCenter
            };
            EditorGUILayout.LabelField("No issues found!", titleStyle);

            EditorGUILayout.Space(4);

            var detailStyle = new GUIStyle(EditorStyles.label)
            {
                alignment = TextAnchor.MiddleCenter
            };
            EditorGUILayout.LabelField(
                $"Scanned {_scanState.ScenesScanned} scene(s) in {_scanState.ScanDurationSeconds:F2}s",
                detailStyle
            );

            EditorGUILayout.Space(6);

            EditorGUILayout.HelpBox(
                "Your open scenes are clean — no missing scripts or broken references detected.",
                MessageType.Info
            );

            GUILayout.FlexibleSpace();
        }

        // ── Footer ────────────────────────────────────────────────────

        private void DrawFooter()
        {
            // Separator
            Rect separator = GUILayoutUtility.GetRect(1, 1);
            EditorGUI.DrawRect(separator, new Color(0.5f, 0.5f, 0.5f, 0.2f));

            EditorGUILayout.BeginHorizontal(EditorStyles.toolbar);

            // Status text
            if (_scanState.IsScanning)
            {
                EditorGUILayout.LabelField("⌛︎ Scanning in progress...", EditorStyles.miniLabel);
            }
            else if (_scanState.HasScanned && _scanResult.HasIssues)
            {
                string count = _scanResult.TotalIssues == 1
                    ? "1 issue found"
                    : $"{_scanResult.TotalIssues} issues found";
                EditorGUILayout.LabelField($"✓ {count}", EditorStyles.miniLabel);
            }
            else if (_scanState.HasScanned)
            {
                EditorGUILayout.LabelField("✓ All clear", EditorStyles.miniLabel);
            }
            else
            {
                EditorGUILayout.LabelField("✓ Ready to scan", EditorStyles.miniLabel);
            }

            GUILayout.FlexibleSpace();

            // Support button
            if (GUILayout.Button(new GUIContent("Support", "Report issues or request features on GitHub"), EditorStyles.toolbarButton, GUILayout.Width(80)))
            {
                Application.OpenURL("https://github.com/a-p-bhatt/ref-safe-lite");
            }

            // Get Pro button (Prominent)
            var getProStyle = new GUIStyle(EditorStyles.toolbarButton);
            getProStyle.normal.textColor = new Color(1f, 0.8f, 0.2f); // Gold-ish color
            getProStyle.fontStyle = FontStyle.Bold;

            if (GUILayout.Button(new GUIContent("★ Get Pro", "Upgrade to RefSafe Pro for Project-Wide scanning, Exports, and more!"), getProStyle, GUILayout.Width(75)))
            {
                Application.OpenURL("https://assetstore.unity.com/"); // TODO: Replace with Pro asset store link
            }

            GUILayout.Space(10);

            // Scene info
            int sceneCount = UnityEngine.SceneManagement.SceneManager.sceneCount;
            EditorGUILayout.LabelField($"Scenes: {sceneCount}", EditorStyles.miniLabel, GUILayout.Width(75));

            EditorGUILayout.LabelField($"v{RefSafeVersion.Version}", EditorStyles.miniLabel, GUILayout.Width(45));

            EditorGUILayout.EndHorizontal();
        }

        // ── Scene Change Handlers ─────────────────────────────────────

        private void OnSceneOpened(
            UnityEngine.SceneManagement.Scene scene,
            OpenSceneMode mode)
        {
            if (_scanState.HasScanned)
                ClearResults();
        }

        private void OnSceneClosed(UnityEngine.SceneManagement.Scene scene)
        {
            if (_scanState.HasScanned)
                ClearResults();
        }

        // ── Repaint Loop ──────────────────────────────────────────────

        private void Update()
        {
            if (_scanState.IsScanning)
            {
                Repaint();
            }
        }
    }
}
