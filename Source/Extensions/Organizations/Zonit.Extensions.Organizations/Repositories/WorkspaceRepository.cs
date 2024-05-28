namespace Zonit.Extensions.Organizations.Repositories;

internal class WorkspaceRepository(IUserOrganizationManager _userWorkspace) : IWorkspaceManager
{
    public event Action? OnChange;

    WorkspaceModel? _model;

    public WorkspaceModel? Workspace => _model;

    public void Inicjalize(WorkspaceModel? model = null)
        => _model = model;

    public async Task<WorkspaceModel> InicjalizeAsync()
        => await _userWorkspace.InicjalizeAsync();

    public async Task<IReadOnlyCollection<OrganizationModel>?> GetOrganizationsAsync()
        => await _userWorkspace.GetOrganizationsAsync();

    public async Task SwitchOrganizationAsync(Guid organizationId)
    {
        _model = await _userWorkspace.SwitchOrganizationAsync(organizationId);
        StateChanged();
    }

    public void StateChanged()
        => OnChange?.Invoke();
}
