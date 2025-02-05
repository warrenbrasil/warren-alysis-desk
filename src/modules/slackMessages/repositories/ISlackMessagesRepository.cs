namespace warren_analysis_desk;

public interface ISlackMessagesRepository 
{
    Task<List<ReportItem>> GetReportAsync(string SlackUserId);
    Task<SlackMessages> AddAsync(SlackMessages slackMessages);
}