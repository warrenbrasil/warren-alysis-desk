using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace warren_analysis_desk 
{
    // [Authorize]
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

        [HttpPost("report")]
        public async Task<IActionResult> GetReport()
        {
            try 
            {
                // var formData = await Request.ReadFormAsync();
                // var userName = formData["user_name"];
                var originUrl = Request.Headers["Origin"].ToString();
                var fullUrl = $"{Request.Scheme}://{Request.Host}/api/slack-messages/download-report";

                return Ok(new { response_type = "in_channel", text = fullUrl });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while fetching the report.", details = ex.Message });
            }
        }

        [HttpGet("download-report")]
        public async Task<IActionResult> DownloadReport()
        {
            try 
            {
                var fileBytes = await _slackMessagesService.GetReportAsync();

                var fileName = "relatorio.txt";
                return File(fileBytes, "application/octet-stream", fileName);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while fetching the report.", details = ex.Message });
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try 
            {
                var slackMessagess = await _slackMessagesService.GetAllAsync();
                return Ok(slackMessagess);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while fetching all robot keys.", details = ex.Message });
            }
        }
    }
}