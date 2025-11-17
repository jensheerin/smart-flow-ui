namespace Shared.Models;

/// <summary>
/// Represents structured schedule data extracted from a document.
/// </summary>
public class ExtractedSchedule
{
    /// <summary>
    /// Gets or sets the unique schedule identifier.
    /// </summary>
    public required string ScheduleId { get; set; }

    /// <summary>
    /// Gets or sets the schedule name or title.
    /// </summary>
    public required string ScheduleName { get; set; }

    /// <summary>
    /// Gets or sets the list of column headers.
    /// </summary>
    public required List<string> ColumnHeaders { get; set; } = new();

    /// <summary>
    /// Gets or sets the row data as a list of dictionaries (column name to value).
    /// </summary>
    public required List<Dictionary<string, string>> RowData { get; set; } = new();

    /// <summary>
    /// Gets or sets the source document identifier.
    /// </summary>
    public required string SourceDocumentId { get; set; }

    /// <summary>
    /// Gets or sets the extraction confidence score (0.0 to 1.0).
    /// </summary>
    public required double ExtractionConfidence { get; set; }

    /// <summary>
    /// Gets or sets the page number where the schedule was found.
    /// </summary>
    public int? PageNumber { get; set; }

    /// <summary>
    /// Gets or sets any notes or warnings about the extraction.
    /// </summary>
    public string? ExtractionNotes { get; set; }
}
