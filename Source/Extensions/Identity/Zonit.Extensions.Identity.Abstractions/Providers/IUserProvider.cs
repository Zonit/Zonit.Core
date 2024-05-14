using Zonit.Extensions.Identity.Abstractions.Models;

namespace Zonit.Extensions.Identity;

public interface IUserProvider
{
    public Task<UserModel?> GetByIdAsync(Guid id);
    public Task<UserModel?> GetByUserNameAsync(string userName);
}