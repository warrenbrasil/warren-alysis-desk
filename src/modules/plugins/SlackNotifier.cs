using Newtonsoft.Json;
using System.Text;

public class SlackNotifier
{
    private static readonly HttpClient client = new HttpClient();
    private readonly string _slackToken;
    private readonly string _channelId;

    public SlackNotifier(string slackToken, string channelId)
    {
        _slackToken = slackToken;
        _channelId = channelId;
    }

    public async Task SendMessageAsync(string message)
    {
        var payload = new
        {
            channel = _channelId,
            text = message
        };

        var jsonPayload = JsonConvert.SerializeObject(payload);
        var request = new HttpRequestMessage(HttpMethod.Post, "https://slack.com/api/chat.postMessage")
        {
            Headers = { { "Authorization", $"Bearer {_slackToken}" } },
            Content = new StringContent(jsonPayload, Encoding.UTF8, "application/json")
        };

        HttpResponseMessage response = await client.SendAsync(request);
        string responseBody = await response.Content.ReadAsStringAsync();

        Console.WriteLine($"Status Code: {response.StatusCode}");
        Console.WriteLine($"Response: {responseBody}");
        
        if (!response.IsSuccessStatusCode)
        {
            Console.WriteLine("Erro ao enviar mensagem!");
        }
        else
        {
            Console.WriteLine("Mensagem enviada com sucesso!");
        }
    }
}
