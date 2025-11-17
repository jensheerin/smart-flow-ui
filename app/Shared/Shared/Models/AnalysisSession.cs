namespace Shared.Models;

/// <summary>
/// Represents a document analysis session containing uploaded documents and analysis results.
/// </summary>
public class AnalysisSession
{
    /// <summary>
    /// Gets or sets the unique session identifier.
    /// </summary>
    public required string SessionId { get; set; }

    /// <summary>
    /// Gets or sets the user identifier who created this session.
    /// </summary>
    public required string UserId { get; set; }

    /// <summary>
    /// Gets or sets the timestamp when the session was created.
    /// </summary>
    public required DateTime UploadTimestamp { get; set; }

    /// <summary>
    /// Gets or sets the list of uploaded document references.
    /// </summary>
    public required List<UploadedDocument> Documents { get; set; } = new();

    /// <summary>
    /// Gets or sets the current status of the analysis session.
    /// </summary>
    public required SessionStatus Status { get; set; }

    /// <summary>
    /// Gets or sets the analysis result containing extracted data and insights.
    /// </summary>
    public AnalysisResult? AnalysisResult { get; set; }

    /// <summary>
    /// Gets or sets the summary text describing key findings.
    /// </summary>
    public string? SummaryResults { get; set; }

    /// <summary>
    /// Gets or sets the timestamp when the session was last modified.
    /// </summary>
    public DateTime? LastModifiedTimestamp { get; set; }
}

/// <summary>
/// Represents the possible states of an analysis session.
/// </summary>
public enum SessionStatus
{
    /// <summary>Documents are being uploaded.</summary>
    Uploading,

    /// <summary>Documents have been uploaded and awaiting processing.</summary>
    Pending,

    /// <summary>Analysis is currently in progress.</summary>
    Processing,

    /// <summary>Analysis completed successfully.</summary>
    Completed,

    /// <summary>Analysis failed with errors.</summary>
    Failed,

    /// <summary>Analysis was cancelled by user.</summary>
    Cancelled
}
