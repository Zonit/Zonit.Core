//using Zonit.Extensions.Organizations;

//namespace Zonit.Services.Manager.Examples.Services;

//public class WorkspaceService : IWorkspaceManager
//{
//    public event Action? OnChange;

//    WorkspaceModel? _model;

//    public WorkspaceModel? Workspace => _model;

//    public void Inicjalize(WorkspaceModel? model = null)
//        => _model = model;

//    public async Task<WorkspaceModel> InicjalizeAsync()
//    {
//        _model = new WorkspaceModel
//        {
//            Organization = OrganizationService.organizations?.SingleOrDefault(x => x.Id == Guid.Parse("266ac900-8940-4b53-bb71-64a164b5d73e")),
//            Permissions = ["Permission1", "Permission2"],
//            Roles = ["Role1", "Role2"]
//        };

//        return await Task.FromResult(_model);
//    }

//    public async Task<IReadOnlyCollection<OrganizationModel>?> GetOrganizationsAsync()
//        => await Task.FromResult(OrganizationService.organizations);

//    public async Task SwitchOrganizationAsync(Guid organizationId)
//    {
//        // TODO: Zrób weryfikację czy użytkownik posiada organizację

//        var organization = OrganizationService.organizations?.SingleOrDefault(x => x.Id == organizationId);

//        if(organization is not null)
//        {
//            _model = new WorkspaceModel
//            {
//                Organization = OrganizationService.organizations?.SingleOrDefault(x => x.Id == organizationId),
//                Permissions = ["Permission1", "Permission2"],
//                Roles = ["Role1", "Role2"]
//            };
//        }

//        StateChanged();

//        await Task.CompletedTask;
//    }

//    public void StateChanged()
//        => OnChange?.Invoke();
//}
