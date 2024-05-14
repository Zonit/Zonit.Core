using Microsoft.AspNetCore.Components;
using Zonit.Extensions.Identity.Abstractions.Entities;
using Zonit.Extensions.Identity.Repositories;

namespace Zonit.Extensions;

public class ZonitIdentityExtension : ComponentBase, IDisposable
{
    [Inject]
    protected IAuthenticatedRepository authenticatedRepository { get; set; } = null!;

    [Inject]
    PersistentComponentState ApplicationState { get; set; } = null!;

    User? User { get; set; } = null!;
    PersistingComponentStateSubscription persistingSubscription;

    protected override void OnInitialized()
    {
        persistingSubscription = ApplicationState.RegisterOnPersisting(PersistData);

        if (!ApplicationState.TryTakeFromJson<User>("ZonitIdentityExtension", out var restored))
        {
            User = authenticatedRepository.User;
        }
        else
        {
            User = restored!;
        }

        if(User is not null)
            authenticatedRepository.Inicjalize(User);
    }

    private Task PersistData()
    {
        ApplicationState.PersistAsJson("ZonitIdentityExtension", User);

        return Task.CompletedTask;
    }

    void IDisposable.Dispose()
        => persistingSubscription.Dispose();
}