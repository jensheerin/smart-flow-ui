// Copyright (c) Microsoft. All rights reserved.

using System.Net;
using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using MinimalApi.Middleware;
using Moq;
using Xunit;

namespace SmartFlow.UI.API.Tests.Middleware;

public class GlobalExceptionHandlerTests
{
    private readonly Mock<ILogger<GlobalExceptionHandler>> _mockLogger;
    private readonly GlobalExceptionHandler _middleware;

    public GlobalExceptionHandlerTests()
    {
        _mockLogger = new Mock<ILogger<GlobalExceptionHandler>>();
        _middleware = new GlobalExceptionHandler(
            next: (innerHttpContext) => throw new InvalidOperationException("Test exception"),
            logger: _mockLogger.Object
        );
    }

    [Fact]
    public async Task HandleArgumentNullException_Returns400BadRequest()
    {
        // Arrange
        var context = new DefaultHttpContext();
        context.Response.Body = new MemoryStream();

        var middleware = new GlobalExceptionHandler(
            next: (innerHttpContext) => throw new ArgumentNullException("testParam", "Test validation error"),
            logger: _mockLogger.Object
        );

        // Act
        await middleware.InvokeAsync(context);

        // Assert
        Assert.Equal((int)HttpStatusCode.BadRequest, context.Response.StatusCode);

        context.Response.Body.Seek(0, SeekOrigin.Begin);
        var responseBody = await new StreamReader(context.Response.Body).ReadToEndAsync();
        var response = JsonSerializer.Deserialize<ErrorResponse>(responseBody, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        Assert.NotNull(response);
        Assert.NotNull(response.Error);
        Assert.Equal("Validation failed", response.Error.Message);
        Assert.Equal(400, response.Error.StatusCode);
        Assert.NotNull(response.Error.CorrelationId);
        Assert.NotEmpty(response.Error.CorrelationId);
    }

    [Fact]
    public async Task HandleArgumentException_Returns400BadRequest()
    {
        // Arrange
        var context = new DefaultHttpContext();
        context.Response.Body = new MemoryStream();

        var middleware = new GlobalExceptionHandler(
            next: (innerHttpContext) => throw new ArgumentException("Invalid argument"),
            logger: _mockLogger.Object
        );

        // Act
        await middleware.InvokeAsync(context);

        // Assert
        Assert.Equal((int)HttpStatusCode.BadRequest, context.Response.StatusCode);

        context.Response.Body.Seek(0, SeekOrigin.Begin);
        var responseBody = await new StreamReader(context.Response.Body).ReadToEndAsync();
        var response = JsonSerializer.Deserialize<ErrorResponse>(responseBody, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        Assert.NotNull(response);
        Assert.NotNull(response.Error);
        Assert.Equal("Validation failed", response.Error.Message);
        Assert.Equal(400, response.Error.StatusCode);
    }

    [Fact]
    public async Task HandleKeyNotFoundException_Returns404NotFound()
    {
        // Arrange
        var context = new DefaultHttpContext();
        context.Response.Body = new MemoryStream();

        var middleware = new GlobalExceptionHandler(
            next: (innerHttpContext) => throw new KeyNotFoundException("Resource not found"),
            logger: _mockLogger.Object
        );

        // Act
        await middleware.InvokeAsync(context);

        // Assert
        Assert.Equal((int)HttpStatusCode.NotFound, context.Response.StatusCode);

        context.Response.Body.Seek(0, SeekOrigin.Begin);
        var responseBody = await new StreamReader(context.Response.Body).ReadToEndAsync();
        var response = JsonSerializer.Deserialize<ErrorResponse>(responseBody, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        Assert.NotNull(response);
        Assert.NotNull(response.Error);
        Assert.Equal("Resource not found", response.Error.Message);
        Assert.Equal(404, response.Error.StatusCode);
    }

    [Fact]
    public async Task HandleInvalidOperationExceptionWithNotFound_Returns404NotFound()
    {
        // Arrange
        var context = new DefaultHttpContext();
        context.Response.Body = new MemoryStream();

        var middleware = new GlobalExceptionHandler(
            next: (innerHttpContext) => throw new InvalidOperationException("Item not found in database"),
            logger: _mockLogger.Object
        );

        // Act
        await middleware.InvokeAsync(context);

        // Assert
        Assert.Equal((int)HttpStatusCode.NotFound, context.Response.StatusCode);

        context.Response.Body.Seek(0, SeekOrigin.Begin);
        var responseBody = await new StreamReader(context.Response.Body).ReadToEndAsync();
        var response = JsonSerializer.Deserialize<ErrorResponse>(responseBody, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        Assert.NotNull(response);
        Assert.NotNull(response.Error);
        Assert.Equal("Resource not found", response.Error.Message);
        Assert.Equal(404, response.Error.StatusCode);
    }

    [Fact]
    public async Task HandleUnauthorizedAccessException_Returns401Unauthorized()
    {
        // Arrange
        var context = new DefaultHttpContext();
        context.Response.Body = new MemoryStream();

        var middleware = new GlobalExceptionHandler(
            next: (innerHttpContext) => throw new UnauthorizedAccessException("Access denied"),
            logger: _mockLogger.Object
        );

        // Act
        await middleware.InvokeAsync(context);

        // Assert
        Assert.Equal((int)HttpStatusCode.Unauthorized, context.Response.StatusCode);

        context.Response.Body.Seek(0, SeekOrigin.Begin);
        var responseBody = await new StreamReader(context.Response.Body).ReadToEndAsync();
        var response = JsonSerializer.Deserialize<ErrorResponse>(responseBody, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        Assert.NotNull(response);
        Assert.NotNull(response.Error);
        Assert.Equal("Unauthorized", response.Error.Message);
        Assert.Equal(401, response.Error.StatusCode);
    }

    [Fact]
    public async Task HandleGenericException_Returns500InternalServerError()
    {
        // Arrange
        var context = new DefaultHttpContext();
        context.Response.Body = new MemoryStream();

        var middleware = new GlobalExceptionHandler(
            next: (innerHttpContext) => throw new Exception("Unexpected error"),
            logger: _mockLogger.Object
        );

        // Act
        await middleware.InvokeAsync(context);

        // Assert
        Assert.Equal((int)HttpStatusCode.InternalServerError, context.Response.StatusCode);

        context.Response.Body.Seek(0, SeekOrigin.Begin);
        var responseBody = await new StreamReader(context.Response.Body).ReadToEndAsync();
        var response = JsonSerializer.Deserialize<ErrorResponse>(responseBody, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        Assert.NotNull(response);
        Assert.NotNull(response.Error);
        Assert.Equal("An internal server error occurred", response.Error.Message);
        Assert.Equal(500, response.Error.StatusCode);
    }

    [Fact]
    public async Task AllExceptions_IncludeCorrelationIdInResponse()
    {
        // Arrange
        var context = new DefaultHttpContext();
        context.Response.Body = new MemoryStream();

        var middleware = new GlobalExceptionHandler(
            next: (innerHttpContext) => throw new Exception("Test exception"),
            logger: _mockLogger.Object
        );

        // Act
        await middleware.InvokeAsync(context);

        // Assert
        context.Response.Body.Seek(0, SeekOrigin.Begin);
        var responseBody = await new StreamReader(context.Response.Body).ReadToEndAsync();
        var response = JsonSerializer.Deserialize<ErrorResponse>(responseBody, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        Assert.NotNull(response);
        Assert.NotNull(response.Error);
        Assert.NotNull(response.Error.CorrelationId);
        Assert.NotEmpty(response.Error.CorrelationId);

        // Verify it's a valid GUID
        Assert.True(Guid.TryParse(response.Error.CorrelationId, out _));
    }

    [Fact]
    public async Task AllExceptions_LoggedWithCorrelationId()
    {
        // Arrange
        var context = new DefaultHttpContext();
        context.Response.Body = new MemoryStream();

        var testException = new Exception("Test exception");
        var middleware = new GlobalExceptionHandler(
            next: (innerHttpContext) => throw testException,
            logger: _mockLogger.Object
        );

        // Act
        await middleware.InvokeAsync(context);

        // Assert
        _mockLogger.Verify(
            x => x.Log(
                LogLevel.Error,
                It.IsAny<EventId>(),
                It.Is<It.IsAnyType>((v, t) => v.ToString()!.Contains("CorrelationId")),
                testException,
                It.IsAny<Func<It.IsAnyType, Exception?, string>>()),
            Times.Once);
    }

    [Fact]
    public async Task StructuredErrorResponse_ContainsAllRequiredFields()
    {
        // Arrange
        var context = new DefaultHttpContext();
        context.Response.Body = new MemoryStream();

        var middleware = new GlobalExceptionHandler(
            next: (innerHttpContext) => throw new ArgumentException("Test error"),
            logger: _mockLogger.Object
        );

        // Act
        await middleware.InvokeAsync(context);

        // Assert
        context.Response.Body.Seek(0, SeekOrigin.Begin);
        var responseBody = await new StreamReader(context.Response.Body).ReadToEndAsync();
        var response = JsonSerializer.Deserialize<ErrorResponse>(responseBody, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        Assert.NotNull(response);
        Assert.NotNull(response.Error);
        Assert.NotNull(response.Error.Message);
        Assert.NotEmpty(response.Error.Message);
        Assert.True(response.Error.StatusCode > 0);
        Assert.NotNull(response.Error.CorrelationId);
        Assert.NotEmpty(response.Error.CorrelationId);

        // Verify content type
        Assert.Equal("application/json", context.Response.ContentType);
    }

    [Fact]
    public async Task NoException_RequestCompletesSuccessfully()
    {
        // Arrange
        var context = new DefaultHttpContext();
        var nextCalled = false;

        var middleware = new GlobalExceptionHandler(
            next: (innerHttpContext) =>
            {
                nextCalled = true;
                return Task.CompletedTask;
            },
            logger: _mockLogger.Object
        );

        // Act
        await middleware.InvokeAsync(context);

        // Assert
        Assert.True(nextCalled);
        Assert.Equal((int)HttpStatusCode.OK, context.Response.StatusCode);
    }

    // Helper classes for deserialization
    private class ErrorResponse
    {
        public ErrorDetails? Error { get; set; }
    }

    private class ErrorDetails
    {
        public string? Message { get; set; }
        public string? CorrelationId { get; set; }
        public int StatusCode { get; set; }
    }
}
