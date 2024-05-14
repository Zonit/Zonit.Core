using System.Globalization;

namespace Zonit.Extensions.Cultures.Repositories;

internal class CultureRepository : ICultureRepository
{
    private string _culture = "en-US";

    public string GetCulture => _culture;

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

    public event Action? OnChange;
    public void StateChanged() => OnChange?.Invoke();
}

// TODO: Warto ogarnąć
// https://learn.microsoft.com/pl-pl/aspnet/core/blazor/globalization-localization?view=aspnetcore-8.0#dynamically-set-the-client-side-culture-by-user-preference