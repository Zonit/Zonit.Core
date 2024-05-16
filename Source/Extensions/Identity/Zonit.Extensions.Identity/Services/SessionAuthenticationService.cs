using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace Zonit.Extensions.Identity.Services;

internal class SessionAuthenticationService(IAuthenticatedProvider _authenticated) : AuthenticationStateProvider
{
    public override Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var user = new ClaimsPrincipal(CreateIdentity());
        return Task.FromResult(new AuthenticationState(user));
    }

    private ClaimsIdentity CreateIdentity()
    {
        if (_authenticated.User is null)
            return new ClaimsIdentity(); // Anonymous

        var claims = new List<Claim>
        {
            new (ClaimTypes.Name, _authenticated.User.Name),
            new (ClaimTypes.NameIdentifier, _authenticated.User.Id.ToString()),
        };

        if (_authenticated.User.Roles is not null)
            claims.AddRange(_authenticated.User.Roles.Select(role => new Claim(ClaimTypes.Role, role)));

        return new ClaimsIdentity(claims, "Logged");
    }
}