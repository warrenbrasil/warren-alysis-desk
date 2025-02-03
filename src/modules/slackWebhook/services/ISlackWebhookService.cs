namespace warren_analysis_desk;

public interface ISlackWebhookService
{
    Task<string> WebhookAdd(string payload);
}