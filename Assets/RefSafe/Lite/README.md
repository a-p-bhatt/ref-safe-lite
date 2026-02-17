# RefSafe Lite — Reference Validator for Unity

Detect missing references, broken prefabs, and missing scripts in your Unity projects — for free.

## Features

- Scan **open scenes** for broken references
- Detect **missing scripts** on GameObjects
- Detect **missing object references** in components
- **Severity color coding** — Critical, Warning, Info
- Scene-wise results listing
- **Select & Ping** to jump to affected GameObjects instantly
- Supports Unity **2021 LTS, 2022 LTS, 2023 LTS, Unity 6**

## Installation

### From Unity Asset Store (Recommended)

1. Open the [Unity Asset Store](https://assetstore.unity.com/) and search for **RefSafe Lite**.
2. Import the package into your Unity project.
3. Go to **Tools > RefSafe > Lite** to launch the window.

### From GitHub

1. Download or clone this repository.
2. Copy the `Assets/RefSafe/Lite` folder into your Unity project's `Assets` directory.
3. Go to **Tools > RefSafe > Lite** to launch the window.

## Usage

1. Open the scenes you want to scan in Unity.
2. Go to **Tools > RefSafe > Lite**.
3. Click the **Scan** button.
4. Review the results — click on any issue to select and ping the affected GameObject.

## Lite vs Pro

| Feature                     | Lite (Free) | Pro (Paid) |
| --------------------------- | :---------: | :--------: |
| Scan Open Scenes            |      ✅      |     ✅      |
| Missing Script Detection    |      ✅      |     ✅      |
| Missing Reference Detection |      ✅      |     ✅      |
| Severity Color Coding       |      ✅      |     ✅      |
| Select & Ping Objects       |      ✅      |     ✅      |
| Scan All Scenes             |      ❌      |     ✅      |
| Scan Prefabs                |      ❌      |     ✅      |
| Scan ScriptableObjects      |      ❌      |     ✅      |
| Project-Wide Scan           |      ❌      |     ✅      |
| Progress Bar + Cancel       |      ❌      |     ✅      |
| Result Filtering            |      ❌      |     ✅      |
| Export Reports (CSV/JSON/HTML/TXT) | ❌   |     ✅      |

Need project-wide scanning, prefab validation, and export reports? Check out [RefSafe Pro](https://assetstore.unity.com/) on the Unity Asset Store.

## Support

- **Issues & Bug Reports:** [GitHub Issues](https://github.com/AkashAkki/refsafe-lite/issues) (for both Lite and Pro)
- **Email:** akashakki522@gmail.com

## License

RefSafe Lite is released under the [MIT License](LICENSE.txt). See the LICENSE.txt file for details.
