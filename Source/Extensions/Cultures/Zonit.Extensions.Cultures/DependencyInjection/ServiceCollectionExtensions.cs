using Microsoft.Extensions.DependencyInjection;
using Zonit.Extensions.Cultures;
using Zonit.Extensions.Cultures.Repositories;
using Zonit.Extensions.Cultures.Services;

namespace Zonit.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCulturesExtension(this IServiceCollection services)
    {
        services.AddSingleton<TranslationRepository>();
        services.AddSingleton<DefaultTranslationRepository>();
        services.AddSingleton<MissingTranslationRepository>();

        services.AddSingleton<DetectCultureService>();

        services.AddScoped<ICultureRepository, CultureRepository>();
        services.AddScoped<ICultureProvider, CultureService>();

        return services;
    }
}