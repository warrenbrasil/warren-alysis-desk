using DocumentFormat.OpenXml.Office2010.Excel;
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
                // await Task.Delay(60000 * 30, stoppingToken);
                // DateTime utcNow = DateTime.UtcNow;
                // TimeZoneInfo brasiliaTimeZone = TimeZoneInfo.FindSystemTimeZoneById("E. South America Standard Time");
                // DateTime brasiliaTime = TimeZoneInfo.ConvertTimeFromUtc(utcNow, brasiliaTimeZone);
                // if(brasiliaTime.ToString("HH") == "6")
                // {
                    //preparar relatório diário
                // }

                var slackNotifier = new SlackNotifier(OAuthToken, ChannelId);

                var bingNewsExtractorService = scope.ServiceProvider
                    .GetRequiredService<IBingNewsExtractorService>();
                
                var newsService = scope.ServiceProvider
                    .GetRequiredService<INewsService>();
                
                var bingNewsList = await bingNewsExtractorService.GetBingNews();

                var resMsg = await slackNotifier.SendMessageAsync(bingNewsList);
                var jsonObj = JObject.Parse(resMsg);
                string messageTs = jsonObj["message"]?["ts"]?.ToString();
                Console.WriteLine(messageTs);

                // var tasks = bingNewsList.Select(async i =>
                // {
                //     var newsDto = new NewsDto
                //     {
                //         MessageId = messageTs,
                //         RobotKeysId = i.RobotKeysId
                //     };
                    
                //     await newsService.UpdateAsync(i.Id ?? 0, newsDto);
                // }).ToList();

                // await Task.WhenAll(tasks);

                Console.WriteLine("De 30 em 30 minutos.");
                await Task.Delay(60000 * 30, stoppingToken);
            }
        }
    }
}
