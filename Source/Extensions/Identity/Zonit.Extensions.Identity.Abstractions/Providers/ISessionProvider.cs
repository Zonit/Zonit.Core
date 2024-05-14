using Zonit.Extensions.Identity.Abstractions.Entities;

namespace Zonit.Extensions.Identity;

// TODO: Nie podoba mi się to, powinno zwracać sesję a nie użytkownika
public interface ISessionProvider
{
    public Task<User?> GetByTokenAsync(string token);
}