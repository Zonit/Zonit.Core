using Zonit.Extensions.Website.Abstractions.Cookies.Models;

namespace Zonit.Extensions.Website.Abstractions.Cookies;

public interface ICookie
{
    public CookieModel? Get(string key);
    public CookieModel Set(string key, string value, int days = 12 * 30);
    public Task<CookieModel> SetAsync(string key, string value, int days = 12 * 30);
    public List<CookieModel> GetCookies();
    public void Inicjalize(List<CookieModel> cookies);
}
