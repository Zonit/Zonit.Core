using Microsoft.Extensions.DependencyInjection;
using Zonit.Extensions.Organizations;
using Zonit.Extensions.Organizations.Repositories;
using Zonit.Extensions.Organizations.Services;

namespace Zonit.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddOrganizationsExtension(this IServiceCollection services)
    {
        services.AddScoped<IWorkspaceManager, WorkspaceRepository>();
        services.AddScoped<IWorkspaceProvider, WorkspaceService>();

        return services;
    }
}