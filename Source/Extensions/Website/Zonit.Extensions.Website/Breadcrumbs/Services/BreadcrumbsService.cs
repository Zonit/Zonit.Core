namespace Zonit.Extensions.Website.Breadcrumbs.Services;

internal class BreadcrumbsService : IBreadcrumbsProvider
{
    IList<BreadcrumbsModel> _breadcrumbsModels = [];

    public IReadOnlyList<BreadcrumbsModel> Get()
        => _breadcrumbsModels.ToList();

    public void Add(IList<BreadcrumbsModel> breadcrumbs)
        => _breadcrumbsModels = breadcrumbs;
}