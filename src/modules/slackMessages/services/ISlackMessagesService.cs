namespace warren_analysis_desk;

public interface ISlackMessagesService
{
    Task<byte[]> GetReportAsync(string SlackUsername);
    Task<List<ReportItem>> GetAllAsync(string SlackUsername);
    Task<SlackMessages> AddAsync(SlackMessages slackMessages);
}