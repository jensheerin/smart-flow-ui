namespace Shared.Models;

/// <summary>
/// Represents the output from Document Agent processing containing extracted data and analysis results.
/// </summary>
public class AnalysisResult
{
    /// <summary>
    /// Gets or sets the unique result identifier.
    /// </summary>
    public required string ResultId { get; set; }

    /// <summary>
    /// Gets or sets the list of extracted sections from the documents.
    /// </summary>
    public required List<ExtractedSection> ExtractedSections { get; set; } = new();

    /// <summary>
    /// Gets or sets the list of extracted schedules from the documents.
    /// </summary>
    public required List<ExtractedSchedule> ExtractedSchedules { get; set; } = new();

    /// <summary>
    /// Gets or sets the list of calculations performed based on extracted parameters.
    /// </summary>
    public required List<Calculation> Calculations { get; set; } = new();

    /// <summary>
    /// Gets or sets the validation findings from parameter validation.
    /// </summary>
    public List<string>? ValidationFindings { get; set; }

    /// <summary>
    /// Gets or sets the overall confidence score (0.0 to 1.0) of the analysis.
    /// </summary>
    public required double ConfidenceScore { get; set; }

    /// <summary>
    /// Gets or sets the processing duration in seconds.
    /// </summary>
    public required double ProcessingDurationSeconds { get; set; }

    /// <summary>
    /// Gets or sets the timestamp when processing completed.
    /// </summary>
    public required DateTime ProcessingCompletedTimestamp { get; set; }

    /// <summary>
    /// Gets or sets any warnings generated during processing.
    /// </summary>
    public List<string>? Warnings { get; set; }
}
