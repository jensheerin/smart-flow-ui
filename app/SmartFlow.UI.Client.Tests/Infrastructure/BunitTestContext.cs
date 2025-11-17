using Bunit;
using Microsoft.Extensions.DependencyInjection;

namespace SmartFlow.UI.Client.Tests.Infrastructure;

/// <summary>
/// Base test context for bUnit component tests.
/// Provides common setup and service registration for Blazor component testing.
/// </summary>
public class BunitTestContext : TestContext
{
    /// <summary>
    /// Initializes a new instance of BunitTestContext with default services.
    /// </summary>
    public BunitTestContext()
    {
        // Register common services needed by components
        RegisterCommonServices();
    }

    /// <summary>
    /// Registers common services used across component tests.
    /// Override this method to add additional test-specific services.
    /// </summary>
    protected virtual void RegisterCommonServices()
    {
        // Register mock services here
        // Example: Services.AddSingleton<IApiClient, MockApiClient>();

        // Note: Add MudBlazor services when writing component tests:
        // Services.AddMudServices();
    }

    /// <summary>
    /// Creates a test context with custom service registration.
    /// </summary>
    /// <param name="configureServices">Action to configure additional services</param>
    /// <returns>Configured test context</returns>
    public static BunitTestContext CreateWithServices(Action<IServiceCollection> configureServices)
    {
        var context = new BunitTestContext();
        configureServices(context.Services);
        return context;
    }
}
