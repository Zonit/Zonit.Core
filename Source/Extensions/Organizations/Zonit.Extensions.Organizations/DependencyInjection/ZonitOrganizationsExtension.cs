using Microsoft.AspNetCore.Components;
using Zonit.Extensions.Organizations;

namespace Zonit.Extensions;

public sealed class ZonitOrganizationsExtension : ComponentBase, IDisposable
{
    [Inject]
    IWorkspaceManager Workspace { get; set; } = default!;

    [Inject]
    PersistentComponentState ApplicationState { get; set; } = default!;

    WorkspaceModel? _workspace { get; set; }

    PersistingComponentStateSubscription persistingSubscription;

    protected override async Task OnInitializedAsync()
    {
        persistingSubscription = ApplicationState.RegisterOnPersisting(PersistData);

        if (!ApplicationState.TryTakeFromJson<WorkspaceModel>("ZonitOrganizationsExtension", out var restored))
        {
            _workspace = await Workspace.InicjalizeAsync();
        }
        else
            _workspace = restored!;

        Workspace.Inicjalize(_workspace);
    }

    private Task PersistData()
    {
        ApplicationState.PersistAsJson("ZonitOrganizationsExtension", _workspace);

        return Task.CompletedTask;
    }

    public void Dispose()
        => persistingSubscription.Dispose();
}