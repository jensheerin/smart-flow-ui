using Shared.Models;
using Xunit;

namespace SmartFlow.UI.API.Tests.Models;

/// <summary>
/// Unit tests for AnalysisSession model validation and business rules.
/// Tests cover required properties, status transitions, and timestamp validation.
/// </summary>
public class AnalysisSessionTests
{
    [Fact]
    public void AnalysisSession_WithValidData_ShouldCreateSuccessfully()
    {
        // Arrange
        var sessionId = Guid.NewGuid().ToString();
        var userId = "test-user-123";
        var uploadTimestamp = DateTime.UtcNow;

        // Act
        var session = new AnalysisSession
        {
            SessionId = sessionId,
            UserId = userId,
            UploadTimestamp = uploadTimestamp,
            Documents = new List<UploadedDocument>(),
            Status = SessionStatus.Pending
        };

        // Assert
        Assert.NotNull(session);
        Assert.Equal(sessionId, session.SessionId);
        Assert.Equal(userId, session.UserId);
        Assert.Equal(uploadTimestamp, session.UploadTimestamp);
        Assert.Empty(session.Documents);
        Assert.Equal(SessionStatus.Pending, session.Status);
    }

    [Fact]
    public void AnalysisSession_WithNullSessionId_ShouldThrowException()
    {
        // Arrange & Act & Assert
        Assert.Throws<ArgumentNullException>(() => new AnalysisSession
        {
            SessionId = null!,
            UserId = "test-user",
            UploadTimestamp = DateTime.UtcNow,
            Documents = new List<UploadedDocument>(),
            Status = SessionStatus.Pending
        });
    }

    [Fact]
    public void AnalysisSession_WithNullUserId_ShouldThrowException()
    {
        // Arrange & Act & Assert
        Assert.Throws<ArgumentNullException>(() => new AnalysisSession
        {
            SessionId = Guid.NewGuid().ToString(),
            UserId = null!,
            UploadTimestamp = DateTime.UtcNow,
            Documents = new List<UploadedDocument>(),
            Status = SessionStatus.Pending
        });
    }

    [Fact]
    public void AnalysisSession_WithCompletedStatus_ShouldHaveAnalysisResult()
    {
        // Arrange
        var session = new AnalysisSession
        {
            SessionId = Guid.NewGuid().ToString(),
            UserId = "test-user",
            UploadTimestamp = DateTime.UtcNow,
            Documents = new List<UploadedDocument>(),
            Status = SessionStatus.Completed,
            AnalysisResult = new AnalysisResult
            {
                ResultId = Guid.NewGuid().ToString(),
                ExtractedSections = new List<ExtractedSection>(),
                ExtractedSchedules = new List<ExtractedSchedule>(),
                Calculations = new List<Calculation>(),
                ConfidenceScore = 0.95,
                ProcessingDurationSeconds = 12.5,
                ProcessingCompletedTimestamp = DateTime.UtcNow
            }
        };

        // Act & Assert
        Assert.Equal(SessionStatus.Completed, session.Status);
        Assert.NotNull(session.AnalysisResult);
        Assert.NotNull(session.AnalysisResult.ResultId);
    }

    [Fact]
    public void AnalysisSession_StatusTransition_FromPendingToProcessing()
    {
        // Arrange
        var session = new AnalysisSession
        {
            SessionId = Guid.NewGuid().ToString(),
            UserId = "test-user",
            UploadTimestamp = DateTime.UtcNow,
            Documents = new List<UploadedDocument>(),
            Status = SessionStatus.Pending
        };

        // Act
        session.Status = SessionStatus.Processing;

        // Assert
        Assert.Equal(SessionStatus.Processing, session.Status);
    }

    [Fact]
    public void AnalysisSession_StatusTransition_FromProcessingToCompleted()
    {
        // Arrange
        var session = new AnalysisSession
        {
            SessionId = Guid.NewGuid().ToString(),
            UserId = "test-user",
            UploadTimestamp = DateTime.UtcNow,
            Documents = new List<UploadedDocument>(),
            Status = SessionStatus.Processing
        };

        // Act
        session.Status = SessionStatus.Completed;
        session.LastModifiedTimestamp = DateTime.UtcNow;

        // Assert
        Assert.Equal(SessionStatus.Completed, session.Status);
        Assert.NotNull(session.LastModifiedTimestamp);
    }

    [Fact]
    public void AnalysisSession_StatusTransition_FromProcessingToFailed()
    {
        // Arrange
        var session = new AnalysisSession
        {
            SessionId = Guid.NewGuid().ToString(),
            UserId = "test-user",
            UploadTimestamp = DateTime.UtcNow,
            Documents = new List<UploadedDocument>(),
            Status = SessionStatus.Processing
        };

        // Act
        session.Status = SessionStatus.Failed;
        session.LastModifiedTimestamp = DateTime.UtcNow;

        // Assert
        Assert.Equal(SessionStatus.Failed, session.Status);
        Assert.NotNull(session.LastModifiedTimestamp);
    }

    [Fact]
    public void AnalysisSession_WithMultipleDocuments_ShouldMaintainList()
    {
        // Arrange
        var doc1 = new UploadedDocument
        {
            DocumentId = Guid.NewGuid().ToString(),
            FileName = "spec1.pdf",
            FileSizeBytes = 1024000,
            UploadTimestamp = DateTime.UtcNow,
            StorageLocationReference = "blob://container/spec1.pdf",
            DocumentType = DocumentType.MechanicalSpec,
            ProcessingStatus = DocumentProcessingStatus.New
        };

        var doc2 = new UploadedDocument
        {
            DocumentId = Guid.NewGuid().ToString(),
            FileName = "spec2.pdf",
            FileSizeBytes = 2048000,
            UploadTimestamp = DateTime.UtcNow,
            StorageLocationReference = "blob://container/spec2.pdf",
            DocumentType = DocumentType.MechanicalSpec,
            ProcessingStatus = DocumentProcessingStatus.New
        };

        // Act
        var session = new AnalysisSession
        {
            SessionId = Guid.NewGuid().ToString(),
            UserId = "test-user",
            UploadTimestamp = DateTime.UtcNow,
            Documents = new List<UploadedDocument> { doc1, doc2 },
            Status = SessionStatus.Pending
        };

        // Assert
        Assert.Equal(2, session.Documents.Count);
        Assert.Contains(doc1, session.Documents);
        Assert.Contains(doc2, session.Documents);
    }

    [Fact]
    public void AnalysisSession_LastModifiedTimestamp_ShouldBeAfterOrEqualUploadTimestamp()
    {
        // Arrange
        var uploadTime = DateTime.UtcNow;
        var session = new AnalysisSession
        {
            SessionId = Guid.NewGuid().ToString(),
            UserId = "test-user",
            UploadTimestamp = uploadTime,
            Documents = new List<UploadedDocument>(),
            Status = SessionStatus.Pending
        };

        // Act
        Thread.Sleep(10); // Ensure time difference
        session.LastModifiedTimestamp = DateTime.UtcNow;

        // Assert
        Assert.NotNull(session.LastModifiedTimestamp);
        Assert.True(session.LastModifiedTimestamp >= session.UploadTimestamp);
    }

    [Theory]
    [InlineData(SessionStatus.Uploading)]
    [InlineData(SessionStatus.Pending)]
    [InlineData(SessionStatus.Processing)]
    [InlineData(SessionStatus.Completed)]
    [InlineData(SessionStatus.Failed)]
    [InlineData(SessionStatus.Cancelled)]
    public void AnalysisSession_AllStatusValues_ShouldBeValid(SessionStatus status)
    {
        // Arrange & Act
        var session = new AnalysisSession
        {
            SessionId = Guid.NewGuid().ToString(),
            UserId = "test-user",
            UploadTimestamp = DateTime.UtcNow,
            Documents = new List<UploadedDocument>(),
            Status = status
        };

        // Assert
        Assert.Equal(status, session.Status);
    }
}
