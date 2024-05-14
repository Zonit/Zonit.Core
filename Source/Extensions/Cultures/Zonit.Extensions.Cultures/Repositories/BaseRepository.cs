using Zonit.Extensions.Cultures.Models;

namespace Zonit.Extensions.Cultures.Repositories;

public abstract class BaseRepository
{
    protected List<Variable> Items = [];

    public void Add(Variable item)
        => Items.Add(item);

    public void AddRange(List<Variable> items)
        => Items.AddRange(items);
    
    public IReadOnlyCollection<Variable> GetAll()
        => Items;

    public void Clear()
        => Items.Clear();
}