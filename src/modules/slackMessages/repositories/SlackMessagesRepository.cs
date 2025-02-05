using Microsoft.EntityFrameworkCore;
using warren_analysis_desk;

public class SlackMessagesRepository : ISlackMessagesRepository
{
    private readonly DatabaseContext _context;

    public SlackMessagesRepository(DatabaseContext context)
    {
        _context = context;
    }

    public async Task<List<ReportItem>> GetReportAsync(string SlackUserId)
    {
        try
        {
            var reportItems = new List<ReportItem>();

            var userSlackMessages = await _context.UserSlackMessages
                .Where(u => u.SlackUserId == SlackUserId && u.Marked == true)
                .ToListAsync();

            foreach (var userSlackMessage in userSlackMessages)
            {
                var slackMessage = await _context.SlackMessages
                    .FirstOrDefaultAsync(sm => sm.Id == userSlackMessage.SlackMessagesId);

                if (slackMessage != null)
                {
                    var news = await _context.News
                        .FirstOrDefaultAsync(n => n.Id == slackMessage.IdNews);

                    if (news != null)
                    {
                        reportItems.Add(new ReportItem
                        {
                            ChatGPTMsg = news.ChatGPTMsg,
                            PublishDate = news.PublishDate,
                            RobotName = news.RobotName,
                            ShortUrl = news.ShortUrl,
                            Title = news.Title
                        });
                    }
                }
            }

            return reportItems;
        }
        catch (Exception ex)
        {
            throw new Exception($"Error fetching report: {ex.Message}", ex);
        }
    }

    public async Task<SlackMessages> AddAsync(SlackMessages slackMessages)
    {
        try
        {
            await _context.SlackMessages.AddAsync(slackMessages);
            await _context.SaveChangesAsync();
            return slackMessages;
        }
        catch (Exception ex)
        {
            throw new Exception($"Error adding robot keys: {ex.Message}", ex);
        }
    }
}