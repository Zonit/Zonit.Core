namespace Zonit.Extensions.Identity.Abstractions.Models;

public class CredentialModel
{
    public string Method { get; set; } = string.Empty;
    public string Value { get; set; } = string.Empty;
}