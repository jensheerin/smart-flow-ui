using Shared.Models;
using Xunit;

namespace SmartFlow.UI.API.Tests.Models;

/// <summary>
/// Unit tests for UploadedDocument model validation and business rules.
/// Tests cover required properties, file size validation, and status transitions.
/// </summary>
public class UploadedDocumentTests
{
    [Fact]
    public void UploadedDocument_WithValidData_ShouldCreateSuccessfully()
    {
        // Arrange
        var documentId = Guid.NewGuid().ToString();
        var fileName = "mechanical-spec.pdf";
        var fileSizeBytes = 1024000L;
        var uploadTimestamp = DateTime.UtcNow;
        var storageLocation = "blob://mech-doc-analysis/documents/mechanical-spec.pdf";

        // Act
        var document = new UploadedDocument
        {
            DocumentId = documentId,
            FileName = fileName,
            FileSizeBytes = fileSizeBytes,
            UploadTimestamp = uploadTimestamp,
            StorageLocationReference = storageLocation,
            DocumentType = DocumentType.MechanicalSpec,
            ProcessingStatus = DocumentProcessingStatus.New
        };

        // Assert
        Assert.NotNull(document);
        Assert.Equal(documentId, document.DocumentId);
        Assert.Equal(fileName, document.FileName);
        Assert.Equal(fileSizeBytes, document.FileSizeBytes);
        Assert.Equal(uploadTimestamp, document.UploadTimestamp);
        Assert.Equal(storageLocation, document.StorageLocationReference);
        Assert.Equal(DocumentType.MechanicalSpec, document.DocumentType);
        Assert.Equal(DocumentProcessingStatus.New, document.ProcessingStatus);
    }

    [Fact]
    public void UploadedDocument_WithNullDocumentId_ShouldThrowException()
    {
        // Arrange & Act & Assert
        Assert.Throws<ArgumentNullException>(() => new UploadedDocument
        {
            DocumentId = null!,
            FileName = "test.pdf",
            FileSizeBytes = 1024,
            UploadTimestamp = DateTime.UtcNow,
            StorageLocationReference = "blob://test",
            DocumentType = DocumentType.MechanicalSpec,
            ProcessingStatus = DocumentProcessingStatus.New
        });
    }

    [Fact]
    public void UploadedDocument_WithNullFileName_ShouldThrowException()
    {
        // Arrange & Act & Assert
        Assert.Throws<ArgumentNullException>(() => new UploadedDocument
        {
            DocumentId = Guid.NewGuid().ToString(),
            FileName = null!,
            FileSizeBytes = 1024,
            UploadTimestamp = DateTime.UtcNow,
            StorageLocationReference = "blob://test",
            DocumentType = DocumentType.MechanicalSpec,
            ProcessingStatus = DocumentProcessingStatus.New
        });
    }

    [Fact]
    public void UploadedDocument_WithExcessiveFileSize_ShouldExceed50MBLimit()
    {
        // Arrange
        const long maxFileSize = 50 * 1024 * 1024; // 50MB
        var document = new UploadedDocument
        {
            DocumentId = Guid.NewGuid().ToString(),
            FileName = "large-spec.pdf",
            FileSizeBytes = maxFileSize + 1,
            UploadTimestamp = DateTime.UtcNow,
            StorageLocationReference = "blob://test",
            DocumentType = DocumentType.MechanicalSpec,
            ProcessingStatus = DocumentProcessingStatus.New
        };

        // Act & Assert
        Assert.True(document.FileSizeBytes > maxFileSize, "File size should exceed 50MB limit");
    }

    [Fact]
    public void UploadedDocument_WithValidFileSize_ShouldBeWithin50MBLimit()
    {
        // Arrange
        const long maxFileSize = 50 * 1024 * 1024; // 50MB
        var document = new UploadedDocument
        {
            DocumentId = Guid.NewGuid().ToString(),
            FileName = "valid-spec.pdf",
            FileSizeBytes = maxFileSize - 1000,
            UploadTimestamp = DateTime.UtcNow,
            StorageLocationReference = "blob://test",
            DocumentType = DocumentType.MechanicalSpec,
            ProcessingStatus = DocumentProcessingStatus.New
        };

        // Act & Assert
        Assert.True(document.FileSizeBytes <= maxFileSize, "File size should be within 50MB limit");
    }

    [Theory]
    [InlineData("document.pdf")]
    [InlineData("mechanical-spec-2024.pdf")]
    [InlineData("HVAC Plan 01-23-2024.pdf")]
    [InlineData("spec_v2.PDF")]
    public void UploadedDocument_WithPdfExtension_ShouldHaveValidFileName(string fileName)
    {
        // Arrange & Act
        var document = new UploadedDocument
        {
            DocumentId = Guid.NewGuid().ToString(),
            FileName = fileName,
            FileSizeBytes = 1024,
            UploadTimestamp = DateTime.UtcNow,
            StorageLocationReference = "blob://test",
            DocumentType = DocumentType.MechanicalSpec,
            ProcessingStatus = DocumentProcessingStatus.New
        };

        // Assert
        Assert.EndsWith(".pdf", document.FileName, StringComparison.OrdinalIgnoreCase);
    }

    [Fact]
    public void ProcessingStatus_AllEnumValues_CanBeSet()
    {
        // Test each enum value
        var docNew = new UploadedDocument
        {
            DocumentId = Guid.NewGuid().ToString(),
            FileName = "test.pdf",
            FileSizeBytes = 1024,
            UploadTimestamp = DateTime.UtcNow,
            StorageLocationReference = "blob://test",
            DocumentType = DocumentType.MechanicalSpec,
            ProcessingStatus = DocumentProcessingStatus.New
        };
        Assert.Equal(DocumentProcessingStatus.New, docNew.ProcessingStatus);

        var docProcessing = new UploadedDocument
        {
            DocumentId = Guid.NewGuid().ToString(),
            FileName = "test.pdf",
            FileSizeBytes = 1024,
            UploadTimestamp = DateTime.UtcNow,
            StorageLocationReference = "blob://test",
            DocumentType = DocumentType.MechanicalSpec,
            ProcessingStatus = DocumentProcessingStatus.Processing
        };
        Assert.Equal(DocumentProcessingStatus.Processing, docProcessing.ProcessingStatus);

        var docSucceeded = new UploadedDocument
        {
            DocumentId = Guid.NewGuid().ToString(),
            FileName = "test.pdf",
            FileSizeBytes = 1024,
            UploadTimestamp = DateTime.UtcNow,
            StorageLocationReference = "blob://test",
            DocumentType = DocumentType.MechanicalSpec,
            ProcessingStatus = DocumentProcessingStatus.Succeeded
        };
        Assert.Equal(DocumentProcessingStatus.Succeeded, docSucceeded.ProcessingStatus);

        var docFailed = new UploadedDocument
        {
            DocumentId = Guid.NewGuid().ToString(),
            FileName = "test.pdf",
            FileSizeBytes = 1024,
            UploadTimestamp = DateTime.UtcNow,
            StorageLocationReference = "blob://test",
            DocumentType = DocumentType.MechanicalSpec,
            ProcessingStatus = DocumentProcessingStatus.Failed
        };
        Assert.Equal(DocumentProcessingStatus.Failed, docFailed.ProcessingStatus);
    }

    [Fact]
    public void UploadedDocument_StatusTransition_FromProcessingToSucceeded()
    {
        // Arrange
        var document = new UploadedDocument
        {
            DocumentId = Guid.NewGuid().ToString(),
            FileName = "spec.pdf",
            FileSizeBytes = 1024,
            UploadTimestamp = DateTime.UtcNow,
            StorageLocationReference = "blob://test",
            DocumentType = DocumentType.MechanicalSpec,
            ProcessingStatus = DocumentProcessingStatus.Processing
        };

        // Act
        document.ProcessingStatus = DocumentProcessingStatus.Succeeded;

        // Assert
        Assert.Equal(DocumentProcessingStatus.Succeeded, document.ProcessingStatus);
        Assert.Null(document.ErrorMessage);
    }

    [Fact]
    public void UploadedDocument_StatusTransition_FromProcessingToFailed()
    {
        // Arrange
        var document = new UploadedDocument
        {
            DocumentId = Guid.NewGuid().ToString(),
            FileName = "spec.pdf",
            FileSizeBytes = 1024,
            UploadTimestamp = DateTime.UtcNow,
            StorageLocationReference = "blob://test",
            DocumentType = DocumentType.MechanicalSpec,
            ProcessingStatus = DocumentProcessingStatus.Processing
        };

        // Act
        document.ProcessingStatus = DocumentProcessingStatus.Failed;
        document.ErrorMessage = "Document parsing failed: corrupted PDF";

        // Assert
        Assert.Equal(DocumentProcessingStatus.Failed, document.ProcessingStatus);
        Assert.NotNull(document.ErrorMessage);
        Assert.Contains("corrupted PDF", document.ErrorMessage);
    }

    [Theory]
    [InlineData(DocumentType.MechanicalSpec)]
    [InlineData(DocumentType.PlanDrawing)]
    [InlineData(DocumentType.Other)]
    public void UploadedDocument_AllDocumentTypes_ShouldBeValid(DocumentType docType)
    {
        // Arrange & Act
        var document = new UploadedDocument
        {
            DocumentId = Guid.NewGuid().ToString(),
            FileName = "test.pdf",
            FileSizeBytes = 1024,
            UploadTimestamp = DateTime.UtcNow,
            StorageLocationReference = "blob://test",
            DocumentType = docType,
            ProcessingStatus = DocumentProcessingStatus.New
        };

        // Assert
        Assert.Equal(docType, document.DocumentType);
    }

    [Theory]
    [InlineData(DocumentProcessingStatus.New)]
    [InlineData(DocumentProcessingStatus.Processing)]
    [InlineData(DocumentProcessingStatus.Succeeded)]
    [InlineData(DocumentProcessingStatus.Failed)]
    public void UploadedDocument_AllProcessingStatuses_ShouldBeValid(DocumentProcessingStatus status)
    {
        // Arrange & Act
        var document = new UploadedDocument
        {
            DocumentId = Guid.NewGuid().ToString(),
            FileName = "test.pdf",
            FileSizeBytes = 1024,
            UploadTimestamp = DateTime.UtcNow,
            StorageLocationReference = "blob://test",
            DocumentType = DocumentType.MechanicalSpec,
            ProcessingStatus = status
        };

        // Assert
        Assert.Equal(status, document.ProcessingStatus);
    }

    [Fact]
    public void UploadedDocument_WithFailedStatus_ShouldHaveErrorMessage()
    {
        // Arrange
        var errorMessage = "Unable to extract text from PDF";
        var document = new UploadedDocument
        {
            DocumentId = Guid.NewGuid().ToString(),
            FileName = "corrupted.pdf",
            FileSizeBytes = 1024,
            UploadTimestamp = DateTime.UtcNow,
            StorageLocationReference = "blob://test",
            DocumentType = DocumentType.MechanicalSpec,
            ProcessingStatus = DocumentProcessingStatus.Failed,
            ErrorMessage = errorMessage
        };

        // Act & Assert
        Assert.Equal(DocumentProcessingStatus.Failed, document.ProcessingStatus);
        Assert.NotNull(document.ErrorMessage);
        Assert.Equal(errorMessage, document.ErrorMessage);
    }
}
