using Zonit.Extensions.Identity.Abstractions.Models;

namespace Zonit.Extensions.Identity;

public interface IAuthenticatedProvider
{
    public UserModel? User { get; }
}