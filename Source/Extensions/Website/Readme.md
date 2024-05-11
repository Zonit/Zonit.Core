## Useful tools for Blazor

**Nuget Package Abstraction**
```
Install-Package Zonit.Extensions.Website.Abstractions 
```

**Nuget Package Extensions**
```
Install-Package Zonit.Extensions.Website
```

## Cookie handling with support for Blazor

### Installation:
Add this in ``Routes.razor``
```razor
@using Zonit.Extensions.Website.Abstractions.Cookies
@using Zonit.Extensions.Website.Abstractions.Cookies.Models

@code {
    [Inject]
    ICookie Cookie { get; set; } = null!;

    [CascadingParameter]
    protected List<CookieModel> Cookies { get; set; } = new();

    protected override void OnInitialized()
    {
        Cookie.Inicjalize(Cookies);
    }
}
```

**Register:**

Services in ``Program.cs``
```cs
    builder.Services.AddCookiesExtension();
```
App in ``Program.cs``
```cs
    app.UseCookiesExtension();
```

### Example:

```razor
@page "/"

@rendermode InteractiveServer

@using Zonit.Extensions.Website.Abstractions.Cookies
@inject ICookie Cookie

@if(Cookie.GetCookies() is not null)
{
    @foreach (var cookie in Cookie.GetCookies())
    {
        <p>@cookie.Name @cookie.Value</p>
    }
}
```


**API**
```cs
    public CookieModel? Get(string key);
    public CookieModel Set(string key, string value, int days = 12 * 30);
    public Task<CookieModel> SetAsync(string key, string value, int days = 12 * 30);
    public List<CookieModel> GetCookies();
    public void Inicjalize(List<CookieModel> cookies);
```

We use SetAsync only in the Blazor circuit. It executes the JS code with the Cookies record.