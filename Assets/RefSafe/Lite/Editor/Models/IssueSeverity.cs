namespace RefSafe.Lite
{
    /// <summary>
    /// Three-tier severity classification for scan issues.
    /// Used for prioritization, filtering, and color-coded display.
    /// </summary>
    public enum IssueSeverity
    {
        /// <summary>
        /// Critical issues that break functionality (e.g., missing scripts).
        /// Displayed in red.
        /// </summary>
        Critical = 0,

        /// <summary>
        /// Issues that indicate broken references but may not crash the game.
        /// Displayed in amber/yellow.
        /// </summary>
        Warning = 1,

        /// <summary>
        /// Informational issues such as unassigned optional fields.
        /// Displayed in blue.
        /// </summary>
        Info = 2
    }
}
