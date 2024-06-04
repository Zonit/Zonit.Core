using Zonit.Extensions.Identity;

namespace Zonit.Services.Manager.Examples.Services;

public class UserService : IUserProvider
{
    UserModel? UserModel { get; set; } = new()
    {
        Name = "NoUser",
        Roles = ["User", "TwojaStara"]
    };

    public Task<UserModel?> GetByIdAsync(Guid id)
        => Task.FromResult(UserModel);

    public Task<UserModel?> GetByUserNameAsync(string userName)
        => Task.FromResult(UserModel);
}
