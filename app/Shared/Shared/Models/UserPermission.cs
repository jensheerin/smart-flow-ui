namespace Shared.Models;

/// <summary>
/// Represents user permissions for accessing specific features.
/// </summary>
public class UserPermission
{
    /// <summary>
    /// Gets or sets the user identifier.
    /// </summary>
    public required string UserId { get; set; }

    /// <summary>
    /// Gets or sets whether the user can provide detailed text feedback.
    /// </summary>
    public required bool CanProvideDetailedFeedback { get; set; }

    /// <summary>
    /// Gets or sets whether the user can export analysis results.
    /// </summary>
    public required bool CanExportResults { get; set; }

    /// <summary>
    /// Gets or sets whether the user can access analysis history.
    /// </summary>
    public required bool CanAccessHistory { get; set; }

    /// <summary>
    /// Gets or sets whether the user can delete analysis sessions.
    /// </summary>
    public required bool CanDeleteSessions { get; set; }

    /// <summary>
    /// Gets or sets additional custom permissions as key-value pairs.
    /// </summary>
    public Dictionary<string, bool>? CustomPermissions { get; set; }

    /// <summary>
    /// Gets or sets the timestamp when permissions were last updated.
    /// </summary>
    public DateTime? LastUpdatedTimestamp { get; set; }
}
