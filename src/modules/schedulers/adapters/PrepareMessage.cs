namespace warren_analysis_desk;

public class PrepareMessages
{
    public async Task<List<object>> CheckboxNews(List<News> bingNewsList)
    {
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

        return blocks;
    }
}
