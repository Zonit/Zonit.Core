using Zonit.Extensions.Website.Abstractions.Cookies.Models;
using Zonit.Extensions.Website.Abstractions.Cookies;
using Microsoft.JSInterop;

namespace Zonit.Extensions.Website.Cookies.Services;

public class CookieService(IJSRuntime _runtime) : ICookie
{
    List<CookieModel> Cookies { get; set; } = [];

    public CookieModel? Get(string key)
        => this.Cookies?
            .SingleOrDefault(x => x.Name.Equals(key, StringComparison.OrdinalIgnoreCase));

    public List<CookieModel> GetCookies()
        => this.Cookies;

    public async Task<CookieModel> SetAsync(string key, string value, int days = 360)
    {
        var model = this.Set(key, value, days);

        if (_runtime is not null)
            await _runtime.InvokeVoidAsync("eval", $"document.cookie = '{key}={value}; expires= {DateTime.UtcNow.AddDays(days):R}; path=/';");

        return model;
    }

    public CookieModel Set(string key, string value, int days = 360)
    {
        if (this.Cookies.Any(x => x.Name == key))
            this.Cookies.RemoveAll(x => x.Name == key);

        var model = new CookieModel(key, value);

        this.Cookies.Add(model);

        return model;
    }

    public void Inicjalize(List<CookieModel> cookies)
        => Cookies = cookies;
}