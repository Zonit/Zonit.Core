using Zonit.Extensions.Identity.Abstractions.Entities;
using Zonit.Extensions.Identity.Repositories;

namespace Zonit.Extensions.Identity.Services;

internal class AuthenticatedService(IAuthenticatedRepository _userRepository) : IAuthenticatedProvider
{
    public User? User { get => _userRepository.User; }
}