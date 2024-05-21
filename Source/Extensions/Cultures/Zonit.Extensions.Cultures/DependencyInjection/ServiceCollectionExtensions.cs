using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Zonit.Extensions.Cultures;
using Zonit.Extensions.Cultures.Abstractions.Options;
using Zonit.Extensions.Cultures.Repositories;
using Zonit.Extensions.Cultures.Services;

namespace Zonit.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCulturesExtension(this IServiceCollection services)
    {
        services.AddOptions<CultureOption>()
            .Configure<IConfiguration>(
                (options, configuration) =>
                    configuration.GetSection("Culture").Bind(options));

        services.AddSingleton<TranslationRepository>();
        services.AddSingleton<DefaultTranslationRepository>();
        services.AddSingleton<MissingTranslationRepository>();

        services.AddSingleton<ITranslationManager, TranslationService>();

        services.AddSingleton<DetectCultureService>();

        services.AddScoped<ICultureManager, CultureRepository>();
        services.AddScoped<ICultureProvider, CultureService>();

        return services;
    }
}