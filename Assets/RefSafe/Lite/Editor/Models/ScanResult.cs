using System.Collections.Generic;
using System.Linq;

namespace RefSafe.Lite
{
    /// <summary>
    /// Aggregated container for all issues found during a scan session.
    /// Provides count helpers, severity/type filtering, and grouping methods
    /// for the results panel.
    /// </summary>
    public sealed class ScanResult
    {
        private readonly List<ScanIssue> _issues = new List<ScanIssue>();

        // ── Read-Only Access ──────────────────────────────────────────

        /// <summary>All issues found in this scan.</summary>
        public IReadOnlyList<ScanIssue> Issues => _issues;

        /// <summary>Total number of issues.</summary>
        public int TotalIssues => _issues.Count;

        // ── Severity Counts ───────────────────────────────────────────

        public int CriticalCount => _issues.Count(i => i.Severity == IssueSeverity.Critical);
        public int WarningCount => _issues.Count(i => i.Severity == IssueSeverity.Warning);
        public int InfoCount => _issues.Count(i => i.Severity == IssueSeverity.Info);

        // ── Type Counts ───────────────────────────────────────────────

        public int MissingScriptCount => _issues.Count(i => i.Type == IssueType.MissingScript);
        public int MissingReferenceCount => _issues.Count(i => i.Type == IssueType.MissingReference);

        // ── Mutation ──────────────────────────────────────────────────

        public void AddIssue(ScanIssue issue)
        {
            if (issue != null)
                _issues.Add(issue);
        }

        public void AddIssues(IEnumerable<ScanIssue> issues)
        {
            if (issues != null)
                _issues.AddRange(issues);
        }

        public void Clear()
        {
            _issues.Clear();
        }

        // ── Filtering ─────────────────────────────────────────────────

        public IEnumerable<ScanIssue> GetBySeverity(IssueSeverity severity)
        {
            return _issues.Where(i => i.Severity == severity);
        }

        public IEnumerable<ScanIssue> GetByType(IssueType type)
        {
            return _issues.Where(i => i.Type == type);
        }

        // ── Grouping ──────────────────────────────────────────────────

        /// <summary>Groups issues by scene name for scene-wise results listing.</summary>
        public IEnumerable<IGrouping<string, ScanIssue>> GroupByScene()
        {
            return _issues.GroupBy(i => i.Location.SceneName);
        }

        /// <summary>Groups issues by hierarchy path for per-object grouping.</summary>
        public IEnumerable<IGrouping<string, ScanIssue>> GroupByGameObject()
        {
            return _issues.GroupBy(i => i.Location.HierarchyPath);
        }

        // ── Summary ───────────────────────────────────────────────────

        /// <summary>Returns true if the scan found any issues.</summary>
        public bool HasIssues => _issues.Count > 0;

        /// <summary>Returns a human-readable summary string.</summary>
        public override string ToString()
        {
            return $"{TotalIssues} issues ({CriticalCount} critical, {WarningCount} warnings, {InfoCount} info)";
        }
    }
}
