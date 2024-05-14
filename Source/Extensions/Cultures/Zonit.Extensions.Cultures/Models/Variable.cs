namespace Zonit.Extensions.Cultures.Models;

public class Variable
{
    public string Name { get; private set; }
    public string? Description { get; private set; }
    public List<Translate>? Translates { get; private set; }

    public Variable(string name)
        => Name = name;

    public Variable(string name, List<Translate> translates) : this(name)
        => Translates = translates;

    public Variable(string name, List<Translate> translates, string description) : this(name, translates)
        => Description = description;

    //public Variable AddTranslate(Translate translate)
    //{
    //    Translates?.Add(translate);
    //    return this;
    //}
}