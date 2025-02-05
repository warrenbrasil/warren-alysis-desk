namespace warren_analysis_desk;

public interface IUserSlackMessagesService
{
    Task<IEnumerable<UserSlackMessages>> GetAllAsync();
    Task<UserSlackMessages> AlterUserSlackMessageAsync(string blockId, string messageId, string SlackUserId, string SlackUsername, bool Mrked);
}