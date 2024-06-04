using Microsoft.AspNetCore.Hosting.StaticWebAssets;
using Zonit.Extensions.Organizations;
using Zonit.Services.Manager.Examples.Services;
using Zonit.Services.Manager.Examples.Components;
using Zonit.Extensions.Identity;
using Zonit.Extensions;

namespace Zonit.Services.Manager.Examples
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            //// Zastanów siê nad tym gdzie powinno byc
            //builder.Services.AddCulturesExtension();
            //builder.Services.AddIdentityExtension();
            //builder.Services.AddCookiesExtension();
            //builder.Services.AddOrganizationsExtension();
            //builder.Services.AddNavigationsExtension();


            builder.Services.AddManagerService();
            builder.Services.AddTestPresentation();

            // Register test services
            builder.Services.AddTransient<IOrganizationManager, OrganizationService>();
            builder.Services.AddTransient<IUserOrganizationManager, UserOrganization>();
            //builder.Services.AddScoped<IWorkspaceManager, WorkspaceService>();

            builder.Services.AddTransient<IUserProvider, UserService>();
            builder.Services.AddTransient<ISessionProvider, SessionService>();

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
