using UnityEngine;

namespace RefSafe.Lite
{
    /// <summary>
    /// Color palette and visual styling for severity levels.
    /// Provides foreground colors, background tints, icons, and labels
    /// for consistent severity-coded display across all UI components.
    ///
    /// Color scheme:
    ///   Critical → 🔴 Red
    ///   Warning  → 🟡 Amber
    ///   Info     → 🔵 Blue
    /// </summary>
    public static class SeverityColors
    {
        // ── Critical (Red) ────────────────────────────────────────────

        public static readonly Color CriticalForeground  = new Color(0.95f, 0.25f, 0.25f, 1.0f);
        public static readonly Color CriticalBackground  = new Color(0.95f, 0.25f, 0.25f, 0.12f);

        // ── Warning (Amber) ───────────────────────────────────────────

        public static readonly Color WarningForeground   = new Color(0.95f, 0.72f, 0.10f, 1.0f);
        public static readonly Color WarningBackground   = new Color(0.95f, 0.72f, 0.10f, 0.12f);

        // ── Info (Blue) ───────────────────────────────────────────────

        public static readonly Color InfoForeground      = new Color(0.30f, 0.60f, 0.90f, 1.0f);
        public static readonly Color InfoBackground      = new Color(0.30f, 0.60f, 0.90f, 0.12f);

        // ── Lookup Methods ────────────────────────────────────────────

        /// <summary>Returns the foreground (text/icon) color for a severity level.</summary>
        public static Color GetForeground(IssueSeverity severity)
        {
            switch (severity)
            {
                case IssueSeverity.Critical: return CriticalForeground;
                case IssueSeverity.Warning:  return WarningForeground;
                case IssueSeverity.Info:     return InfoForeground;
                default:                     return Color.white;
            }
        }

        /// <summary>Returns the background tint color for a severity level.</summary>
        public static Color GetBackground(IssueSeverity severity)
        {
            switch (severity)
            {
                case IssueSeverity.Critical: return CriticalBackground;
                case IssueSeverity.Warning:  return WarningBackground;
                case IssueSeverity.Info:     return InfoBackground;
                default:                     return Color.clear;
            }
        }

        /// <summary>Returns a colored dot icon string for a severity level.</summary>
        public static string GetIcon(IssueSeverity severity)
        {
            switch (severity)
            {
                case IssueSeverity.Critical: return "●";
                case IssueSeverity.Warning:  return "●";
                case IssueSeverity.Info:     return "●";
                default:                     return "○";
            }
        }

        /// <summary>Returns an uppercase label for a severity level.</summary>
        public static string GetLabel(IssueSeverity severity)
        {
            switch (severity)
            {
                case IssueSeverity.Critical: return "CRITICAL";
                case IssueSeverity.Warning:  return "WARNING";
                case IssueSeverity.Info:     return "INFO";
                default:                     return "UNKNOWN";
            }
        }

        /// <summary>Returns a short tag string like "[CRITICAL]" for log output.</summary>
        public static string GetTag(IssueSeverity severity)
        {
            return $"[{GetLabel(severity)}]";
        }
    }
}
