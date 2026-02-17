namespace RefSafe.Lite
{
    /// <summary>
    /// Maps IssueType to IssueSeverity.
    /// Centralizes severity classification so all rules produce consistent severity levels.
    /// </summary>
    public static class SeverityClassifier
    {
        /// <summary>
        /// Returns the severity level for a given issue type.
        /// </summary>
        /// <param name="type">The type of issue detected.</param>
        /// <returns>The corresponding severity level.</returns>
        public static IssueSeverity Classify(IssueType type)
        {
            switch (type)
            {
                case IssueType.MissingScript:
                    // Missing scripts break prefabs and cause runtime errors
                    return IssueSeverity.Critical;

                case IssueType.MissingReference:
                    // Broken references may cause NullReferenceExceptions
                    return IssueSeverity.Warning;

                default:
                    return IssueSeverity.Info;
            }
        }
    }
}
