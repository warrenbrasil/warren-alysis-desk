using Microsoft.AspNetCore.Mvc;

namespace warren_analysis_desk 
{
    [ApiController]
    [Tags("Slack Messages")]
    [Route("api/slack-messages")]
    public class SlackMessagesController : ControllerBase
    {
        private readonly ISlackMessagesService _slackMessagesService;

        public SlackMessagesController(ISlackMessagesService slackMessagesService)
        {
            _slackMessagesService = slackMessagesService;
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        [HttpPost("report")]
        public async Task<IActionResult> GetReport()
        {
            try 
            {
                var formData = await Request.ReadFormAsync();
                var userId = formData["user_id"];
                var originUrl = Request.Headers["Origin"].ToString();
                var fullUrl = $"{Request.Scheme}://{Request.Host}/api/slack-messages/download-report/{userId}";

                return Ok(new { response_type = "in_channel", text = fullUrl });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while fetching the report.", details = ex.Message });
            }
        }

        [HttpGet("download-report/{slackUserId}")]
        public async Task<IActionResult> DownloadReport(string slackUserId)
        {
            try 
            {
                var fileBytes = await _slackMessagesService.GetReportAsync(slackUserId);

                var fileName = "relatorio.txt";
                return File(fileBytes, "application/octet-stream", fileName);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while fetching the report.", details = ex.Message });
            }
        }

        [HttpGet("{slackUserId}")]
        public async Task<IActionResult> GetAll(string slackUserId)
        {
            try 
            {
                var slackMessagess = await _slackMessagesService.GetAllAsync(slackUserId);
                return Ok(slackMessagess);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while fetching all robot keys.", details = ex.Message });
            }
        }
    }
}