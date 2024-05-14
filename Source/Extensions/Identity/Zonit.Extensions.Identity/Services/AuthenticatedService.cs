using Zonit.Extensions.Identity.Abstractions.Models;
using Zonit.Extensions.Identity.Repositories;

namespace Zonit.Extensions.Identity.Services;

internal class AuthenticatedService(IAuthenticatedRepository _userRepository) : IAuthenticatedProvider
{
    public UserModel? User { get => _userRepository.User; }
}