namespace RefSafe.Lite
{
    /// <summary>
    /// Categories of issues that the detection engine can identify.
    /// </summary>
    public enum IssueType
    {
        /// <summary>
        /// A MonoBehaviour script file is missing or deleted.
        /// The component slot exists but the script cannot be found.
        /// </summary>
        MissingScript = 0,

        /// <summary>
        /// A serialized object reference field points to a deleted or missing asset.
        /// The field was previously assigned but the target no longer exists.
        /// </summary>
        MissingReference = 1
    }
}
