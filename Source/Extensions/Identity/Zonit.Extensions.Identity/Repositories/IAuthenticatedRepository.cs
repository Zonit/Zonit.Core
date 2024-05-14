using Zonit.Extensions.Identity.Abstractions.Entities;

namespace Zonit.Extensions.Identity.Repositories;

public interface IAuthenticatedRepository
{
    public void Inicjalize(User users);
    public User? User { get; }
}