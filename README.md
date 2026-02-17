# RefSafe Lite — Missing Script & Reference Scanner for Unity

Detect missing scripts and broken references in the currently opened Unity scene — for free.

RefSafe Lite scans the active scene and helps you quickly identify GameObjects that contain:

* Missing MonoBehaviour scripts
* Broken serialized object references

No project-wide scanning. No prefab scanning. Just fast scene validation.

---

## Features

* Scan **currently opened scene only**
* Detect **missing MonoBehaviour scripts**
* Detect **broken object references** in components
* Clear issue listing window
* **Select & Ping** to jump to affected GameObjects instantly
* Lightweight & editor-safe
* Supports Unity **2021 LTS, 2022 LTS, 2023 LTS, Unity 6**

---

## What Lite Does

RefSafe Lite scans:

* Active scene hierarchy
* All GameObjects in the opened scene
* All attached components for missing references

It reports issues so you can manually fix them.

> Lite does NOT auto-fix issues.
> Lite does NOT scan prefabs.
> Lite does NOT scan other scenes.

---

## Installation

### From Unity Asset Store (Recommended)

1. Open the Unity Asset Store.
2. Search for **RefSafe Lite – Missing Reference Scanner for Unity**.
3. Import the package into your project.
4. Go to **Tools > RefSafe > Lite** to open the window.

### From GitHub

1. Download or clone this repository.
2. Copy the `Assets/RefSafe/Lite` folder into your Unity project's `Assets` directory.
3. Go to **Tools > RefSafe > Lite** to open the window.

---

## Usage

1. Open the scene you want to scan.
2. Go to **Tools > RefSafe > Lite**.
3. Click **Scan Scene**.
4. Review the detected issues.
5. Click any result to select and ping the affected GameObject.

---

## Lite vs Pro

| Feature                            | Lite (Free) | Pro (Paid) |
| ---------------------------------- | :---------: | :--------: |
| Scan Currently Open Scene          |      ✅      |      ✅     |
| Missing Script Detection           |      ✅      |      ✅     |
| Broken Reference Detection         |      ✅      |      ✅     |
| Select & Ping Objects              |      ✅      |      ✅     |
| Scan All Scenes                    |      ❌      |      ✅     |
| Scan Prefabs                       |      ❌      |      ✅     |
| Scan ScriptableObjects             |      ❌      |      ✅     |
| Project-Wide Scan                  |      ❌      |      ✅     |
| Progress Bar + Cancel              |      ❌      |      ✅     |
| Result Filtering                   |      ❌      |      ✅     |
| Export Reports (CSV/JSON/HTML/TXT) |      ❌      |      ✅     |

Need project-wide scanning and prefab validation?
Check out **RefSafe Pro** on the Unity Asset Store.

---

## Support

* Issues & Bug Reports: GitHub Issues (Lite & Pro)

---

## License

RefSafe Lite is released under the MIT License.
See the LICENSE.txt file for details.
