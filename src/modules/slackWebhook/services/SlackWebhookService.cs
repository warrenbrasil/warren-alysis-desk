using Newtonsoft.Json.Linq;
using System.Web;

namespace warren_analysis_desk;

public class SlackWebhookService : ISlackWebhookService
{
    private readonly ISlackMessagesRepository _slackMessagesRepository;

    public SlackWebhookService(ISlackMessagesRepository slackMessagesRepository)
    {
        _slackMessagesRepository = slackMessagesRepository 
            ?? throw new ArgumentNullException(nameof(slackMessagesRepository));
    }

    public async Task<string> WebhookAdd(string requestBody)
    {
        var formData = HttpUtility.ParseQueryString(requestBody);
        string payloadEncoded = formData["payload"];
        string payloadDecoded = HttpUtility.UrlDecode(payloadEncoded);

        JObject payloadObject = JObject.Parse(payloadDecoded);

        string MessageId = $"{payloadObject["container"]["message_ts"]}";

        foreach (var block in (JArray)payloadObject["actions"])
        {
            if(block["selected_options"].Any())
            {
                string BlockId = $"{block["block_id"]}";

                await _slackMessagesRepository.MarkedUpdateAsync(BlockId, MessageId, true);
            } else {
                string BlockId = $"{block["block_id"]}";

                await _slackMessagesRepository.MarkedUpdateAsync(BlockId, MessageId, false);
            }
        }

        return "Webhook processado com sucesso!";
    }
}
