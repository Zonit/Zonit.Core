namespace Zonit.Extensions.Organizations;

/// <summary>
/// Current user organization
/// </summary>
public interface IWorkspaceProvider
{
    /// <summary>
    /// Detailed information about the organization, if blank it does not belong to any organization or the module does not work.
    /// </summary>
    public OrganizationModel? Organization { get; }

    /// <summary>
    /// Checks if the user has access to permissions
    /// </summary>
    /// <param name="permission"></param>
    /// <returns></returns>
    public bool IsPermission(string permission);

    /// <summary>
    /// Checks whether the user has the given role
    /// </summary>
    /// <param name="role"></param>
    /// <returns></returns>
    public bool IsRole(string role);

    /// <summary>
    /// Event of changing the user organization
    /// </summary>
    public event Action? OnChange;
}