
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Authorization;
using Blazored.LocalStorage;
using System.IdentityModel.Tokens.Jwt;

namespace AcademiaCoursePortal.UI.Services;

public class CustomAuthenticationStateProvider : AuthenticationStateProvider
{
    private readonly ILocalStorageService _localStorage;

    public CustomAuthenticationStateProvider(ILocalStorageService localStorage)
    {
        _localStorage = localStorage;
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var token = await _localStorage.GetItemAsync<string>("authToken");
        var identity = string.IsNullOrEmpty(token) ? new ClaimsIdentity() : GetClaimsIdentityFromToken(token);
        var user = new ClaimsPrincipal(identity);
        return new AuthenticationState(user);
    }

    public ClaimsIdentity GetClaimsIdentityFromToken(string token)
    {
        var handler = new JwtSecurityTokenHandler();
        var jwtToken = handler.ReadJwtToken(token);

        // Assuming the user ID is in the "sub" claim (standard for user ID) or a custom "userId" claim
        var userIdClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier || c.Type == "userId");

        if (userIdClaim != null)
        {
            // Create an identity with the user ID as a claim
            var claims = new[] { new Claim(ClaimTypes.NameIdentifier, userIdClaim.Value) };
            return new ClaimsIdentity(claims, "jwt");
        }

        return new ClaimsIdentity();
    }

}


