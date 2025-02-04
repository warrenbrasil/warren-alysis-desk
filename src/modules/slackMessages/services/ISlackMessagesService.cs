namespace warren_analysis_desk;

public interface ISlackMessagesService
{
    Task<byte[]> GetReportAsync();
    Task<List<ReportItem>> GetAllAsync();
    Task<SlackMessages> AddAsync(SlackMessages slackMessages);
}