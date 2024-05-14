using Microsoft.AspNetCore.Http;
using System.Globalization;
using Zonit.Extensions.Cultures.Repositories;
using Zonit.Extensions.Cultures.Services;

namespace Zonit.Extensions.Cultures.Middlewares;

internal class CultureMiddleware(RequestDelegate _next)
{
    public Task Invoke(HttpContext httpContext, DetectCultureService detectCultureService,  ICultureRepository _culture)
    {
        if (httpContext.Request.Path.Value is null)
            return _next(httpContext);
        
        var match = detectCultureService.GetUrl(httpContext.Request.Path.Value);

        /*
         * If path resembles a culture structure
         */
        if (match is not null)
        {
            var cultures = CultureInfo.GetCultures(CultureTypes.AllCultures);

            /*
             * If the culture is correct, protection against malicious attack.
             */
            if(cultures.Any(x => string.Equals(x.Name, match.Culture, StringComparison.OrdinalIgnoreCase)) is true)
            {
                /*
                 * If you are currently using a different culture than the url enforces
                 */
                if (CultureInfo.CurrentCulture.Name != match.Culture)
                {
                    httpContext.Response.Cookies.Append("Culture", match.Culture, new CookieOptions { Expires = DateTime.UtcNow.AddYears(1) });

                    var cultureInfo = new CultureInfo(match.Culture);

                    CultureInfo.CurrentCulture = cultureInfo;
                    CultureInfo.CurrentUICulture = cultureInfo;
                    //Thread.CurrentThread.CurrentCulture = cultureInfo;
                    //Thread.CurrentThread.CurrentUICulture = cultureInfo;

                    _culture.SetCulture(match.Culture);
                }

                /*
                 * Continues to routing
                 */
                httpContext.Request.Path = $"/{match.Url}";
            }

            // TOOD: Nie wiem czy tutaj nie powinno zwrócić błędu strony lub przekierować na domyślną wersję językową np angielską
        }
        else
        {
            // TODO: Przepisz tego ifa, na szybko robione

            var culture = httpContext.Request.Cookies["Culture"];

            if(culture is not null)
            {
                var cultureInfo = new CultureInfo(culture);

                CultureInfo.CurrentCulture = cultureInfo;
                CultureInfo.CurrentUICulture = cultureInfo;

                _culture.SetCulture(culture);

                return _next(httpContext);
            }

            var preferredLanguage = httpContext.Request.GetTypedHeaders().AcceptLanguage?.FirstOrDefault()?.Value.ToString();
            var cultureInfo2 = CultureInfo.GetCultureInfo(preferredLanguage ?? "en-us");

            CultureInfo.CurrentCulture = cultureInfo2;
            CultureInfo.CurrentUICulture = cultureInfo2;

            _culture.SetCulture(CultureInfo.CreateSpecificCulture(cultureInfo2.Name).Name.ToLower());
        }

        return _next(httpContext);
    }
}