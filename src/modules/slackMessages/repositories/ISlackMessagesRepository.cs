namespace warren_analysis_desk;

public interface ISlackMessagesRepository 
{
    Task<List<ReportItem>> GetReportAsync();
    Task<SlackMessages> AddAsync(SlackMessages slackMessages);
    Task MarkedUpdateAsync(string BlockId, string MessageId, bool Marked);
}