using Zonit.Extensions.Cultures.Models;

namespace Zonit.Extensions.Cultures;

public interface ITranslationManager
{
    public void Add(Variable item);
    public void AddRange(List<Variable> items);
}