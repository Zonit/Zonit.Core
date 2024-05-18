using Zonit.Extensions.Organizations.Abstractions.Entities;

namespace Zonit.Extensions.Organizations;

public interface IOrganizationProvider
{
    public Organization? Default { get; }
}