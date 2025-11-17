namespace Shared.Models;

/// <summary>
/// Represents an uploaded PDF document in an analysis session.
/// </summary>
public class UploadedDocument
{
    /// <summary>
    /// Gets or sets the unique document identifier.
    /// </summary>
    public required string DocumentId { get; set; }

    /// <summary>
    /// Gets or sets the original file name.
    /// </summary>
    public required string FileName { get; set; }

    /// <summary>
    /// Gets or sets the file size in bytes.
    /// </summary>
    public required long FileSizeBytes { get; set; }

    /// <summary>
    /// Gets or sets the timestamp when the document was uploaded.
    /// </summary>
    public required DateTime UploadTimestamp { get; set; }

    /// <summary>
    /// Gets or sets the Azure Blob Storage reference or URI.
    /// </summary>
    public required string StorageLocationReference { get; set; }

    /// <summary>
    /// Gets or sets the document type classification.
    /// </summary>
    public required DocumentType DocumentType { get; set; }

    /// <summary>
    /// Gets or sets the current processing status of the document.
    /// </summary>
    public required DocumentProcessingStatus ProcessingStatus { get; set; }

    /// <summary>
    /// Gets or sets the error message if processing failed.
    /// </summary>
    public string? ErrorMessage { get; set; }
}

/// <summary>
/// Represents the type classification of a document.
/// </summary>
public enum DocumentType
{
    /// <summary>Mechanical specification document.</summary>
    MechanicalSpec,

    /// <summary>Plan drawing document.</summary>
    PlanDrawing,

    /// <summary>Other or unclassified document type.</summary>
    Other
}
