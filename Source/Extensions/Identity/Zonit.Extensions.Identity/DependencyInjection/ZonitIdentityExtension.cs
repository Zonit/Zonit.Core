using Microsoft.AspNetCore.Components;
using Zonit.Extensions.Identity.Abstractions.Entities;
using Zonit.Extensions.Identity.Repositories;

namespace Zonit.Extensions;

public sealed class ZonitIdentityExtension : ComponentBase, IDisposable
{
    [Inject]
    IAuthenticatedRepository _authenticatedRepository { get; set; } = default!;

    [Inject]
    PersistentComponentState ApplicationState { get; set; } = default!;

    User? User { get; set; } = null!;
    PersistingComponentStateSubscription persistingSubscription;

    protected override void OnInitialized()
    {
        persistingSubscription = ApplicationState.RegisterOnPersisting(PersistData);

        if (!ApplicationState.TryTakeFromJson<User>("ZonitIdentityExtension", out var restored))
            User = _authenticatedRepository.User;
        else
            User = restored!;

        if(User is not null)
            _authenticatedRepository.Inicjalize(User);
    }

    private Task PersistData()
    {
        ApplicationState.PersistAsJson("ZonitIdentityExtension", User);

        return Task.CompletedTask;
    }

    public void Dispose()
        => persistingSubscription.Dispose();
}