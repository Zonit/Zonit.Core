## The extension facilitates CRUD creation in databases.

It speeds up the work of creating repositories, with the help of abstractions and interface handle the connection to the database.

**Nuget Package**
```
Install-Package Zonit.Extensions.Databases.SqlServer
```

### Example:
**Model**
```cs
public class Blog
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public DateTime Created { get; private set; } = DateTime.UtcNow;
}
```

**Repository**
```cs
public interface IBlogRepository : IDatabaseRepository<Blog, Guid>
{ }

internal class BlogRepository(DatabaseContext _context) : DatabaseRepository<Blog, Guid>(_context), IBlogRepository
{ }

public interface IBlogsRepository : IDatabasesRepository<Blog>
{ }

internal class BlogsRepository(IDbContextFactory<DatabaseContext> _context) : DatabasesRepository<Blog, DatabaseContext>(_context), IBlogsRepository
{ }
```

**Register**
```cs
    builder.Services.AddDbSqlServer<DatabaseContext>();

    builder.Services.AddTransient<IBlogRepository, BlogRepository>();
    builder.Services.AddTransient<IBlogsRepository, BlogsRepository>();
```

**Create**
```cs
var blog = await _blogRepository.AddAsync(new Blog
{
    Title = "Hello World",
    Content = "Example content"
});
```

**Read**
```cs
var blogSingle = await _blogRepository.GetAsync(x => x.Title == "Hello World");
var blogFirst = await _blogRepository.GetFirstAsync(x => x.Title == "Hello World");
```

**Update**
```cs
var blog = await _blogRepository.GetFirstAsync(x => x.Title == "Hello World");
blog.Title = "New Title";
var update = await _blogRepository.UpdateAsync(blog);
```

**Delete**
```cs
var delete = await _blogRepository.DeleteAsync(blog.Id);
```

**Read All**
```cs
using var repository = _blogsRepository;
var blogs = await repository.GetAsync();

foreach (var blog in blogs)
{
...
}
```

**API IDatabaseRepository<T1, T2>**
```cs
Task<T1> AddAsync(T1 entity);
Task<T1?> GetAsync(T2 id);
Task<T1?> GetAsync(Expression<Func<T1, bool>> predicate);
Task<T1?> GetFirstAsync(Expression<Func<T1, bool>> predicate);
Task<bool> UpdateAsync(T1 entity);
Task<bool> DeleteAsync(T2 entity);
```

**API IDatabasesRepository<T1, T2>**
```cs
IDatabasesRepository<T> Skip(int skip);
IDatabasesRepository<T> Take(int take);
IDatabasesRepository<T> Include(Expression<Func<T, object>> includeExpression);
IDatabasesRepository<T> Where(Expression<Func<T, bool>> predicate);
IDatabasesRepository<T> OrderBy(Expression<Func<T, object>> keySelector);
IDatabasesRepository<T> OrderByDescending(Expression<Func<T, object>> keySelector);
IDatabasesRepository<T> Select(Expression<Func<T, T>> selector);
Task<IReadOnlyCollection<T>> GetAsync();
Task<int> GetCountAsync();
```

For more information, see the Examples project.