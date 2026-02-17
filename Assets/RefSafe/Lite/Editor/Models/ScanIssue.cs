namespace RefSafe.Lite
{
    /// <summary>
    /// Represents a single issue found during a scan.
    /// Each issue has a type, severity level, human-readable message,
    /// and a location describing exactly where the issue was found.
    /// </summary>
    public sealed class ScanIssue
    {
        /// <summary>Category of the issue (MissingScript, MissingReference, etc.).</summary>
        public IssueType Type { get; }

        /// <summary>Severity level (Critical, Warning, Info) for prioritization and color coding.</summary>
        public IssueSeverity Severity { get; }

        /// <summary>Human-readable description of the issue.</summary>
        public string Message { get; }

        /// <summary>Detailed location information for navigation and display.</summary>
        public ReferenceLocation Location { get; }

        public ScanIssue(
            IssueType type,
            IssueSeverity severity,
            string message,
            ReferenceLocation location)
        {
            Type = type;
            Severity = severity;
            Message = message ?? string.Empty;
            Location = location;
        }

        public override string ToString()
        {
            return $"[{Severity}] {Type}: {Message} at {Location}";
        }
    }
}
