using Microsoft.AspNetCore.Components.Authorization;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BlazorHybridCustomAuthentication;

public class CustomAuthenticationStateProvider : AuthenticationStateProvider
{
    private AuthService _authService;
    public CustomAuthenticationStateProvider()
    {
    }

    public async Task AuthenticateLogin(string token)
    {
        // Add token to secure storage.
        await SecureStorage.Default.SetAsync("accesstoken", token);
        NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
    }

    public async Task RemoveAuthenticationLogout()
    {
        //Call to cancel the token 
        //await _authService.LogoutAsync(CancellationToken.None); // This call is not working.
        // Remove token from secure storage.
        SecureStorage.Default.Remove("accesstoken");

        NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        try
        {
            // Pull Token from secure storage.
            var token = await SecureStorage.Default.GetAsync("accesstoken");

            // If token is null or empty then no token is stored and authentication is not given.
            if (token != null)
            {
                var handler = new JwtSecurityTokenHandler();
                var data = handler.ReadJwtToken(token);
                var expirationTime = data.ValidTo;
                string name = data.Claims.FirstOrDefault(x => x.Type.Equals("name"))?.Value;

                var claims = new[] { new Claim(ClaimTypes.Name, $"{name}") };
                var identity = new ClaimsIdentity(claims, "Server authentication");

                Console.WriteLine($"Request Successful: {name}");

                return new AuthenticationState(new ClaimsPrincipal(identity));
            }
            else
            {
                Console.WriteLine("Successfully Logged out.");
            }
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine("Request failed:" + ex.ToString());
        }

        return new AuthenticationState(new ClaimsPrincipal());
    }
}
