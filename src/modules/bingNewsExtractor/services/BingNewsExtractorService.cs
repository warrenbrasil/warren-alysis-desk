using System.Net;

namespace warren_analysis_desk
{
    public class BingNewsExtractorService : IBingNewsExtractorService
    {
        private readonly IRobotKeysRepository _robotKeysRepository;
        private readonly INewsRepository _newsRepository;
        private readonly string Token;

        public BingNewsExtractorService(
            IRobotKeysRepository robotKeysRepository,
            INewsRepository newsRepository,
            IConfiguration configuration)
        {
            _robotKeysRepository = robotKeysRepository;
            _newsRepository = newsRepository;
            Token = configuration["ChatGPT:Token"];
        }

        public async Task<List<News>> GetBingNews()
        {
            try
            {
                var newsList = new List<News>();
                var robotKeys = await _robotKeysRepository.GetAllAsync();

                foreach(var rks in robotKeys)
                {
                    foreach(var rk in rks.RobotKeysList)
                    { 
                        var (firstLink, firstTitle) = await new ExtractHtml().GetLinksAndTitles(rk);

                        if (firstLink != null && firstTitle != null)
                        {
                            var hrefValue = firstLink.GetAttributeValue("href", string.Empty);
                            var titleValue = WebUtility.HtmlDecode(firstTitle.InnerText.Trim());
                            
                            var existingNews = await _newsRepository.GetByTitleAsync(titleValue);
                            string shortenedUrl = await new LinkShortener().ShortenUrl(hrefValue);

                            var htmlDoc = await new WebFetcher().FetchNewsHtml(hrefValue);

                            if(htmlDoc != null)
                            {
                                var (textList, formattedText) = await new ExtractHtml().GetNewsHtmlList(htmlDoc);

                                var gptRes = textList.Any() ? await new ChatGptService(Token)
                                    .GetChatGptResponseAsync(
                                        $"Pode fazer um resumo do conteúdo dessas informações: \n [{formattedText}] \n em até 3 parágrafos. " 
                                        + "Pense que você está traduzindo notícias para trazer o máximo de informações relevantes a cerca da mesma. "
                                        + "Não quero que você me forneça um resumo que vai me atiçar a ler a notícia, mas sim um resumo que vai"
                                        +" me permitir obter todo o conhecimento referente a notícia para não precisar entrar na mesma"
                                    ) : "A página não contem elementos suficientes para gerar um resumo da notícia";

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
                                    var newObj = new News
                                    {
                                        Title = titleValue,
                                        Url = hrefValue,
                                        HtmlList = textList,
                                        ChatGPTMsg = gptRes,
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