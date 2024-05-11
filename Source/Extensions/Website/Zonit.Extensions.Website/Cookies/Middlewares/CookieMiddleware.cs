using Microsoft.AspNetCore.Http;
using Zonit.Extensions.Website.Abstractions.Cookies;

namespace Zonit.Extensions.Website.Cookies.Middlewares;

internal class CookieMiddleware(RequestDelegate next)
{
    readonly RequestDelegate _next = next;

    public Task Invoke(HttpContext httpContext, ICookie cookie)
    {
        foreach (var c in httpContext.Request.Cookies)
            cookie.Set(c.Key, c.Value);

        return _next(httpContext);
    }
}