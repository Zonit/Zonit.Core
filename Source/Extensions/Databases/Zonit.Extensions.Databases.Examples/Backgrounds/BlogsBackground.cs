using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Zonit.Extensions.Databases.Examples.Dto;
using Zonit.Extensions.Databases.Examples.Repositories;

namespace Zonit.Extensions.Databases.Examples.Backgrounds;

internal class BlogsBackground(
    IBlogsRepository _blogsRepository,
    ILogger<BlogsBackground> _logger
    ) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {

        using var repository = _blogsRepository;
        var blogs = await repository.GetAsync<BlogDto>();

        if(blogs is not null)
        {
            foreach (var blog in blogs)
            {
                _logger.LogInformation("Blog: {Id} {Title} {Content} {Created}", blog.Id, blog.Title, blog.Content, blog.Created);
            }
        }
        else
        {
            _logger.LogInformation("Blogs not found");
        }
    }
}