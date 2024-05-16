using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Zonit.Extensions.Website;

namespace Zonit.Services.Manager.Examples.Presentation.Pages;

//[Route("/Counter")]
[Route(Counter2.Route)]
public partial class Counter2 : ComponentBase, IAreaManager
{
    public const string Route = "/Counter2";
}