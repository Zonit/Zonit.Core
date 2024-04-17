using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Zonit.Extensions.Databases.Examples.Entities;

namespace Zonit.Extensions.Databases.Examples.Data;

/// <summary>
/// Initialization of the database, checking whether there is a migration, whether there is basic data in the database.
/// </summary>
/// <param name="context"></param>
internal class DatabaseInitialize(IDbContextFactory<DatabaseContext> context) : IHostedService
{
    readonly DatabaseContext _context = context.CreateDbContext();

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        var peding = await _context.Database.GetPendingMigrationsAsync(cancellationToken);

        if (peding.Any() is true)
            await _context.Database.MigrateAsync(cancellationToken);

        await this.ExampleDataBlogs();

        await Task.CompletedTask;
    }

    async Task ExampleDataBlogs()
    {
        if (await _context.Blogs.AnyAsync())
            return;

        var currencies = new List<Blog>
        {
            new() { Title = "Hello World", Content = "Content..." },
        };

        _context.Blogs.AddRange(currencies);
        await _context.SaveChangesAsync();
    }

    public Task StopAsync(CancellationToken cancellationToken)
        => Task.CompletedTask;
}