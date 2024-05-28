namespace Zonit.Extensions.Organizations;

public class OrganizationModel
{
    public Guid Id { get; set; }

    /// <summary>
    /// Organization name
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Informacje o organizacji
    /// </summary>
    public FieldModel Field { get; set; } = new();

    /// <summary>
    /// Date of creation
    /// </summary>
    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
}