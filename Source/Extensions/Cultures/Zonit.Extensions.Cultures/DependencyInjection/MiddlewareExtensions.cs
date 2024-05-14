using Microsoft.AspNetCore.Builder;
using Zonit.Extensions.Cultures.Middlewares;

namespace Zonit.Extensions;

public static class MiddlewareExtensions
{
    public static IApplicationBuilder UseCultureExtension(this IApplicationBuilder builder)
    {
        builder.UseMiddleware<CultureMiddleware>();

        return builder;
    }
}