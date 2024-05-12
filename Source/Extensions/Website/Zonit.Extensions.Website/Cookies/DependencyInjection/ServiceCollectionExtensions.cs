using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Zonit.Extensions.Website.Abstractions.Cookies;
using Zonit.Extensions.Website.Abstractions.Cookies.Models;
using Zonit.Extensions.Website.Cookies.Services;
using Zonit.Extensions.Website.Cookies.Repositories;
using System.Net;

namespace Zonit.Extensions;

public static class ServiceCollectionCookiesExtensions
{
    public static IServiceCollection AddCookiesExtension(this IServiceCollection services)
    {
        services.AddHttpContextAccessor();

        services.AddScoped<ICookie, CookieService>();
        services.AddScoped<ICookiesRepository, CookiesRepository>();

        services.AddCascadingValue(sp =>
        {
            var httpContextAccessor = sp.GetRequiredService<IHttpContextAccessor>();
            var cookiesRepository = sp.GetRequiredService<ICookiesRepository>();

            if (httpContextAccessor.HttpContext is not null)
                foreach (var c in httpContextAccessor.HttpContext.Request.Cookies)
                    cookiesRepository.Add(new CookieModel
                    {
                        Name = c.Key,
                        Value = c.Value,
                    });

            return new CascadingValueSource<List<CookieModel>>(cookiesRepository.GetCookies(), isFixed: true);
        });

        return services;
    }
}