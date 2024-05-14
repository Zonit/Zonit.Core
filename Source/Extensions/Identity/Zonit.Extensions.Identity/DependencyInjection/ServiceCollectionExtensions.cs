using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Zonit.Extensions.Identity;
using Zonit.Extensions.Identity.Repositories;
using Zonit.Extensions.Identity.Services;

namespace Zonit.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddIdentityExtension(this IServiceCollection services)
    {
        services.AddScoped<AuthenticationStateProvider, SessionAuthenticationService>();

        services.AddScoped<IAuthenticatedRepository, AuthenticatedRepository>();
        services.AddScoped<IAuthenticatedProvider, AuthenticatedService>();


        return services;
    }
}