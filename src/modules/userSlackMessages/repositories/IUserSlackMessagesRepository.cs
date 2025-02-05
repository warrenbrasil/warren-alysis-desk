namespace warren_analysis_desk;

public interface IUserSlackMessagesRepository 
{
    Task<IEnumerable<UserSlackMessages>> GetAllAsync();
    Task<UserSlackMessages> AlterUserSlackMessageAsync(string blockId, string messageId, string SlackUserId, string SlackUsername, bool Marked);
}