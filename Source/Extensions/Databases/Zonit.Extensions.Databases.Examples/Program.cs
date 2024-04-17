using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Zonit.Extensions.Databases.Examples.Data;
using Zonit.Extensions.Databases.SqlServer.DependencyInjection;
using Zonit.Extensions.Databases.Examples.Repositories;

namespace Zonit.Extensions.Databases.Examples;

internal class Program
{
    static void Main(string[] args)
    {
        var builder = Host.CreateApplicationBuilder(new HostApplicationBuilderSettings
        {
            Args = args
        });

        builder.Services.AddDatabase();

        builder.Services.AddDbContextSqlServer<DatabaseContext>()
                        .AddHostedService<DatabaseInitialize>();    // TODO: Przenieś to do Databases.SqlServer

        builder.Services.AddTransient<IBlogRepository, BlogRepository>();
        builder.Services.AddTransient<IBlogsRepository, BlogsRepository>();

        var app = builder.Build();
        app.Run();
    }
}