using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Text.Encodings.Web;

namespace Zonit.Extensions.Identity.Services;

public class AuthenticationSchemeService(
        IOptionsMonitor<AuthenticationSchemeOptions> options,
        ILoggerFactory logger,
        UrlEncoder encoder,
        ISessionProvider _session
    ) : AuthenticationHandler<AuthenticationSchemeOptions>(options, logger, encoder)
{

    protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        var sessionValue = Request.Cookies["Session"];

        if (sessionValue is null)
            return AuthenticateResult.Fail("Unauthorized");

        var user = await _session.GetByTokenAsync(sessionValue);

        if (user is null)
            return AuthenticateResult.Fail("Unauthorized");

        var identity = new ClaimsIdentity([
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            ], "Logged");

        if (user.Roles is not null)
            foreach (var role in user.Roles)
                identity.AddClaim(new Claim(ClaimTypes.Role, role));
            
        var principal = new ClaimsPrincipal(identity);
        var ticket = new AuthenticationTicket(principal, "ZonitIdentity");

        return AuthenticateResult.Success(ticket);
    }
}