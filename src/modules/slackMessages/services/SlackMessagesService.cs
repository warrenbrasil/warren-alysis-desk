namespace warren_analysis_desk;
public class SlackMessagesService(ISlackMessagesRepository slackMessagesRepository) : ISlackMessagesService
{
    private readonly ISlackMessagesRepository _slackMessagesRepository = slackMessagesRepository;

    public async Task<byte[]> GetReportAsync()
    {
        var reportContent = await _slackMessagesRepository.GetReportAsync();
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

    public async Task<List<ReportItem>> GetAllAsync()
    {
        return await _slackMessagesRepository.GetReportAsync();
    }

    public async Task<SlackMessages> AddAsync(SlackMessages slackMessages)
    {
        return await _slackMessagesRepository.AddAsync(slackMessages);
    }
}