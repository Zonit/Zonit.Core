namespace Zonit.Extensions.Cultures;

public interface ICultureProvider
{
    /// <summary>
    /// Returns the current culture in use in the BCP 47 standard
    /// </summary>
    public string GetCulture { get; }

    /// <summary>
    /// Returns the translation in the current language used
    /// </summary>
    /// <param name="content">Search string, example: “Hello {0}”</param>
    /// <param name="args">Additional arguments, example: “User”</param>
    /// <returns></returns>
    public string Translate(string content, params object?[] args);

    /// <summary>
    /// Event that is triggered when the language is changed
    /// </summary>
    public event Action? OnChange;
}