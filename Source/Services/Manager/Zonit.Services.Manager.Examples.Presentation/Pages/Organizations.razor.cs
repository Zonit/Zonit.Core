using Microsoft.AspNetCore.Components;
using Zonit.Extensions.Website;
using Zonit.Extensions.Cultures;
using Zonit.Extensions.Organizations;
using Microsoft.AspNetCore.Authorization;

namespace Zonit.Services.Manager.Examples.Presentation.Pages;

[Route("/" + Organizations.Route), Authorize]
sealed partial class Organizations : ComponentBase, IAreaManager, IDisposable
{
    public const string Route = "Organizations";

    [Inject]
    ICultureProvider Culture { get; set; } = default!;

    [Inject]
    IWorkspaceProvider Workspace { get; set; } = default!;

    protected override void OnInitialized()
    {
        Culture.OnChange += StateHasChanged;
        Workspace.OnChange += StateHasChanged;
    }

    public void Dispose()
    {
        Culture.OnChange -= StateHasChanged;
        Workspace.OnChange -= StateHasChanged;
    }
}