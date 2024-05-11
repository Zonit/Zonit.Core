using Zonit.Extensions.Website.Abstractions.Navigations;
using Zonit.Extensions.Website.Abstractions.Navigations.Models;
using Zonit.Extensions.Website.Abstractions.Navigations.Types;

namespace Zonit.Extensions.Website.Navigations.Services;

internal class NavigationService : INavigation
{
    IList<NavGroupModel> _navigationModels = [];

    public IReadOnlyList<NavGroupModel>? Get(AreaType area)
        => _navigationModels?
            .Where(x => x.Area == area)
            .OrderBy(x => x.Order)
            .ToList();

    public void Add(NavGroupModel model)
        => _navigationModels.Add(model);

}