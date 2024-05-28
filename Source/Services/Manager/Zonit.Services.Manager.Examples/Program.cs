using Microsoft.AspNetCore.Hosting.StaticWebAssets;
using Zonit.Extensions;
using Zonit.Extensions.Organizations;
using Zonit.Services.Manager.Examples.Services;
using Zonit.Services.Manager.Examples.Components;

namespace Zonit.Services.Manager.Examples
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddManagerService();
            builder.Services.AddTestPresentation();

            // Register test services
            builder.Services.AddTransient<IOrganizationManager, OrganizationService>();
            builder.Services.AddTransient<IUserOrganizationManager, UserOrganization>();
            //builder.Services.AddScoped<IWorkspaceManager, WorkspaceService>();

            //builder.WebHost.UseStaticWebAssets();

            StaticWebAssetsLoader.UseStaticWebAssets(builder.Environment, builder.Configuration);

            var app = builder.Build();

            app.UseStaticFiles();

            app.UseAntiforgery();
            app.MapRazorComponents<App>()
                .AddInteractiveServerRenderMode();

            app.UseManagerService();

            app.Run();
        }
    }
}
