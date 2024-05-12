using Microsoft.AspNetCore.Components;
using Zonit.Extensions.Website.Abstractions.Cookies.Models;
using Zonit.Extensions.Website.Cookies.Repositories;

namespace Zonit.Extensions;

public class ZonitCookiesExtension : ComponentBase
{
    [Inject]
    protected ICookiesRepository Cookie { get; set; } = null!;

    [CascadingParameter]
    protected List<CookieModel> Cookies { get; set; } = null!;

    protected override void OnInitialized()
    {
        Cookie.Inicjalize(Cookies);
    }
}