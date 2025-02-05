namespace warren_analysis_desk;
public class UserSlackMessagesService(IUserSlackMessagesRepository userSlackMessagesRepository) : IUserSlackMessagesService
{
    private readonly IUserSlackMessagesRepository _userSlackMessagesRepository = userSlackMessagesRepository;

    public async Task<IEnumerable<UserSlackMessages>> GetAllAsync()
    {
        return await _userSlackMessagesRepository.GetAllAsync();
    }

    public async Task<UserSlackMessages> CreateUserSlackMessageAsync(string blockId, string messageId, string SlackUsername)
    {
        return await _userSlackMessagesRepository.CreateUserSlackMessageAsync(blockId, messageId, SlackUsername);
    }
}