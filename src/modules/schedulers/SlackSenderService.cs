using warren_analysis_desk;

public class SlackSchedulerService : BackgroundService
{
    private readonly IServiceScopeFactory _serviceScopeFactory;
    private readonly string OAuthToken;
    private readonly string ChannelId;
    private readonly string Token;

    public SlackSchedulerService(
        IServiceScopeFactory serviceScopeFactory, 
        IConfiguration configuration)
    {
        _serviceScopeFactory = serviceScopeFactory;
        OAuthToken = configuration["Slack:OAuthToken"];
        ChannelId = configuration["Slack:ChannelId"];
        Token = configuration["ChatGPT:Token"];
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                // DateTime utcNow = DateTime.UtcNow;
                // TimeZoneInfo brasiliaTimeZone = TimeZoneInfo.FindSystemTimeZoneById("E. South America Standard Time");
                // DateTime brasiliaTime = TimeZoneInfo.ConvertTimeFromUtc(utcNow, brasiliaTimeZone);
                // if(brasiliaTime.ToString("HH") == "6")
                // {

                // }

                await Task.Delay(60000 * 30, stoppingToken);

                var slackNotifier = new SlackNotifier(OAuthToken, ChannelId);

                var googleNewsExtractorService = scope.ServiceProvider
                    .GetRequiredService<IGoogleNewsExtractorService>();
                
                var googleNewsList = await googleNewsExtractorService
                    .GetGoogleNews();
                
                string newsList = "";
                
                foreach(var news in googleNewsList)
                { 
                    if(news.HtmlList.Any()) 
                    {
                        newsList+=$"---------\nNotícia: {news.Title}\nLink: {news.ShortUrl}\nResumo: {news.ChatGPTMsg}\n---------";
                    }
                }

                await slackNotifier.SendMessageAsync(newsList);
                // await slackNotifier.SendMessageAsync("Olá, Slack! 🚀");
                // await Task.Delay(60000 * 30, stoppingToken);

                Console.WriteLine("De 15 em 15 minutos.");
            }
        }
    }
}
