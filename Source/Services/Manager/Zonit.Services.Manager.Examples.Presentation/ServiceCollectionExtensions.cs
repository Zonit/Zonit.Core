using Microsoft.Extensions.DependencyInjection;
using Zonit.Services.Manager.Examples.Presentation.Data;

namespace Zonit.Services;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddTestPresentation(this IServiceCollection services)
    {
        services.AddHostedService<NavData>();
        services.AddHostedService<CultureData>();
        return services;
    }
}