namespace warren_analysis_desk;

public interface IUserSlackMessagesService
{
    Task<IEnumerable<UserSlackMessages>> GetAllAsync();
    Task<UserSlackMessages> CreateUserSlackMessageAsync(string blockId, string messageId, string SlackUsername);
}