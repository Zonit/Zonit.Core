using Zonit.Extensions.Organizations.Abstractions.Entities;

namespace Zonit.Extensions.Organizations;

public interface IWorkspaceProvider
{
    public Organization? Organization { get; }

    public event Action? OnChange;
}