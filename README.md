# Blazor Hybrid Entra Id Authentication
## Overview
This Blazor Hybrid application demonstrates how to implement custom authentication using Azure Active Directory (Entra ID). It provides a secure and efficient way for users to log in to your application.

## Prerequisites
* **Azure AD (Entra ID) Tenant:** Create an Azure AD tenant if you don't have one.
* **Registered Application:** Register your application in Azure AD, obtaining the necessary client ID, client secret, and tenant ID.
* **Blazor Hybrid Project:** Set up a Blazor Hybrid project using your preferred framework (e.g., .NET MAUI).

## Setup
1. **Install Packages:** Install the required NuGet packages:
  * `Microsoft.AspNetCore.Components.Authorization`
  * `Microsoft.Identity.Client`
  * `Microsoft.Extensions.DependencyInjection.Extensions`

2. **Configure Constants:** Create a `Constants.cs` file and define the following constants:
  * `ClientId`
  * `TenantId`
  * `ClientSecret`
  * `RedirectURI`
  * `GraphScope`
  * `IScopes`
  * `ApiScope`

3. **Register Services:** In your `Program.cs` file, register the `CustomAuthenticationStateProvider` and `AuthenticationStateProvider`.

## Custom Authentication Provider
Create a `CustomAuthenticationStateProvider.cs` file to handle authentication logic:
* **AuthenticateLogin:** Stores the access token in secure storage and notifies the authentication state provider.
* **RemoveAuthenticationLogout:** Removes the access token from secure storage and notifies the authentication state provider.
* **GetAuthenticationStateAsync:** Retrieves the access token from secure storage and creates an authentication state if valid.

## Authentication Service
Create an `AuthService.cs` file to manage authentication:
* **LoginAsync:** Attempts to acquire a token silently. If it fails, acquires interactively.
* **AcquireSilentTokenAsync:** Acquires a token silently using cached credentials.
* **AcquireTokenInteractiveAsync:** Prompts the user to enter credentials and acquires a token.
* **LogoutAsync:** Removes cached accounts.

## Platform-Specific Configurations

### **iOS:**
* Add the following to your `Info.plist`:
  * `<key>CFBundleURLTypes</key>` with a dictionary containing `CFBundleTypeRole`, `CFBundleURLName`, and `CFBundleURLSchemes`.
  * `<key>LSApplicationQueriesSchemes</key>` with the values `"msauthv2"` and `"msauthv3"`.
* Implement `OpenUrl` in your `AppDelegate.cs` to handle authentication callbacks.

### Android:
* Add the following to your `AndroidManifest.xml`:
  * `<activity>` for `microsoft.identity.client.BrowserTabActivity` with appropriate intent filters.
  * `<queries>` for package names and intents.
  * Permissions: `ACCESS_NETWORK_STATE`, `CAMERA`, `INTERNET`.
* Override `OnActivityResult` in your `MainActivity.cs` to handle authentication continuation.

## Login Page
Create a Blazor component for the login page:
* Use a button to trigger authentication.
* Handle the authentication result and navigate accordingly.
* Display error messages if necessary.

## Note:
This project has been tested on Android and Windows platforms. **iOS testing is pending.**
