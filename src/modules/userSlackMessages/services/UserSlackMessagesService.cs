namespace warren_analysis_desk;
public class UserSlackMessagesService : IUserSlackMessagesService
{
    private readonly IUserSlackMessagesRepository _userSlackMessagesRepository;

    public UserSlackMessagesService(IUserSlackMessagesRepository userSlackMessagesRepository)
    {
        _userSlackMessagesRepository = userSlackMessagesRepository;
    }

    public async Task<IEnumerable<UserSlackMessages>> GetAllAsync()
    {
        return await _userSlackMessagesRepository.GetAllAsync();
    }

    public async Task<UserSlackMessages> AlterUserSlackMessageAsync(string blockId, string messageId, string SlackUserId, string SlackUsername, bool Marked)
    {
        return await _userSlackMessagesRepository.AlterUserSlackMessageAsync(blockId, messageId, SlackUserId, SlackUsername, Marked);
    }
}