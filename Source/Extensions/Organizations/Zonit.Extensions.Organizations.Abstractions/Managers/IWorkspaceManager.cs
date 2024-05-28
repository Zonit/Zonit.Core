namespace Zonit.Extensions.Organizations;

/// <summary>
/// User state management in the organization
/// </summary>
public interface IWorkspaceManager
{
    /// <summary>
    /// Changes the state
    /// </summary>
    /// <param name="model"></param>
    public void Inicjalize(WorkspaceModel? model);

    /// <summary>
    /// Retrieves details of the user's organization
    /// </summary>
    public Task<WorkspaceModel> InicjalizeAsync();

    /// <summary>
    /// Change the default user organization
    /// </summary>
    /// <param name="organizationId"></param>
    /// <returns></returns>
    public Task SwitchOrganizationAsync(Guid organizationId);

    /// <summary>
    /// Get all user organizations
    /// </summary>
    /// <returns></returns>
    public Task<IReadOnlyCollection<OrganizationModel>?> GetOrganizationsAsync();

    /// <summary>
    /// Workspace data
    /// </summary>
    public WorkspaceModel? Workspace { get; }

    /// <summary>
    /// Event of changing the user organization
    /// </summary>
    public event Action? OnChange;
}