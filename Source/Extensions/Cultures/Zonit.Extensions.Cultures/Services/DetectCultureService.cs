using System.Text.RegularExpressions;

namespace Zonit.Extensions.Cultures.Services;

internal partial class DetectCultureService
{
    public record PathCulture(string Url, string Culture);

    public PathCulture? GetUrl(string adress)
    {
        var match = GetUrlRegex().Match(adress);

        if(match.Success)
            return new PathCulture(match.Groups["url"].Value, match.Groups["culture"].Value.ToLower());

        return null;
    }

    public void Test()
    {

    }

    [GeneratedRegex(@"^/(?<culture>[a-z]{2}(?:-[a-z]{2})?)/(?<url>.+)", options: RegexOptions.Compiled | RegexOptions.IgnoreCase, matchTimeoutMilliseconds: 1000)]
    private static partial Regex GetUrlRegex();
}
