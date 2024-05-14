using Microsoft.AspNetCore.Components;
using Zonit.Extensions.Cultures.Repositories;

namespace Zonit.Extensions;

public class ZonitCultureExtension : ComponentBase, IDisposable
{
    [Inject]
    protected ICultureRepository Culture { get; set; } = null!;

    [Inject]
    PersistentComponentState ApplicationState { get; set; } = null!;

    string CultureName { get; set; } = null!;
    PersistingComponentStateSubscription persistingSubscription;

    protected override void OnInitialized()
    {
        persistingSubscription = ApplicationState.RegisterOnPersisting(PersistData);

        if (!ApplicationState.TryTakeFromJson<string>("ZonitCultureExtension", out var restored))
        {
            CultureName = Culture.GetCulture;
        }
        else
        {
            CultureName = restored!;
        }

        Culture.SetCulture(CultureName);
    }

    private Task PersistData()
    {
        ApplicationState.PersistAsJson("ZonitCultureExtension", CultureName);

        return Task.CompletedTask;
    }

    void IDisposable.Dispose()
        => persistingSubscription.Dispose();
}