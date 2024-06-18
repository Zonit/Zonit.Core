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

            app.UseStatusCodePages(context =>
            {
                var response = context.HttpContext.Response;
                var request = context.HttpContext.Request;
                var pathBase = request.PathBase;

                switch (response.StatusCode)
                {
                    case 401:
                        response.Redirect($"{pathBase}/Errors/401");
                        break;
                    case 403:
                        response.Redirect($"{pathBase}/Errors/403");
                        break;
                    case 404:
                        response.Redirect($"{pathBase}/Errors/404");
                        break;
                    case 500:
                        response.Redirect($"{pathBase}/Errors/500");
                        break;
                }

                //response.Redirect("Errors/401");
                //response.HttpContext.Request.Path = "/Errors/403";
                //response.WriteAsync("Error 403 - Forbidden");

                return Task.CompletedTask;
            });

            app.UseCookiesExtension();
            app.UseCulturesExtension();
            app.UseIdentityExtension();
            app.UseOrganizationsExtension(); // It has to be after UseIdentityExtension!
            app.UseProjectsExtension();

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
