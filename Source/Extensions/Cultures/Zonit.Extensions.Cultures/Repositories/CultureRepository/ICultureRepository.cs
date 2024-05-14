namespace Zonit.Extensions.Cultures.Repositories;

public interface ICultureRepository
{
    /// <summary>
    /// Changing the default language
    /// </summary>
    /// <param name="culture">Language parameter in BCP 47 standard</param>
    public void SetCulture(string culture);

    /// <summary>
    /// Returns the current culture in use in the BCP 47 standard
    /// </summary>
    public string GetCulture { get; }

    public event Action? OnChange;
}