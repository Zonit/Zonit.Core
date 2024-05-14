namespace Zonit.Extensions.Cultures.Repositories;

public class MissingTranslationRepository : BaseRepository
{
    private static readonly Lazy<MissingTranslationRepository> instance = new(() => new MissingTranslationRepository());

    public static MissingTranslationRepository Instance => instance.Value;
}
