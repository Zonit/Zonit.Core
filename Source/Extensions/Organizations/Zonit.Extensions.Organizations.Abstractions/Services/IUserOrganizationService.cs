using Zonit.Extensions.Organizations.Abstractions.Entities;

namespace Zonit.Extensions.Organizations.Abstractions.Services;

/// <summary>
/// Retrieves information about the organization's user
/// </summary>
public interface IUserOrganizationService
{
    /// <summary>
    /// Information about the user's default organization. <br/>
    /// If the user is not in an organization then one is created.
    /// </summary>
    /// <param name="userId">User ID</param>
    /// <returns></returns>
    public Task<Organization?> GetDefaultAsync(Guid userId);

    /// <summary>
    /// Joins the organization with an invitation code
    /// </summary>
    /// <param name="userId">User ID</param>
    /// <param name="invitation">Invitation code</param>
    /// <returns></returns>
    public Task<bool> JoinAsync(Guid userId, Guid invitation);

    /// <summary>
    /// Changes the default organization
    /// </summary>
    /// <param name="userId">User ID</param>
    /// <param name="organizationId">Organization ID</param>
    /// <returns></returns>
    public Task<bool> SwitchDefaultAsync(Guid userId, Guid organizationId);

    /// <summary>
    /// View all user organizations
    /// </summary>
    /// <param name="identityId">User ID</param>
    /// <returns></returns>
    public Task<IReadOnlyCollection<Organization>?> GetOrganizationsAsync(Guid identityId);
}