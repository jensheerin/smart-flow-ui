namespace Shared.Models;

/// <summary>
/// Represents user feedback on analysis results or chat responses.
/// </summary>
public class Feedback
{
    /// <summary>
    /// Gets or sets the unique feedback identifier.
    /// </summary>
    public required string FeedbackId { get; set; }

    /// <summary>
    /// Gets or sets the session identifier this feedback relates to.
    /// </summary>
    public required string SessionId { get; set; }

    /// <summary>
    /// Gets or sets the user identifier who provided the feedback.
    /// </summary>
    public required string UserId { get; set; }

    /// <summary>
    /// Gets or sets the message identifier if feedback is for a specific chat response.
    /// </summary>
    public string? MessageId { get; set; }

    /// <summary>
    /// Gets or sets the feedback type (thumbs up/down or detailed).
    /// </summary>
    public required FeedbackType FeedbackType { get; set; }

    /// <summary>
    /// Gets or sets the rating (e.g., 1 for thumbs down, 5 for thumbs up).
    /// </summary>
    public int? Rating { get; set; }

    /// <summary>
    /// Gets or sets the detailed text feedback (only for privileged users).
    /// </summary>
    public string? DetailedFeedback { get; set; }

    /// <summary>
    /// Gets or sets the timestamp when the feedback was submitted.
    /// </summary>
    public required DateTime SubmittedTimestamp { get; set; }

    /// <summary>
    /// Gets or sets metadata about the context where feedback was given.
    /// </summary>
    public Dictionary<string, string>? ContextMetadata { get; set; }
}

/// <summary>
/// Represents the type of feedback provided.
/// </summary>
public enum FeedbackType
{
    /// <summary>Simple thumbs up/down rating.</summary>
    ThumbsUpDown,

    /// <summary>Detailed text feedback with explanation.</summary>
    DetailedText,

    /// <summary>Feedback on overall analysis quality.</summary>
    AnalysisQuality,

    /// <summary>Feedback on chat response quality.</summary>
    ChatResponse,

    /// <summary>Feedback on extraction accuracy.</summary>
    ExtractionAccuracy,

    /// <summary>Feedback on calculation correctness.</summary>
    CalculationCorrectness
}
