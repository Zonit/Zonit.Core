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
public interface IBlogRepository : IBaseRepository<Blog, Guid>
{ }

internal class BlogRepository(DatabaseContext _context) : BaseRepository<Blog, Guid>(_context), IBlogRepository
{ }

public interface IBlogsRepository : IBasesRepository<Blog>
{ }

internal class BlogsRepository(IDbContextFactory<DatabaseContext> _context) : BasesRepository<Blog, DatabaseContext>(_context), IBlogsRepository
{ }
```

**Create**
```cs
var create = await _blogRepository.AddAsync(new Blog
{
    Title = "Hello World",
    Content = "Example content"
});
```

**Read**
```cs
var read = await _blogRepository.GetFirstAsync(x => x.Title == "Hello World");
```

**Update**
```cs
var read = await _blogRepository.GetFirstAsync(x => x.Title == "Hello World");
read.Title = "New Title";
var update = await _blogRepository.UpdateAsync(read);
```

**Delete**
```cs
var delete = await _blogRepository.DeleteAsync(createBlog.Id);
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

**API IBaseRepository<T1, T2>**
```cs
Task<T1> AddAsync(T1 entity);
Task<T1?> GetAsync(T2 id);
Task<T1?> GetAsync(Expression<Func<T1, bool>> predicate);
Task<T1?> GetFirstAsync(Expression<Func<T1, bool>> predicate);
Task<bool> UpdateAsync(T1 entity);
Task<bool> DeleteAsync(T2 entity);
```

**API IBasesRepository<T>**
```cs
IBasesRepository<T> Skip(int skip);
IBasesRepository<T> Take(int take);
IBasesRepository<T> Include(Expression<Func<T, object>> includeExpression);
IBasesRepository<T> Where(Expression<Func<T, bool>> predicate);
IBasesRepository<T> OrderBy(Expression<Func<T, object>> keySelector);
IBasesRepository<T> OrderByDescending(Expression<Func<T, object>> keySelector);
IBasesRepository<T> Select(Expression<Func<T, T>> selector);
Task<IReadOnlyCollection<T>> GetAsync();
Task<int> GetCountAsync();
```

For more information, see the Examples project.