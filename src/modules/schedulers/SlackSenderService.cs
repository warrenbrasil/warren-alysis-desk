using Newtonsoft.Json.Linq;
using warren_analysis_desk;

public class SlackSchedulerService : BackgroundService
{
    private readonly IServiceScopeFactory _serviceScopeFactory;
    private readonly string OAuthToken;
    private readonly string ChannelId;

    public SlackSchedulerService(
        IServiceScopeFactory serviceScopeFactory, 
        IConfiguration configuration)
    {
        _serviceScopeFactory = serviceScopeFactory;
        OAuthToken = configuration["Slack:OAuthToken"];
        ChannelId = configuration["Slack:ChannelId"];
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                await Task.Delay(60000 * 30, stoppingToken);

                var slackNotifier = new SlackNotifier(OAuthToken);

                var bingNewsExtractorService = scope.ServiceProvider
                    .GetRequiredService<IBingNewsExtractorService>();
                
                var newsService = scope.ServiceProvider
                    .GetRequiredService<INewsService>();
                
                var bingNewsList = await bingNewsExtractorService.GetBingNews();

                var blocks = await new PrepareMessages().CheckboxNews(bingNewsList);

                var payload = new
                {
                    channel = ChannelId,
                    blocks
                };

                var resMsg = await slackNotifier.SendMessageAsync(payload);
                var jsonObj = JObject.Parse(resMsg);
                string messageTs = jsonObj["message"]?["ts"]?.ToString();

                var slackMessagesService = scope.ServiceProvider
                    .GetRequiredService<ISlackMessagesService>();

                var blockIds = jsonObj["message"]?["blocks"]?
                    .Select(block => (string)block["block_id"])
                    .ToList() ?? new List<string>();

                var newIds = bingNewsList
                    .Where(news => news.HtmlList != null && news.HtmlList.Any())
                    .Select(news => news.Id ?? 0)
                    .ToList();

                var pivotSlackMsgs = newIds.Zip(blockIds, (newId, blockId) => new SlackMessages
                {
                    IdNews = newId,
                    MessageId = messageTs,
                    BlockIds = blockId
                });

                foreach (var slackMessage in pivotSlackMsgs)
                {
                    await slackMessagesService.AddAsync(slackMessage);
                }

                Console.WriteLine("De 30 em 30 minutos.");
                // await Task.Delay(60000 * 30, stoppingToken);
            }
        }
    }
}