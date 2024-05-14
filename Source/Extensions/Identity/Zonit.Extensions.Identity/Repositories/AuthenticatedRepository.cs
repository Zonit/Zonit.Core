using Zonit.Extensions.Identity.Abstractions.Models;

namespace Zonit.Extensions.Identity.Repositories;

internal class AuthenticatedRepository : IAuthenticatedRepository
{
    UserModel? _user;

    public UserModel? User => _user;

    public void Inicjalize(UserModel users)
        => _user = users;
}