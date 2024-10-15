using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using Microsoft.Identity.Client;
using System.Security.Claims;

/*
 * Note: Make sure to add your id's to the following files.
 * Constants.cs
 * AndroidManifest.xml
 */

namespace BlazorHybridCustomAuthentication
{
	public static class MauiProgram
	{
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();

            // Configure authorization services
            // Enables authorization features in the application
            builder.Services.AddAuthorizationCore();

            // Set up the Maui App and configure fonts
            builder.UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                });

            // Add BlazorWebView for rendering Blazor components
            builder.Services.AddMauiBlazorWebView();

            // Enable developer tools and debug logging in debug mode
#if DEBUG
            builder.Services.AddBlazorWebViewDeveloperTools();
            builder.Logging.AddDebug();
#endif

            // Register the custom authentication state provider
            // This provider handles authentication and authorization logic
            builder.Services.AddScoped<CustomAuthenticationStateProvider>();

            // Register the custom authentication state provider as the default
            // AuthenticationStateProvider in the dependency injection container
            builder.Services.AddScoped<AuthenticationStateProvider>(s => s.GetRequiredService<CustomAuthenticationStateProvider>());

            return builder.Build();

        }
    }
}
