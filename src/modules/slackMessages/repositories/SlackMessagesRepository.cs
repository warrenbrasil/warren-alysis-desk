using Microsoft.EntityFrameworkCore;
using warren_analysis_desk;

public class SlackMessagesRepository : ISlackMessagesRepository
{
    private readonly DatabaseContext _context;

    public SlackMessagesRepository(DatabaseContext context)
    {
        _context = context;
    }

    public async Task<List<ReportItem>> GetReportAsync()
    {
        try
        {
            return await _context.SlackMessages
                .Where(sm => sm.Marked == true)
                .Join(_context.News,
                    sm => sm.IdNews,
                    n => n.Id,
                    (sm, n) => new ReportItem
                    {
                        ChatGPTMsg = n.ChatGPTMsg,
                        PublishDate = n.PublishDate,
                        RobotName = n.RobotName,
                        ShortUrl = n.ShortUrl,
                        Title = n.Title
                    })
                .ToListAsync();
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

    public async Task MarkedUpdateAsync(string blockId, string messageId, bool marked)
    {
        try 
        {
            var existingMessage = await _context.SlackMessages
                .FirstOrDefaultAsync(x => x.BlockIds == blockId && x.MessageId == messageId);

            if (existingMessage == null)
            {
                throw new Exception("Slack message not found.");
            }

            existingMessage.Marked = marked;
            _context.Entry(existingMessage).Property(x => x.Marked).IsModified = true;

            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            throw new Exception($"Error updating Marked field: {ex.Message}", ex);
        }
    }
}