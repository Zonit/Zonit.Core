using Microsoft.AspNetCore.Builder;
using Zonit.Extensions.Identity.Middlewares;

namespace Zonit.Extensions;

public static class MiddlewareExtensions
{
    public static IApplicationBuilder UseIdentityExtension(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<SessionMiddleware>();
    }
}
