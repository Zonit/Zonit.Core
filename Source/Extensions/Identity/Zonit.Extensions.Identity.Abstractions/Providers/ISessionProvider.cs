using Zonit.Extensions.Identity.Abstractions.Models;

namespace Zonit.Extensions.Identity;

// TODO: Nie podoba mi się to, powinno zwracać sesję a nie użytkownika
public interface ISessionProvider
{
    public Task<UserModel?> GetByTokenAsync(string token);
}