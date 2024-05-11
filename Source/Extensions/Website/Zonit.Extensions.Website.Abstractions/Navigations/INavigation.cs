using Zonit.Extensions.Website.Abstractions.Navigations.Models;
using Zonit.Extensions.Website.Abstractions.Navigations.Types;

namespace Zonit.Extensions.Website.Abstractions.Navigations;

public interface INavigation
{
    public void Add(NavGroupModel model);
    public IReadOnlyList<NavGroupModel>? Get(AreaType area);
}