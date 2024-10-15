using Microsoft.Identity.Client;
using System.Diagnostics;

namespace BlazorHybridCustomAuthentication;

public class AuthService
{
    private IPublicClientApplication authenticationClient; // Stores the MSAL Public Client Application instance

    public AuthService()
    {
    }

    // Attempts to silently acquire a token for the API. Falls back to interactive login if necessary.
    public async Task<AuthenticationResult> LoginAsync(CancellationToken cancellationToken)
    {
        // Acquire token for API
        AuthenticationResult apiResult = await AcquireSilentTokenAsync(cancellationToken);

        return apiResult;
    }

    // Attempts to acquire a token silently (without user interaction).
    private async Task<AuthenticationResult> AcquireSilentTokenAsync(CancellationToken cancellationToken)
    {
        // Check if the authentication client is already created
        if (authenticationClient == null)
        {
            authenticationClient = PublicClientApplicationBuilder
                .Create(Constants.ClientId) // Use the client ID from Constants
                .WithAuthority($"https://login.microsoftonline.com/{Constants.TenantId}/oauth2/v2.0/token") // Set the authority URL
#if ANDROID
                    .WithRedirectUri($"msal{Constants.ClientId}://auth") // Redirect URI for Android
                    .WithParentActivityOrWindow(() => Platform.CurrentActivity) // Platform-specific configuration for Android
#elif IOS
                    .WithIosKeychainSecurityGroup("com.company.pps") // Keychain security group for iOS
                    .WithRedirectUri($"msal{Constants.ClientId}://auth") // Redirect URI for iOS
#else
                    .WithRedirectUri($"http://localhost") // Redirect URI (default)
#endif
            .Build();
        }

        var accounts = await authenticationClient.GetAccountsAsync(); // Get existing accounts
        IAccount account = accounts.FirstOrDefault(); // Get the first account (if any)

        // Define the scopes for the API access
        IEnumerable<string> apiScopes = new List<string>() { $"User.Read" };

        AuthenticationResult result = null;
        bool tryInteractiveLogin = false;

        try
        {
            // Attempt to acquire token silently
            result = await AcquireTokenInteractiveAsync(apiScopes, cancellationToken);
        }
        catch (MsalUiRequiredException) // Exception if UI is needed
        {
            tryInteractiveLogin = true;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"MSAL Silent Error: {ex.Message}"); // Log silent acquisition error
        }

        if (tryInteractiveLogin)
        {
            try
            {
                // Silent acquisition failed, attempt interactive login
                result = await AcquireTokenInteractiveAsync(apiScopes, cancellationToken);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"MSAL Interactive Error: {ex.Message}");
            }
        }

        return result;
    }

    // Acquires a token interactively \(prompts the user for credentials\)\
    public async Task<AuthenticationResult?> AcquireTokenInteractiveAsync(IEnumerable<string> scopes, CancellationToken cancellationToken)
    {
        if (authenticationClient == null)
            return null; // No client instance, cannot acquire token

        AuthenticationResult result = null;
        try
        {
            result = await authenticationClient
                    .AcquireTokenInteractive(scopes) // Specify the scopes to request
                    .WithPrompt(Prompt.SelectAccount) // Prompt the user to select an account \(optional\)
                    .WithTenantId(Constants.TenantId) // Set the tenant ID from Constants
                    .WithTenantIdFromAuthority(new Uri($"https://login.microsoftonline.com/{Constants.TenantId}")) // Set tenant ID from authority URL
                    .ExecuteAsync(cancellationToken); // Execute the token acquisition request
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"MSAL Interactive Error: {ex.Message}"); // Log interactive login error
        }

        return result;
    }

    // Logs out the user by removing all cached accounts.
    public async Task LogoutAsync(CancellationToken cancellationToken)
    {
        var accounts = await authenticationClient.GetAccountsAsync();
        foreach (var account in accounts)
        {
            await authenticationClient.RemoveAsync(account);
        }
    }
}
