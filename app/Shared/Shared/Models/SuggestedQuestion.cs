namespace Shared.Models;

/// <summary>
/// Represents an AI-generated suggested follow-up question for the user.
/// </summary>
public class SuggestedQuestion
{
    /// <summary>
    /// Gets or sets the unique suggestion identifier.
    /// </summary>
    public required string SuggestionId { get; set; }

    /// <summary>
    /// Gets or sets the session identifier this suggestion belongs to.
    /// </summary>
    public required string SessionId { get; set; }

    /// <summary>
    /// Gets or sets the suggested question text.
    /// </summary>
    public required string QuestionText { get; set; }

    /// <summary>
    /// Gets or sets the category or topic of the question.
    /// </summary>
    public required QuestionCategory Category { get; set; }

    /// <summary>
    /// Gets or sets the relevance score (0.0 to 1.0) based on analysis context.
    /// </summary>
    public required double RelevanceScore { get; set; }

    /// <summary>
    /// Gets or sets the timestamp when the suggestion was generated.
    /// </summary>
    public required DateTime GeneratedTimestamp { get; set; }

    /// <summary>
    /// Gets or sets whether this suggestion has been used by the user.
    /// </summary>
    public bool IsUsed { get; set; }
}

/// <summary>
/// Represents the category of a suggested question.
/// </summary>
public enum QuestionCategory
{
    /// <summary>Questions about specifications and parameters.</summary>
    Specifications,

    /// <summary>Questions about calculations and validations.</summary>
    Calculations,

    /// <summary>Questions about schedules and data tables.</summary>
    Schedules,

    /// <summary>Questions about requirements and compliance.</summary>
    Requirements,

    /// <summary>Questions about sections or document content.</summary>
    Content,

    /// <summary>General or uncategorized questions.</summary>
    General
}
