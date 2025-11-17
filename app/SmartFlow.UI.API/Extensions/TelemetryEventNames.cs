namespace SmartFlow.UI.API.Extensions;

/// <summary>
/// Application Insights custom event names for mechanical document analysis operations.
/// Use these constants when logging telemetry events via ILogger or TelemetryClient.
/// </summary>
/// <remarks>
/// Task: T023 - Configure Application Insights custom events for document analysis operations
/// </remarks>
public static class TelemetryEventNames
{
    /// <summary>
    /// Logged when document upload process starts.
    /// Properties: SessionId, UserId, DocumentCount
    /// </summary>
    public const string DocumentAnalysis_Upload_Started = "DocumentAnalysis.Upload.Started";

    /// <summary>
    /// Logged when document upload process completes successfully.
    /// Properties: SessionId, UserId, DocumentCount, TotalSizeBytes, DurationMs
    /// </summary>
    public const string DocumentAnalysis_Upload_Completed = "DocumentAnalysis.Upload.Completed";

    /// <summary>
    /// Logged when document upload process fails.
    /// Properties: SessionId, UserId, DocumentCount, ErrorMessage
    /// </summary>
    public const string DocumentAnalysis_Upload_Failed = "DocumentAnalysis.Upload.Failed";

    /// <summary>
    /// Logged when Document Agent processing starts.
    /// Properties: SessionId, UserId, DocumentCount
    /// </summary>
    public const string DocumentAnalysis_Processing_Started = "DocumentAnalysis.Processing.Started";

    /// <summary>
    /// Logged when Document Agent processing completes successfully.
    /// Properties: SessionId, UserId, ExtractedSectionsCount, ExtractedSchedulesCount, CalculationsCount, ProcessingDurationSeconds, ConfidenceScore
    /// </summary>
    public const string DocumentAnalysis_Processing_Completed = "DocumentAnalysis.Processing.Completed";

    /// <summary>
    /// Logged when Document Agent processing fails.
    /// Properties: SessionId, UserId, ErrorMessage, ProcessingDurationSeconds
    /// </summary>
    public const string DocumentAnalysis_Processing_Failed = "DocumentAnalysis.Processing.Failed";

    /// <summary>
    /// Logged when user sends chat message.
    /// Properties: SessionId, UserId, MessageId, MessageLength
    /// </summary>
    public const string DocumentAnalysis_Chat_MessageSent = "DocumentAnalysis.Chat.MessageSent";

    /// <summary>
    /// Logged when AI assistant responds to chat message.
    /// Properties: SessionId, UserId, MessageId, ResponseLength, CitationsCount, DurationMs
    /// </summary>
    public const string DocumentAnalysis_Chat_ResponseGenerated = "DocumentAnalysis.Chat.ResponseGenerated";

    /// <summary>
    /// Logged when user submits feedback.
    /// Properties: SessionId, UserId, FeedbackType, MessageId (optional), Rating (optional)
    /// </summary>
    public const string DocumentAnalysis_Feedback_Submitted = "DocumentAnalysis.Feedback.Submitted";

    /// <summary>
    /// Logged when user requests export of analysis results.
    /// Properties: SessionId, UserId, ExportFormat
    /// </summary>
    public const string DocumentAnalysis_Export_Requested = "DocumentAnalysis.Export.Requested";

    /// <summary>
    /// Logged when export completes successfully.
    /// Properties: SessionId, UserId, ExportFormat, FileSizeBytes, DurationMs
    /// </summary>
    public const string DocumentAnalysis_Export_Completed = "DocumentAnalysis.Export.Completed";

    /// <summary>
    /// Logged when export fails.
    /// Properties: SessionId, UserId, ExportFormat, ErrorMessage
    /// </summary>
    public const string DocumentAnalysis_Export_Failed = "DocumentAnalysis.Export.Failed";

    /// <summary>
    /// Logged when user accesses session history.
    /// Properties: UserId, SessionCount
    /// </summary>
    public const string DocumentAnalysis_History_Accessed = "DocumentAnalysis.History.Accessed";

    /// <summary>
    /// Logged when user deletes an analysis session.
    /// Properties: SessionId, UserId
    /// </summary>
    public const string DocumentAnalysis_Session_Deleted = "DocumentAnalysis.Session.Deleted";
}
