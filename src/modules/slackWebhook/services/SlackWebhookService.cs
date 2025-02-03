using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.Text;
using System.Web;

namespace warren_analysis_desk;

public class SlackWebhookService : ISlackWebhookService
{
    // private readonly IRobotKeysRepository _robotKeysRepository = robotKeysRepository;

    public async Task<string> WebhookAdd(string requestBody)
    {
        var formData = HttpUtility.ParseQueryString(requestBody);
        string payloadEncoded = formData["payload"];
        string payloadDecoded = HttpUtility.UrlDecode(payloadEncoded);

        JObject payloadObject = JObject.Parse(payloadDecoded);
        // Console.WriteLine($"Decoded payload: {payloadObject}");

        Console.WriteLine($"{payloadObject["container"]["message_ts"]}");

        foreach (var block in (JArray)payloadObject["actions"])
        {
            if(block["selected_options"].Any())
            {
                Console.WriteLine($"Checkbox {block["block_id"]} marcada.");
            } else {
                Console.WriteLine($"Checkbox {block["block_id"]} desmarcada.");
            }
        }

        // string messageTs = payloadObject["message"]?["ts"]?.ToString();

        return "Webhook processado com sucesso!";
    }
}
