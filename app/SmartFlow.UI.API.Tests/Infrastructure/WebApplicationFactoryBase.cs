using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace SmartFlow.UI.API.Tests.Infrastructure;

/// <summary>
/// Base class for integration tests using WebApplicationFactory.
/// Provides a configured test server for API endpoint testing.
/// </summary>
/// <typeparam name="TProgram">The Program class of the API project</typeparam>
public class WebApplicationFactoryBase<TProgram> : WebApplicationFactory<TProgram>
    where TProgram : class
{
    /// <summary>
    /// Configures the test host for integration tests.
    /// Override this method to customize service registration for tests.
    /// </summary>
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            // Override service registrations for testing here
            // Example: Replace production services with mocks
        });

        builder.UseEnvironment("Testing");
    }

    /// <summary>
    /// Creates a configured HttpClient for API requests in tests.
    /// </summary>
    /// <returns>HttpClient configured for the test server</returns>
    public HttpClient CreateTestClient()
    {
        return CreateClient(new WebApplicationFactoryClientOptions
        {
            AllowAutoRedirect = false,
            BaseAddress = new Uri("http://localhost")
        });
    }
}
