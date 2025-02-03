namespace warren_analysis_desk;

public interface IBingNewsExtractorService
{
    Task<List<News>> GetBingNews();
}