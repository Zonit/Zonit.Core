using System.Globalization;

namespace Zonit.Extensions.Cultures.Repositories;

internal class CultureRepository : ICultureManager
{
    string _culture = "en-US";
    string _getTimeZone = "Europe/Warsaw";
    string[] _supportedCultures = [
        "en-us",
        "ar-sa",
        "fr-fr",
        "de-de",
        "es-es",
        "it-it",
        "nl-nl",
        "sv-se",
        "da-dk",
        "no-no",
        "fi-fi",
        "ru-ru",
        "pl-pl",
        "cs-cz",
        "hu-hu",
        "sk-sk",
        "pt-pt"
        ];

    public string GetCulture => _culture;

    public string GetTimeZone => _getTimeZone;

    // TODO: Jest to zła implementacja, jest to SCOPE a powinno być SINGLETON. Język raczej nie będzie tylko dla konkretnego użytkownika lecz dla całej aplikacji
    // Myślę że można zrobić nową klasę która będzie zajmowała się ogólnymi ustawieniami dla całej aplikacji
    public string[] SupportedCultures => _supportedCultures;

    public void SetCulture(string culture)
    {
        var cultureInfo = new CultureInfo(culture);

        _culture = CultureInfo.CreateSpecificCulture(cultureInfo.Name).Name.ToLower();

        CultureInfo.CurrentCulture = cultureInfo;
        CultureInfo.CurrentUICulture = cultureInfo;
        //CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
        //CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;

        StateChanged();
    }

    public void SetTimeZone(string timeZone)
    {
        _getTimeZone = timeZone;

        StateChanged();
    }

    public event Action? OnChange;

    public void StateChanged() 
        => OnChange?.Invoke();
}

// TODO: Warto ogarnąć
// https://learn.microsoft.com/pl-pl/aspnet/core/blazor/globalization-localization?view=aspnetcore-8.0#dynamically-set-the-client-side-culture-by-user-preference