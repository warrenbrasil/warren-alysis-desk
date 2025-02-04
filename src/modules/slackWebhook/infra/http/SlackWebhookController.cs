using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace warren_analysis_desk
{
    [ApiController]
    [Tags("Slack Webhook")]
    [Route("api/slack-webhook")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class SlackWebhookController : ControllerBase
    {
        private readonly ISlackWebhookService _slackWebhookService;

        public SlackWebhookController(ISlackWebhookService slackWebhookService)
        {
            _slackWebhookService = slackWebhookService;
        }

        [HttpPost]
        public async Task<IActionResult> Webhook()
        {
            try
            {
                using (StreamReader reader = new StreamReader(Request.Body, Encoding.UTF8))
                {
                    string requestBody = await reader.ReadToEndAsync();

                    var res = await _slackWebhookService.WebhookAdd(requestBody);
                }

                return Ok(new { message = "Received successfully" });
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(new { message = "An error occurred while fetching the robot keys.", details = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while fetching the robot keys.", details = ex.Message });
            }
        }
    }
}