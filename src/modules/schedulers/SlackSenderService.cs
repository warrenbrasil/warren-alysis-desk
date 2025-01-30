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
                await Task.Delay(60000 * 30, stoppingToken);

                // var slackNotifier = new SlackNotifier(OAuthToken, ChannelId);
                // await slackNotifier.SendMessageAsync("Olá, Slack! 🚀");

                var googleNewsExtractorService = scope.ServiceProvider
                    .GetRequiredService<IGoogleNewsExtractorService>();
                
                var googleNewsList = await googleNewsExtractorService
                    .GetGoogleNews();
                
                foreach(var news in googleNewsList)
                {
                    var gptRes = await new ChatGptService(Token)
                        .GetChatGptResponseAsync(
                            $"Pode fazer um resumo do conteúdo do link a seguir: \n {news.Url} \n em até 3 parágrafos. " 
                            + "Pense que você está traduzindo notícias para trazer o máximo de informações relevantes a cerca da mesma. "
                            + "Não quero que você me forneça um resumo que vai me atiçar a ler a notícia, mas sim um resumo que vai"
                            +" me permitir obter todo o conhecimento referente a notícia para não precisar entrar na mesma"
                        );
                    
                    Console.WriteLine(gptRes);
                }

                Console.WriteLine("De 15 em 15 minutos.");
            }
        }
    }
}
