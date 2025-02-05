using Microsoft.EntityFrameworkCore;
using warren_analysis_desk;

public class UserSlackMessagesRepository : IUserSlackMessagesRepository
{
    private readonly DatabaseContext _context;

    public UserSlackMessagesRepository(DatabaseContext context)
    {
        _context = context;
    }

    public async Task<UserSlackMessages> CreateUserSlackMessageAsync(string blockId, string messageId, string SlackUsername)
    {
        try
        {
            var slackMessage = await _context.SlackMessages
                .FirstOrDefaultAsync(sm => sm.BlockIds == blockId && sm.MessageId == messageId);

            if (slackMessage == null)
            {
                throw new Exception("Slack message not found.");
            }

            var userSlackMessage = new UserSlackMessages
            {
                SlackUserName = SlackUsername,
                SlackMessagesId = slackMessage.Id
            };

            _context.UserSlackMessages.Add(userSlackMessage);
            await _context.SaveChangesAsync(); 

            return userSlackMessage;
        }
        catch (Exception ex)
        {
            throw new Exception($"Error creating UserSlackMessage: {ex.Message}", ex);
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