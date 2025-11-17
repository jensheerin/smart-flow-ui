// Copyright (c) Microsoft. All rights reserved.

using System.Net;
using System.Text.Json;

namespace MinimalApi.Middleware;

/// <summary>
/// Global exception handler middleware that catches unhandled exceptions,
/// generates correlation IDs, logs errors, and returns structured JSON error responses.
/// </summary>
public class GlobalExceptionHandler(RequestDelegate next, ILogger<GlobalExceptionHandler> logger)
{
    private readonly RequestDelegate _next = next;
    private readonly ILogger<GlobalExceptionHandler> _logger = logger;

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var correlationId = Guid.NewGuid().ToString();

        _logger.LogError(exception, "Unhandled exception occurred. CorrelationId: {CorrelationId}", correlationId);

        var (statusCode, message) = MapExceptionToResponse(exception);

        context.Response.StatusCode = statusCode;
        context.Response.ContentType = "application/json";

        var errorResponse = new
        {
            error = new
            {
                message = message,
                correlationId = correlationId,
                statusCode = statusCode
            }
        };

        var json = JsonSerializer.Serialize(errorResponse, new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        });

        await context.Response.WriteAsync(json);
    }

    private static (int statusCode, string message) MapExceptionToResponse(Exception exception)
    {
        return exception switch
        {
            ArgumentNullException => ((int)HttpStatusCode.BadRequest, "Validation failed"),
            ArgumentException => ((int)HttpStatusCode.BadRequest, "Validation failed"),
            InvalidOperationException when exception.Message.Contains("not found", StringComparison.OrdinalIgnoreCase)
                => ((int)HttpStatusCode.NotFound, "Resource not found"),
            KeyNotFoundException => ((int)HttpStatusCode.NotFound, "Resource not found"),
            UnauthorizedAccessException => ((int)HttpStatusCode.Unauthorized, "Unauthorized"),
            _ => ((int)HttpStatusCode.InternalServerError, "An internal server error occurred")
        };
    }
}
