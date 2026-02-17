namespace RefSafe.Lite
{
    /// <summary>
    /// Tracks the current state of a scan session.
    /// Used by the toolbar, footer, and window to display appropriate status.
    /// </summary>
    public sealed class ScanState
    {
        /// <summary>True while a scan is actively running.</summary>
        public bool IsScanning { get; private set; }

        /// <summary>True after at least one scan has completed (even if results were cleared).</summary>
        public bool HasScanned { get; private set; }

        /// <summary>Duration of the last completed scan in seconds.</summary>
        public float ScanDurationSeconds { get; private set; }

        /// <summary>Number of scenes scanned in the last completed scan.</summary>
        public int ScenesScanned { get; private set; }

        /// <summary>Mark scan as started. Resets timing.</summary>
        public void BeginScan()
        {
            IsScanning = true;
            HasScanned = false;
            ScanDurationSeconds = 0f;
            ScenesScanned = 0;
        }

        /// <summary>Mark scan as completed with results.</summary>
        public void EndScan(float durationSeconds, int scenesScanned)
        {
            IsScanning = false;
            HasScanned = true;
            ScanDurationSeconds = durationSeconds;
            ScenesScanned = scenesScanned;
        }

        /// <summary>Reset to initial state (no scan performed).</summary>
        public void Reset()
        {
            IsScanning = false;
            HasScanned = false;
            ScanDurationSeconds = 0f;
            ScenesScanned = 0;
        }
    }
}
