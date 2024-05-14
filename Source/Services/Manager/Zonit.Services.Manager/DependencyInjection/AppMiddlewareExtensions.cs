using Zonit.Extensions;
using Zonit.Services.Manager.Areas.Manager;

namespace Zonit.Services;

public static class AppMiddlewareExtensions
{
    public static IApplicationBuilder UseManagerService(this WebApplication builder)
    {
        builder.MapWhen(ctx => ctx.Request.Path.StartsWithSegments("/manager"), app =>
        {
            app.UsePathBase("/manager/");
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAntiforgery();

            app.UseIdentityExtension();
            app.UseCookiesExtension();
            app.UseCultureExtension();

            app.UseEndpoints(endpoints =>
            {
                var route = new Routes();
                var assembly = route.Types().ToArray();

                endpoints.MapRazorComponents<App>()
                    .AddInteractiveServerRenderMode()
                    .AddAdditionalAssemblies(assembly);
            });
        });

        return builder;
    }
}
