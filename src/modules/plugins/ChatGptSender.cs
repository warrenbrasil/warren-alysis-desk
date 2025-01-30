using OpenAI.Chat;

public class ChatGptService(string token)
{
    public async Task<string> GetChatGptResponseAsync(string input)
    {
        ChatClient client = new(model: "gpt-3.5-turbo-0125", apiKey: token);
            
        ChatCompletion completion = client
            .CompleteChat(input);
            
        Console.WriteLine($"[ASSISTANT]: {completion.Content[0].Text}");
        return completion.Content[0].Text;
    }
}
