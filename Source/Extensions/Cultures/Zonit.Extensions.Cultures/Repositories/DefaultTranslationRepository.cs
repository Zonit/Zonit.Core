namespace Zonit.Extensions.Cultures.Repositories;

public class DefaultTranslationRepository : BaseRepository
{
    private static readonly Lazy<DefaultTranslationRepository> instance = new(() => new DefaultTranslationRepository());

    public static DefaultTranslationRepository Instance => instance.Value;
}