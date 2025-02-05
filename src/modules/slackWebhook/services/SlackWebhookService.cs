using Newtonsoft.Json.Linq;
using System.Web;

namespace warren_analysis_desk;

public class SlackWebhookService : ISlackWebhookService
{
    private readonly IUserSlackMessagesRepository _userSlackMessagesRepository;

    public SlackWebhookService(IUserSlackMessagesRepository userSlackMessagesRepository)
    {
        _userSlackMessagesRepository = userSlackMessagesRepository 
            ?? throw new ArgumentNullException(nameof(userSlackMessagesRepository));
    }

    public async Task<string> WebhookAdd(string requestBody)
    {
        var formData = HttpUtility.ParseQueryString(requestBody);
        string payloadEncoded = formData["payload"];
        string payloadDecoded = HttpUtility.UrlDecode(payloadEncoded);

        JObject payloadObject = JObject.Parse(payloadDecoded);

        string MessageId = $"{payloadObject["container"]["message_ts"]}";

        string userName = $"{payloadObject["user"]["username"]}";
        string userId = $"{payloadObject["user"]["id"]}";

        foreach (var block in (JArray)payloadObject["actions"])
        {
            if(block["selected_options"].Any())
            {
                string BlockId = $"{block["block_id"]}";

                await _userSlackMessagesRepository.AlterUserSlackMessageAsync(BlockId, MessageId, userId, userName, true);
            } else {
                string BlockId = $"{block["block_id"]}";

                await _userSlackMessagesRepository.AlterUserSlackMessageAsync(BlockId, MessageId, userId, userName, false);
            }
        }

        return "Webhook processado com sucesso!";
    }
}