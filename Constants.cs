using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorHybridCustomAuthentication;

public static class Constants
{
    public static IAccount Account { get; set; }
    public static int UserId { get; set; }
    public static readonly string TenantName = "yourdomain.com";
    public static readonly string TenantId = "your-tenant-id";
    public static readonly string ClientId = "your-client-id";
    public static readonly string ClientSecret = "your-secret";
    public static readonly string RedirectURI = $"msal{ClientId}://auth";
    public static readonly string GraphScope = "https://graph.microsoft.com/User.Read";
    //public static readonly string IScopes = $"{GraphScope} {ApiScope}";
    //public static readonly string ApiScope = "api://your-api-client-id/api.scope";
}
//public static class Constants
//{
//    public static IAccount Account { get; set; }
//    public static int UserId { get; set; }

//    // Replace with actual domain name
//    public static readonly string TenantName = "your-domain.com";

//    // Replace with actual Tenant ID from Azure AD
//    public static readonly string TenantId = "your-tenant-id";

//    // Replace with actual Client ID from Azure AD application registration
//    public static readonly string ClientId = "your-client-id";

//    // Store Client Secret securely, not directly in code
//    public static readonly string ClientSecret = GetClientSecretFromConfig(); // Replace with logic to retrieve from secure store

//    // Redirect URI configured in Azure AD application registration
//    public static readonly string RedirectURI = $"msal{ClientId}://auth";

//    // Scope for accessing Microsoft Graph User data
//    public static readonly string GraphScope = "https://graph.microsoft.com/User.Read";

//    // Scope for accessing your custom API (replace with actual API client ID)
//    public static readonly string ApiScope = "api://your-api-client-id/api.scope";

//    // Combined scope for Graph and your API
//    public static readonly string IScopes = $"{GraphScope} {ApiScope}";

//    // Scope for accessing your custom API (replace with actual API client ID)
//    public static readonly string ApiScope = "api://your-api-client-id/api.scope";

//    // Helper method to retrieve Client Secret securely (implementation details omitted)
//    private static string GetClientSecretFromConfig()
//    {
//        // Replace with logic to retrieve Client Secret from secure store (e.g., environment variable)
//        throw new NotImplementedException("Implement logic to get Client Secret from secure configuration");
//    }
//}
