using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Zonit.Extensions.Databases.Abstractions.Options;
using Zonit.Extensions.Databases.Abstractions.Exceptions;

namespace Zonit.Extensions.Databases.SqlServer.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDbContextSqlServer<T>(this IServiceCollection services) where T : DbContext
    {
        services.AddDbContextFactory<T>((serviceProvider, options) =>
        {
            var databaseOptions = serviceProvider.GetRequiredService<IOptions<DatabaseOptions>>().Value;
            options.UseSqlServer($"Server={databaseOptions.Server};Database={databaseOptions.Name};user={databaseOptions.User};Password={databaseOptions.Password};{databaseOptions.Parameters}");
        });

        return services;
    }

    /// <summary>
    /// Inicjalize Database, config etc.
    /// Only register Program.cs
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection AddDatabase(this IServiceCollection services)
    {
        var serviceProvider = services.BuildServiceProvider();
        var configuration = serviceProvider.GetService<IConfiguration>();

        var databaseSection = configuration?.GetSection("Database");
        if (databaseSection is null || databaseSection.Exists() is false)
            throw new DatabaseException("Database configuration section not found.");
        
        services.AddOptions<DatabaseOptions>()
            .Configure<IConfiguration>(
                (options, configuration) =>
                    configuration.GetSection("Database").Bind(options));

        return services;
    }

    public static IServiceCollection AddAutomaticMigrations<T>(this IServiceCollection services)
    {

       return services;
    }
}
