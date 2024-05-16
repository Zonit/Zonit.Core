using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Zonit.Extensions.Identity.Middlewares;

namespace Zonit.Extensions;

public static class MiddlewareExtensions
{
    public static IApplicationBuilder UseIdentityExtension(this IApplicationBuilder builder)
    {
        builder.UseMiddleware<SessionMiddleware>();

        builder.UseAuthorization();
        builder.UseAuthentication();

        return builder;
    }
}