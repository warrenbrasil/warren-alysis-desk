using Microsoft.EntityFrameworkCore;
using Mysqlx;
using warren_analysis_desk;

public class UserSlackMessagesRepository : IUserSlackMessagesRepository
{
    private readonly DatabaseContext _context;

    public UserSlackMessagesRepository(DatabaseContext context)
    {
        _context = context;
    }

    public async Task<UserSlackMessages> AlterUserSlackMessageAsync(string blockId, string messageId, string SlackUserId, string SlackUsername, bool Marked)
    {
        try
        {
            var slackMessage = await _context.SlackMessages
                .FirstOrDefaultAsync(sm => sm.BlockIds == blockId && sm.MessageId == messageId) 
                    ?? throw new Exception("Slack message not found.");

            var userSlackMessage = await _context.UserSlackMessages
                .FirstOrDefaultAsync(usm => usm.SlackUserId == SlackUserId && usm.SlackMessagesId == slackMessage.Id);

            if (userSlackMessage != null)
            {
                userSlackMessage.Marked = Marked;
            }
            else
            {
                userSlackMessage = new UserSlackMessages
                {
                    SlackUserId = SlackUserId,
                    SlackMessagesId = slackMessage.Id,
                    SlackUserName = SlackUsername,
                    Marked = Marked,
                };
                _context.UserSlackMessages.Add(userSlackMessage);
            }

            await _context.SaveChangesAsync(); 

            return userSlackMessage;
        }
        catch (Exception ex)
        {
            throw new Exception($"Error creating/updating UserSlackMessage: {ex.Message}", ex);
        }
    }

    public async Task<IEnumerable<UserSlackMessages>> GetAllAsync()
    {
        try 
        {
            return await _context.UserSlackMessages.ToListAsync();
        }
        catch (Exception ex)
        {
            throw new Exception($"Error adding: {ex.Message}", ex);
        }
    }
}