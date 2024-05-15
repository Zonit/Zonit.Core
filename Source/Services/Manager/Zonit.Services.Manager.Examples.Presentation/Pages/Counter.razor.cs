using Microsoft.AspNetCore.Components;
using Zonit.Extensions.Website;

namespace Zonit.Services.Manager.Examples.Presentation.Pages;

//[Route("/Counter")]
[Route(Counter.Route)]
public partial class Counter : ComponentBase, IAreaManager
{
    public const string Route = "/Counter";
}