namespace Shared.Models;

/// <summary>
/// Represents a chat message in a document analysis conversation.
/// </summary>
public class ChatMessage
{
    /// <summary>
    /// Gets or sets the unique message identifier.
    /// </summary>
    public required string MessageId { get; set; }

    /// <summary>
    /// Gets or sets the session identifier this message belongs to.
    /// </summary>
    public required string SessionId { get; set; }

    /// <summary>
    /// Gets or sets the user identifier who sent the message (empty for AI responses).
    /// </summary>
    public string? UserId { get; set; }

    /// <summary>
    /// Gets or sets the role of the message sender.
    /// </summary>
    public required MessageRole Role { get; set; }

    /// <summary>
    /// Gets or sets the message content text.
    /// </summary>
    public required string Content { get; set; }

    /// <summary>
    /// Gets or sets the timestamp when the message was created.
    /// </summary>
    public required DateTime Timestamp { get; set; }

    /// <summary>
    /// Gets or sets references to document sections or data cited in the response.
    /// </summary>
    public List<string>? Citations { get; set; }

    /// <summary>
    /// Gets or sets metadata associated with the message (e.g., confidence scores).
    /// </summary>
    public Dictionary<string, string>? Metadata { get; set; }
}

/// <summary>
/// Represents the role of a message sender in a conversation.
/// </summary>
public enum MessageRole
{
    /// <summary>Message from the user.</summary>
    User,

    /// <summary>Message from the AI assistant.</summary>
    Assistant,

    /// <summary>System message (e.g., session start, status updates).</summary>
    System
}
