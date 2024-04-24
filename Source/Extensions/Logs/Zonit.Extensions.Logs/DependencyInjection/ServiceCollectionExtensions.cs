using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Formatting.Json;

namespace Zonit.Extensions;

public static class ServiceCollectionExtensions
{
    // TODO: Dodaj opcje, w nich np wyłączenie wszystkich logów
    public static IServiceCollection AddLogs(this IServiceCollection services)
    {
        var serviceProvider = services.BuildServiceProvider();
        var configuration = serviceProvider.GetService<IConfiguration>();

        if (configuration is null)
            return services;

        var logger = new LoggerConfiguration()
            .ReadFrom.Configuration(configuration)
            .Enrich.FromLogContext()
            .WriteTo.File(formatter: new JsonFormatter(renderMessage: true),
                path: Path.Combine(Environment.CurrentDirectory, "Logs", ".txt"),
                rollingInterval: RollingInterval.Day,
                rollOnFileSizeLimit: true,
                fileSizeLimitBytes: 1024 * 1024 * 10, // 10 Mb
                shared: true)
            .WriteTo.Console();

        var logLevels = configuration.GetSection("Logging:LogLevel").GetChildren();
        foreach (var logLevel in logLevels)
        {
            var category = logLevel.Key;
            var level = logLevel.Value;

            if (level is null)
                continue;

            logger.MinimumLevel.Override(category, (Serilog.Events.LogEventLevel)Enum.Parse(typeof(Serilog.Events.LogEventLevel), level));
        }

        services.AddLogging(loggingBuilder =>
        {
            loggingBuilder.ClearProviders();
            loggingBuilder.AddSerilog(logger.CreateLogger(), true);
        });

        return services;
    }
}