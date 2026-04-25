# RefSafe Lite — Free Missing Reference Validator for Unity

RefSafe Lite helps you catch missing scripts and broken object references in your currently open Unity scenes, for free.

It is built for solo developers and smaller projects that want a simple editor tool to validate scene health before hitting Play, testing a feature, or preparing a small update.

If you need full project coverage, batch-style validation, or exportable reports for production workflows, RefSafe Pro is the upgrade path.

## What RefSafe Lite Does

- Scans **open scenes** for broken references
- Detects **missing scripts** on GameObjects
- Detects **missing object references** in components
- Uses **severity color coding** for Critical, Warning, and Info issues
- Lists results by scene for quick review
- Supports **Select & Ping** so you can jump to affected GameObjects instantly
- Supports Unity **2021 LTS, 2022 LTS, 2023 LTS, and Unity 6**

## Installation

1. Download or clone this repository.
2. Copy `Assets/RefSafe/Lite` into your Unity project's `Assets` folder.
3. Open **Tools > RefSafe > Lite**.

## Quick Start

1. Open the scene or scenes you want to validate in Unity.
2. Go to **Tools > RefSafe > Lite**.
3. Click **Scan**.
4. Review the results and use **Select & Ping** to jump directly to the affected object.

## Why Upgrade to RefSafe Pro

RefSafe Lite is intentionally focused: it helps you validate what is already open in the editor.

RefSafe Pro is for the moment when that is no longer enough. If you want to validate more than the scenes you currently have loaded, catch broader project health issues before release, or build a repeatable validation workflow for a team, Pro removes the manual work and gives you much broader coverage.

RefSafe Pro is better when you need to:

- scan your **entire project** instead of only open scenes,
- validate **all scenes** and **build settings** without opening everything manually,
- detect broken references and structural issues across **prefabs**, **ScriptableObjects**, **materials**, and supported project assets,
- catch deeper validation problems like **missing prefab instances**, **broken UnityEvents**, **invalid layers**, **invalid materials**, **shader errors**, **broken Addressables**, and **huge transforms**,
- run **changed-only** and broader project validation workflows,
- monitor long scans with **progress and cancel support**,
- narrow down results with **filtering, search, diff tracking, and batch review tools**,
- fix supported issues faster with **built-in fixers** and **Fix All**,
- share findings with teammates using **exportable reports** and **CI-friendly workflows**,
- use **reference finding**, **dependency views**, **cleaner tools**, and **health trends** to manage larger projects over time,
- extend RefSafe for studio workflows with **custom rules**, **custom fixers**, and **custom exporters**,
- work more confidently on larger projects, team projects, or release builds.

## Lite vs Pro

| Feature                                           | Lite | Pro |
| ------------------------------------------------- | :--: | :-: |
| Open Scenes Scan                                  |  ✅  | ✅  |
| All Scenes in Build Settings                      |  ❌  | ✅  |
| All Project Scenes                                |  ❌  | ✅  |
| Entire Project Scan                               |  ❌  | ✅  |
| Prefab Scanning                                   |  ❌  | ✅  |
| ScriptableObject Scanning                         |  ❌  | ✅  |
| Material Validation                               |  ❌  | ✅  |
| Project Settings / Deleted Build Scene Validation |  ❌  | ✅  |
| Unloaded Asset Validation                         |  ❌  | ✅  |
| Changed-Only Incremental Scan                     |  ❌  | ✅  |
| Context Menu Validation                           |  ❌  | ✅  |
| Drag-and-Drop Validation                          |  ❌  | ✅  |
| Missing Script Detection                          |  ✅  | ✅  |
| Missing Reference Detection                       |  ✅  | ✅  |
| Missing Serialized Reference Detection            |  ✅  | ✅  |
| Missing Prefab Detection                          |  ❌  | ✅  |
| Broken UnityEvent Detection                       |  ❌  | ✅  |
| Broken Addressable Detection                      |  ❌  | ✅  |
| Duplicate Component Detection                     |  ❌  | ✅  |
| Invalid Layer Detection                           |  ❌  | ✅  |
| Invalid Material Detection                        |  ❌  | ✅  |
| Shader Error Detection                            |  ❌  | ✅  |
| Huge Transform Detection                          |  ❌  | ✅  |
| Severity Color Coding                             |  ✅  | ✅  |
| Three Severity Levels                             |  ✅  | ✅  |
| Scene-Grouped Results                             |  ✅  | ✅  |
| Severity Summary Counts                           |  ✅  | ✅  |
| Scan Duration Display                             |  ✅  | ✅  |
| Select Affected Objects                           |  ✅  | ✅  |
| Ping Affected Objects                             |  ✅  | ✅  |
| Copy Hierarchy Path                               |  ✅  | ✅  |
| Progress Bar + Cancel                             |  ❌  | ✅  |
| Result Search                                     |  ❌  | ✅  |
| Result Filtering by Type                          |  ❌  | ✅  |
| Result Filtering by Severity                      |  ❌  | ✅  |
| Result Filtering by Scene Visibility              |  ❌  | ✅  |
| Diff vs Previous Scan                             |  ❌  | ✅  |
| Batch Review Workflows                            |  ❌  | ✅  |
| Built-In Fixers                                   |  ❌  | ✅  |
| Fix All                                           |  ❌  | ✅  |
| Export CSV Reports                                |  ❌  | ✅  |
| Export JSON Reports                               |  ❌  | ✅  |
| Export HTML Reports                               |  ❌  | ✅  |
| Export TXT Reports                                |  ❌  | ✅  |
| Export Markdown Reports                           |  ❌  | ✅  |
| Reference Finder                                  |  ❌  | ✅  |
| Cached Dependency Map                             |  ❌  | ✅  |
| Dependency Graph View                             |  ❌  | ✅  |
| Unused Asset Cleaner                              |  ❌  | ✅  |
| Empty Folder Cleanup                              |  ❌  | ✅  |
| Scan History                                      |  ❌  | ✅  |
| Health Score and Trends                           |  ❌  | ✅  |
| Build Validation Hook                             |  ❌  | ✅  |
| CLI / CI Batchmode Support                        |  ❌  | ✅  |
| Auto-Scan on Change                               |  ❌  | ✅  |
| Keyboard Shortcuts                                |  ❌  | ✅  |
| In-Editor Notifications                           |  ❌  | ✅  |
| Custom Validation Rules SDK                       |  ❌  | ✅  |
| Custom Issue Fixers SDK                           |  ❌  | ✅  |
| Custom Report Exporters SDK                       |  ❌  | ✅  |
| Large Project Workflow Support                    |  ❌  | ✅  |

## Which Version Is Right for You

**Stay on Lite if:**

- you only need to validate the scenes you are actively editing,
- you want a free tool for basic scene-level reference checks,
- your workflow does not require exports or project-wide review.

**Upgrade to Pro if:**

- you are preparing release builds,
- you work with many scenes, prefabs, ScriptableObjects, materials, or build settings,
- you want broader validation without manually opening assets,
- you need visibility beyond the scenes currently open in the editor,
- you want CI-ready validation, exports, or batch workflows,
- you want built-in fixers, history, trends, or project-cleaning tools,
- you want to extend the validator with studio-specific rules or exporters,
- you need cleaner review workflows for clients, teammates, or QA,
- you want a faster path from scan results to actionable project-wide cleanup.

## Upgrade to Pro

If RefSafe Lite is already helping you, RefSafe Pro is the next step when you want full-project confidence.

Get RefSafe Pro on the Unity Asset Store:

[Upgrade to RefSafe Pro](https://u3d.as/3S3y)

## Support

- **Issues & Bug Reports:** [GitHub Issues](https://github.com/a-p-bhatt/ref-safe-lite/issues)

## License

RefSafe Lite is released under the [MIT License](LICENSE.txt). See `LICENSE.txt` for details.
