namespace warren_analysis_desk
{
    public class GoogleNewsExtractorService : IGoogleNewsExtractorService
    {
        private readonly IRobotKeysRepository _robotKeysRepository;
        private readonly INewsRepository _newsRepository;

        public GoogleNewsExtractorService(
            IRobotKeysRepository robotKeysRepository,
            INewsRepository newsRepository)
        {
            _robotKeysRepository = robotKeysRepository;
            _newsRepository = newsRepository;
        }

        public async Task<List<News>> GetGoogleNews()
        {
            try
            {
                var newsList = new List<News>();
                var robotKeys = await _robotKeysRepository.GetAllAsync();

                foreach(var rks in robotKeys)
                {
                    foreach(var rk in rks.RobotKeysList)
                    { 
                        var lt = await new ExtractHtml().GetLinksAndTitles(rk);
                        var firstLink = lt.firstLink;
                        var firstTitle = lt.firstTitle;

                        if (firstLink != null && firstTitle != null)
                        {
                            var hrefValue = firstLink.GetAttributeValue("href", string.Empty);
                            var titleValue = firstTitle.GetAttributeValue("aria-label", string.Empty);

                            var existingNews = await _newsRepository
                                .GetByTitleAsync(titleValue
                                    .Replace("Mais -", "").Trim()
                                    .Replace("; entenda", ""));

                            string url = $"https://news.google.com{hrefValue.TrimStart('.')}";
                            string shortenedUrl = await new LinkShortener().ShortenUrl(url);

                            if (existingNews.Any())
                            {
                                var newsToUpdate = existingNews.FirstOrDefault();
                                if (newsToUpdate != null)
                                {
                                    newsToUpdate.EndExecution = DateTime.Now;  
                                    await _newsRepository.UpdateAsync(newsToUpdate); 
                                }
                            }
                            else
                            {
                                var newsLists = new List<string>();
                                var newObj = new News
                                {
                                    Title = titleValue
                                        .Replace("Mais -", "").Trim()
                                        .Replace("; entenda", ""),
                                    Url = url,
                                    ShortUrl = shortenedUrl,
                                    RobotName = rks.KeyName,
                                    PublishDate = DateTime.Now,
                                    StartExecution = DateTime.Now,
                                    EndExecution = DateTime.Now, 
                                    RobotKeysId = rks.Id ?? 0,
                                };

                                var insertedNews = await _newsRepository.AddAsync(newObj);  
                                newObj.Id = insertedNews.Id;
                                newsList.Add(newObj);
                            }
                        }
                    }
                }

                return newsList;
            }
            catch (HttpRequestException ex)
            {
                throw new Exception($"Error fetching news: {ex.Message}", ex);
            }
        }
    }
}