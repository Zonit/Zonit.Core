using System.Globalization;
using System.Reflection.Metadata;
using Zonit.Extensions.Cultures.Models;
using Zonit.Extensions.Cultures.Repositories;

namespace Zonit.Extensions.Cultures.Services;

public class CultureService : ICultureProvider
{

    private readonly TranslationRepository _translationRepository;
    private readonly MissingTranslationRepository _missingTranslationRepository;
    private readonly ICultureManager _cultureRepository;

    public CultureService(
        TranslationRepository translationRepository,
        MissingTranslationRepository missingTranslationRepository,
        ICultureManager cultureRepository
    )
    {
        _translationRepository = translationRepository;
        _missingTranslationRepository = missingTranslationRepository;
        _cultureRepository = cultureRepository;

        _cultureRepository.OnChange += HandleCultureRepositoryChange;

        GetCulture = _cultureRepository.GetCulture;
    }

    private DateTimeFormatModel _dateTimeFormat = new()
    {
        ShortDatePattern = CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern,
        ShortTimePattern = CultureInfo.CurrentCulture.DateTimeFormat.ShortTimePattern
    };

    public DateTimeFormatModel DateTimeFormat => _dateTimeFormat;

    public string GetCulture { get; private set; }

    public event Action? OnChange;

    private void HandleCultureRepositoryChange()
    {
        GetCulture = _cultureRepository.GetCulture;

        _dateTimeFormat = new()
        {
            ShortDatePattern = CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern,
            ShortTimePattern = CultureInfo.CurrentCulture.DateTimeFormat.ShortTimePattern
        };

        StateChanged();
    }

    public void StateChanged() => OnChange?.Invoke();

    // Do refaktoru
    public string Translate(string content, params object?[] args)
    {
        var varables = _translationRepository.GetAll().FirstOrDefault(x => x.Name == content);

        // Jeżeli jest tłumaczenie w bazie danych
        if (varables is not null)
        {
            var translates = varables.Translates?.SingleOrDefault(x => x.Culture == GetCulture) ?? null;

            // Jeżeli jest wymagany język
            if (translates is not null)
            {
                return args is not null ? string.Format(translates.Content, args) : translates.Content;
            }
            else // Developer code, TODO: do not use in production
            {
                // Jeżeli jest zmienna ale brak jej tłumaczenia
                var missing = _missingTranslationRepository.GetAll().FirstOrDefault(x =>
                    x.Name == content &&
                    
                        x.Translates is not null &&
                        x.Translates.Any(t => t.Culture == GetCulture) is false
                    
                );

                if (missing is not null && GetCulture != "en-us")
                {
                    // Jeżeli istnieje zmienna a nie ma języka to dodaje informację jakiego języka brakuje
                    missing.Translates?.Add(new() { Content = "", Culture = GetCulture });
                }
                else
                {
                    // Zapobiega przypadkowi gdzie jest już zmienna ale nie ma tłumaczenia, pokazywało się że nie ma również zmiennej 
                    if (_missingTranslationRepository.GetAll().Any(x => x.Name == content) is false && GetCulture != "en-us")
                        _missingTranslationRepository.Add(new(content, [ new() { Content = "", Culture = GetCulture } ]));
                }
            }
        }
        else // Developer code, TODO: do not use in production
        {
            // Jeżeli nie ma zmiennej w bazie danych oraz w liście braku tłumaczenia
            if (_missingTranslationRepository.GetAll().Any(x => x.Name == content) is false)
                _missingTranslationRepository.Add(new(content, [ new() { Content = "", Culture = "no varable" } ]));
        }

        return args is not null ? string.Format(content, args) : content;
    }

    public DateTime ClientTimeZone(DateTime utcDateTime)
    {
        var timeZone = TimeZoneInfo.FindSystemTimeZoneById(_cultureRepository.GetTimeZone);

        return TimeZoneInfo.ConvertTimeFromUtc(utcDateTime, timeZone);
    }
}