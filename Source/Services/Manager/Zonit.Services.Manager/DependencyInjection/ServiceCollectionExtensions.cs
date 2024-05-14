using MudBlazor;
using MudBlazor.Services;
using Zonit.Extensions;
using Zonit.Extensions.Identity.Abstractions.Entities;
using Zonit.Extensions.Identity;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Zonit.Extensions.Identity.Abstractions.Models;

namespace Zonit.Services;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddManagerService(this IServiceCollection services)
    {
        services.AddHttpContextAccessor();

        services.AddCulturesExtension();
        services.AddIdentityExtension();
        services.AddCookiesExtension();
        services.AddNavigationsExtension();

        services.AddAntiforgery();
        services
            .AddRazorComponents()
            .AddInteractiveServerComponents();
            //.AddHubOptions(options =>
            //{
            //    options.ClientTimeoutInterval = TimeSpan.FromSeconds(30);
            //    options.EnableDetailedErrors = false;
            //    options.HandshakeTimeout = TimeSpan.FromSeconds(15);
            //    options.KeepAliveInterval = TimeSpan.FromSeconds(15);
            //    options.MaximumParallelInvocationsPerClient = 1;
            //    options.MaximumReceiveMessageSize = 4 * 128 * 1024;
            //    options.StreamBufferCapacity = 10;
            //});

        services.AddMudServices(config =>
        {
            config.SnackbarConfiguration.PositionClass = Defaults.Classes.Position.TopCenter;
            //config.SnackbarConfiguration.PreventDuplicates = false;
            //config.SnackbarConfiguration.NewestOnTop = false;
            //config.SnackbarConfiguration.ShowCloseIcon = true;
            //config.SnackbarConfiguration.VisibleStateDuration = 10000;
            //config.SnackbarConfiguration.HideTransitionDuration = 500;
            //config.SnackbarConfiguration.ShowTransitionDuration = 500;
            //config.SnackbarConfiguration.SnackbarVariant = Variant.Filled;
        });


        services.TryAddTransient<IUserProvider, UserTest>();
        services.TryAddTransient<ISessionProvider, SessionTest>();

        return services;
    }
}

public class UserTest : IUserProvider
{
    public async Task<UserModel?> GetByIdAsync(Guid id)
    {
        return new UserModel
        { 
            Name = "NoUser",
            DisplayRole = "Admin"
        };
    }

    public async Task<UserModel?> GetByUserNameAsync(string userName)
    {
        return new UserModel
        {
            Name = "NoUser",
            DisplayRole = "Admin"
        };
    }
}

public class SessionTest : ISessionProvider
{
    public async Task<UserModel?> GetByTokenAsync(string token)
    {
        return new UserModel
        {
            Name = "NoUser",
            DisplayRole = "Admin"
        };
    }
}