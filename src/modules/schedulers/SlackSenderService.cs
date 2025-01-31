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
                // DateTime utcNow = DateTime.UtcNow;
                // TimeZoneInfo brasiliaTimeZone = TimeZoneInfo.FindSystemTimeZoneById("E. South America Standard Time");
                // DateTime brasiliaTime = TimeZoneInfo.ConvertTimeFromUtc(utcNow, brasiliaTimeZone);
                // if(brasiliaTime.ToString("HH") == "6")
                // {
                    //preparar relatório diário
                // }

                var slackNotifier = new SlackNotifier(OAuthToken, ChannelId);

                var googleNewsExtractorService = scope.ServiceProvider
                    .GetRequiredService<IGoogleNewsExtractorService>();
                
                var googleNewsList = await googleNewsExtractorService.GetGoogleNews();

                await slackNotifier.SendMessageAsync(googleNewsList);

                await Task.Delay(60000 * 30, stoppingToken);
                Console.WriteLine("De 30 em 30 minutos.");
            }
        }
    }
}
