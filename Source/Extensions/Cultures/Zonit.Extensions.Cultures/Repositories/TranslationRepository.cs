namespace Zonit.Extensions.Cultures.Repositories;

public class TranslationRepository : BaseRepository
{
    private static readonly Lazy<TranslationRepository> instance = new(() => new TranslationRepository());

    public static TranslationRepository Instance => instance.Value;
}
