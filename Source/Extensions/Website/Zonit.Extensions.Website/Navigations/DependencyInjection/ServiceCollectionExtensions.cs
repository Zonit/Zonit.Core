using Microsoft.Extensions.DependencyInjection;
using Zonit.Extensions.Website.Navigations.Services;
using Zonit.Extensions.Website.Abstractions.Navigations;

namespace Zonit.Extensions;

public static class ServiceCollectionNavigationsExtensions
{
    public static IServiceCollection AddNavigationsExtension(this IServiceCollection services)
    {
        services.AddSingleton<INavigation, NavigationService>();

        return services;
    }
}