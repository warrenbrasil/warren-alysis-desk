namespace warren_analysis_desk;
public class SlackMessagesService : ISlackMessagesService
{
    private readonly ISlackMessagesRepository _slackMessagesRepository;

    public SlackMessagesService(
        ISlackMessagesRepository slackMessagesRepository)
        {
            _slackMessagesRepository = slackMessagesRepository;
        }

    public async Task<byte[]> GetReportAsync(string SlackUserId)
    {
        var reportContent = await _slackMessagesRepository.GetReportAsync(SlackUserId);
        string txt = $"";

        foreach(var rcs in reportContent)
        {
            txt += $"Notícia: {rcs.Title}\nLink: {rcs.ShortUrl}\nResumo {rcs.ChatGPTMsg}\nData: {rcs.PublishDate}\n\n";
        }

        using (var memoryStream = new MemoryStream())

        
        using (var writer = new StreamWriter(memoryStream))
        {
            writer.WriteLine(txt);

            writer.Flush();
            
            return memoryStream.ToArray(); 
        }
    }

    public async Task<List<ReportItem>> GetAllAsync(string SlackUserId)
    {
        return await _slackMessagesRepository.GetReportAsync(SlackUserId);
    }

    public async Task<SlackMessages> AddAsync(SlackMessages slackMessages)
    {
        return await _slackMessagesRepository.AddAsync(slackMessages);
    }
}