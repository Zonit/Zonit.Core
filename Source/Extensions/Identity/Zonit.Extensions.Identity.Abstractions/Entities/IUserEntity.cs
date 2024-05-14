namespace Zonit.Extensions.Identity.Abstractions.Entities;

public interface IUserEntity
{
    public User? User { get; }
    public Guid? UserId { get; set; }
}