using Microsoft.Extensions.Hosting;
using Zonit.Extensions.Cultures.Models;
using Zonit.Extensions.Cultures.Repositories;

namespace Zonit.Services.Manager.Examples.Presentation.Data;

internal class CultureData(TranslationRepository _translationRepository) : IHostedService
{
    public Task StartAsync(CancellationToken cancellationToken)
    {
        var variable = new List<Variable>()
        {
            // Nav
            new("Wallet", [
                new() { Culture = "en-us", Content = "Wallet"},
                new() { Culture = "pl-pl", Content = "Portfel"},
            ]),
        };

        _translationRepository.AddRange(variable);

        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
        => Task.CompletedTask;
}
