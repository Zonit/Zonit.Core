using Microsoft.Extensions.Hosting;
using Zonit.Extensions.Databases.Examples.Repositories;

namespace Zonit.Extensions.Databases.Examples.Events;

internal class BlogEvent(
    IBlogRepository _blogRepository,
    IBlogsRepository _blogsRepository
    ) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {



        await Task.CompletedTask;
    }
}
