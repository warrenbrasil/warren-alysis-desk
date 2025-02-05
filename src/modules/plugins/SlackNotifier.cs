using Newtonsoft.Json;
using System.Text;

namespace warren_analysis_desk;
public class SlackNotifier
{
    private static readonly HttpClient client = new HttpClient();
    private readonly string _slackToken;

    public SlackNotifier(string slackToken)
    {
        _slackToken = slackToken;
    }

    public async Task<string> SendMessageAsync(object payload)
    {
        // var payload = new
        // {
        //     channel = _channelId,
        //     text = message
        // };

        var jsonPayload = JsonConvert.SerializeObject(payload);
        var request = new HttpRequestMessage(HttpMethod.Post, "https://slack.com/api/chat.postMessage")
        {
            Headers = { { "Authorization", $"Bearer {_slackToken}" } },
            Content = new StringContent(jsonPayload, Encoding.UTF8, "application/json")
        };

        HttpResponseMessage response = await client.SendAsync(request);
        string responseBody = await response.Content.ReadAsStringAsync();

        // Console.WriteLine($"Status Code: {response.StatusCode}");
        // Console.WriteLine($"Response: {responseBody}");
        
        if (!response.IsSuccessStatusCode)
        {
            // Console.WriteLine("Erro ao enviar mensagem!");
            return null;
        }
        else
        {
            // Console.WriteLine("Mensagem enviada com sucesso!");
            return responseBody;
        }
    }
}