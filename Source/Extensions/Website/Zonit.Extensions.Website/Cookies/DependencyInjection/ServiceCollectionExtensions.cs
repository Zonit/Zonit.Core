using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Zonit.Extensions.Website.Abstractions.Cookies;
using Zonit.Extensions.Website.Abstractions.Cookies.Models;
using Zonit.Extensions.Website.Cookies.Services;

namespace Zonit.Extensions;

public static class ServiceCollectionCookiesExtensions
{
    public static IServiceCollection AddCookiesExtension(this IServiceCollection services)
    {
        services.AddHttpContextAccessor();

        services.AddScoped<ICookie, CookieService>();

        services.AddCascadingValue(sp =>
        {
            var httpContextAccessor = sp.GetRequiredService<IHttpContextAccessor>();
            var cookie = sp.GetRequiredService<ICookie>();

            if (httpContextAccessor.HttpContext is not null)
                foreach (var c in httpContextAccessor.HttpContext.Request.Cookies)
                    cookie.Set(c.Key, c.Value);

            //return cookie.GetCookies();
            return new CascadingValueSource<List<CookieModel>>(cookie.GetCookies(), isFixed: true);
        });

        return services;
    }
}