namespace Zonit.Extensions.Website.Abstractions.Cookies.Models;

public class CookieModel(string name, string value)
{
    public string Name { get; set; } = name;
    public string Value { get; set; } = value;
}