using Zonit.Extensions.Identity.Abstractions.Entities;

namespace Zonit.Extensions.Identity;

public interface IUserProvider
{
    public Task<User?> GetByIdAsync(Guid id);
    public Task<User?> GetByUserNameAsync(string userName);
}