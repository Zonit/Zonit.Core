namespace Zonit.Extensions.Organizations;

public interface IUserOrganizationManager
{
    public Task<WorkspaceModel> InicjalizeAsync();
    public Task<IReadOnlyCollection<OrganizationModel>?> GetOrganizationsAsync();
    public Task<WorkspaceModel?> SwitchOrganizationAsync(Guid organizationId);
}
