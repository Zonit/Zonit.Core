using Microsoft.AspNetCore.Http;
using Zonit.Extensions.Identity.Repositories;

namespace Zonit.Extensions.Identity.Middlewares;

internal class SessionMiddleware(RequestDelegate _next)
{
    public async Task InvokeAsync(
        HttpContext httpContext,
        ISessionProvider _session,
        IAuthenticatedRepository userRepository  
    )
    {
        var sessionValue = httpContext.Request.Cookies["Session"];

        if (sessionValue is null)
        {
            await _next(httpContext);
            return;
        }

        var session = await _session.GetByTokenAsync(sessionValue);

        if(session is null)
        {
            await _next(httpContext);
            return;
        }

        userRepository.Inicjalize(session);

        await _next(httpContext);
        return;
    }
}