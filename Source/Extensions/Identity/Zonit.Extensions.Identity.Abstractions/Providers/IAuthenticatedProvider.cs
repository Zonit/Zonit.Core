using Zonit.Extensions.Identity.Abstractions.Entities;

namespace Zonit.Extensions.Identity;

public interface IAuthenticatedProvider
{
    public User? User { get; }
}