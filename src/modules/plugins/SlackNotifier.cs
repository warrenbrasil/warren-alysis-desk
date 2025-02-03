using Newtonsoft.Json;
using System.Text;

namespace warren_analysis_desk;
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

    public async Task<string> SendMessageAsync(List<News> bingNewsList)
    {
        // var payload = new
        // {
        //     channel = _channelId,
        //     text = message
        // };

        var blocks = new List<object>();

        foreach (var (news, index) in bingNewsList.Select((value, i) => (value, i)))
        {
            if (news.HtmlList.Any())
            {
                var newsBlock = new
                {
                    type = "section",
                    block_id = $"news_section_{index}",
                    text = new
                    {
                        type = "mrkdwn",
                        text = $"*Notícia:* {news.Title}\n*Link:* {news.ShortUrl}\n*Resumo:* {news.ChatGPTMsg}\n*Data:* {news.PublishDate}"
                    },
                    accessory = new
                    {
                        type = "checkboxes",
                        action_id = "checkbox_action",
                        options = new[]
                        {
                            new {
                                text = new {
                                    type = "plain_text",
                                    text = "Marcar como lida"
                                },
                                value = "read"
                            }
                        }
                    }
                };

                blocks.Add(newsBlock);
            }
        }

        var payload = new
        {
            channel = _channelId,
            blocks
        };

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
            Console.WriteLine("Erro ao enviar mensagem!");
            return null;
        }
        else
        {
            Console.WriteLine("Mensagem enviada com sucesso!");
            return responseBody;
        }
    }
}
