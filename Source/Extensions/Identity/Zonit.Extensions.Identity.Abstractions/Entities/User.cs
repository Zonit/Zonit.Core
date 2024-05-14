namespace Zonit.Extensions.Identity.Abstractions.Entities;

public class User
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string DisplayRole { get; set; } = "User";
    public string Avatar { get; set; } = string.Empty;

    public string? FullName => $"{FirstName} {LastName}";
    public string? FirstName { get; set; }
    public string? LastName { get; set; }

    public List<string>? Policy { get; set; }
    public List<string>? Roles { get; set; }

    // Credentials
    // Data stworzenia
    // Informacje
}
