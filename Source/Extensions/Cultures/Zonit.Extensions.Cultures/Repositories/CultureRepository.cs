using System.Globalization;

namespace Zonit.Extensions.Cultures.Repositories;

internal class CultureRepository : ICultureManager
{
    string _culture = "en-US";
    string _getTimeZone = "Europe/Warsaw";

    public string GetCulture => _culture;

    public string GetTimeZone => _getTimeZone;

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