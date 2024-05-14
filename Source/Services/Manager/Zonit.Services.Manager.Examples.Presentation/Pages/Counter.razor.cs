using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.VisualBasic;
using Zonit.Extensions.Website.Abstractions.Areas;

namespace Zonit.Services.Manager.Examples.Presentation.Pages;

//[Route("/Counter")]
[Route(Counter.Route)]
public partial class Counter : ComponentBase, IAreaManager
{
    public const string Route = "/Counter";
}