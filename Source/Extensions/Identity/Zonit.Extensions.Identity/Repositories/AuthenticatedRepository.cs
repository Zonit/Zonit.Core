using Zonit.Extensions.Identity.Abstractions.Entities;

namespace Zonit.Extensions.Identity.Repositories;

internal class AuthenticatedRepository : IAuthenticatedRepository
{
    User? _user;

    public User? User => _user;

    public void Inicjalize(User users)
        => _user = users;
}