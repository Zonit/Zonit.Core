using Zonit.Extensions.Identity.Abstractions.Models;

namespace Zonit.Extensions.Identity.Repositories;

public interface IAuthenticatedRepository
{
    public void Inicjalize(UserModel users);
    public UserModel? User { get; }
}