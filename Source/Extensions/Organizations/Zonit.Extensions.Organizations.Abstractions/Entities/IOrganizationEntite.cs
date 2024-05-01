namespace Zonit.Extensions.Organizations.Abstractions.Entities;

public interface IOrganizationEntite
{
    /// <summary>
    /// Organization ID
    /// </summary>
    public Guid? OrganizationId { get; set; }

    /// <summary>
    /// Data of the organization
    /// </summary>
    public Organization? Organization { get; }
}
