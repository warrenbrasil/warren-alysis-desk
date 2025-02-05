namespace warren_analysis_desk;

public interface ISlackMessagesService
{
    Task<byte[]> GetReportAsync(string SlackUserId);
    Task<List<ReportItem>> GetAllAsync(string SlackUserId);
    Task<SlackMessages> AddAsync(SlackMessages slackMessages);
}