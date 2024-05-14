using Microsoft.AspNetCore.Hosting.StaticWebAssets;
using Zonit.Extensions;
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

            StaticWebAssetsLoader.UseStaticWebAssets(builder.Environment, builder.Configuration);

            var app = builder.Build();
            app.UseManagerService();
            app.Run();
        }
    }
}
