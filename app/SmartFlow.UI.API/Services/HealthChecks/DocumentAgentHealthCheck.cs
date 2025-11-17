// Copyright (c) Microsoft. All rights reserved.

using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace MinimalApi.Services.HealthChecks;

/// <summary>
/// Health check for Document Agent API connectivity.
/// Verifies that the Document Agent service is accessible and responsive.
/// </summary>
public class DocumentAgentHealthCheck(IHttpClientFactory httpClientFactory, ILogger<DocumentAgentHealthCheck> logger) : IHealthCheck
{
    private readonly IHttpClientFactory _httpClientFactory = httpClientFactory;
    private readonly ILogger<DocumentAgentHealthCheck> _logger = logger;

    public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
    {
        Dictionary<string, object> data = [];

#pragma warning disable CA1031 // Do not catch general exception types
        try
        {
            var client = _httpClientFactory.CreateClient("DocumentAgentClient");

            if (client.BaseAddress == null)
            {
                data.Add("DocumentAgent", "Document Agent client is not configured");
                return new HealthCheckResult(HealthStatus.Degraded, description: "Document Agent client configuration is missing", data: data);
            }

            data.Add("Endpoint", client.BaseAddress.ToString());

            // Try to call the health endpoint
            var response = await client.GetAsync("/health", cancellationToken).ConfigureAwait(false);

            if (response.IsSuccessStatusCode)
            {
                data.Add("Status", "Document Agent API is responsive");
                return new HealthCheckResult(HealthStatus.Healthy, description: "Document Agent API is accessible", data: data);
            }

            data.Add("Status", $"Document Agent API returned status code: {response.StatusCode}");
            return new HealthCheckResult(HealthStatus.Degraded, description: $"Document Agent API returned non-success status: {response.StatusCode}", data: data);
        }
        catch (HttpRequestException ex)
        {
            _logger.LogWarning(ex, "Document Agent API health check failed due to HTTP request error");
            data.Add("Error", "Document Agent API is not reachable");
            return new HealthCheckResult(HealthStatus.Unhealthy, description: "Document Agent API is not reachable", exception: ex, data: data);
        }
        catch (TaskCanceledException ex)
        {
            _logger.LogWarning(ex, "Document Agent API health check timed out");
            data.Add("Error", "Document Agent API health check timed out");
            return new HealthCheckResult(HealthStatus.Unhealthy, description: "Document Agent API health check timed out", exception: ex, data: data);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Document Agent API health check failed with unexpected error");
            data.Add("Error", "Document Agent API health check failed");
            return new HealthCheckResult(HealthStatus.Unhealthy, description: "Document Agent API health check failed", exception: ex, data: data);
        }
#pragma warning restore CA1031 // Do not catch general exception types
    }
}
