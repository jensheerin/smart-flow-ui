namespace Shared.Models;

/// <summary>
/// Represents a section extracted from a document during analysis.
/// </summary>
public class ExtractedSection
{
    /// <summary>
    /// Gets or sets the unique section identifier.
    /// </summary>
    public required string SectionId { get; set; }

    /// <summary>
    /// Gets or sets the section title or heading.
    /// </summary>
    public required string SectionTitle { get; set; }

    /// <summary>
    /// Gets or sets the extracted content text.
    /// </summary>
    public required string ContentText { get; set; }

    /// <summary>
    /// Gets or sets the page numbers where this section appears.
    /// </summary>
    public required List<int> PageNumbers { get; set; } = new();

    /// <summary>
    /// Gets or sets the classification type of this section.
    /// </summary>
    public required SectionClassificationType ClassificationType { get; set; }

    /// <summary>
    /// Gets or sets the relevance score (0.0 to 1.0) indicating importance.
    /// </summary>
    public required double RelevanceScore { get; set; }

    /// <summary>
    /// Gets or sets the source document identifier.
    /// </summary>
    public required string SourceDocumentId { get; set; }
}

/// <summary>
/// Represents the classification type of an extracted section.
/// </summary>
public enum SectionClassificationType
{
    /// <summary>General overview or introduction.</summary>
    Overview,

    /// <summary>Technical specifications.</summary>
    Specifications,

    /// <summary>Engineering requirements.</summary>
    Requirements,

    /// <summary>Safety information.</summary>
    Safety,

    /// <summary>Installation instructions.</summary>
    Installation,

    /// <summary>Maintenance information.</summary>
    Maintenance,

    /// <summary>Other or unclassified section.</summary>
    Other
}
