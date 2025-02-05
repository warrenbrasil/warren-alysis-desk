using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace warren_analysis_desk 
{
    // [Authorize]
    [ApiController]
    [Tags("User Slack Messages")]
    [Route("api/user-slack-messages")]
    public class UserSlackMessagesController : ControllerBase
    {
        private readonly IUserSlackMessagesService _userSlackMessagesService;

        public UserSlackMessagesController(IUserSlackMessagesService userSlackMessagesService)
        {
            _userSlackMessagesService = userSlackMessagesService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try 
            {
                var userSlackMessagess = await _userSlackMessagesService.GetAllAsync();
                return Ok(userSlackMessagess);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while fetching.", details = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> AlterSlackUser(UserSlackMessagesDto userSlackMessagesDto, string BlockId, string MessageId)
        {
            try 
            {
                var userSlackMessagess = await _userSlackMessagesService.AlterUserSlackMessageAsync(BlockId, MessageId, userSlackMessagesDto.SlackUserId, userSlackMessagesDto.SlackUserName, userSlackMessagesDto.Marked);
                return Ok(userSlackMessagess);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while fetching.", details = ex.Message });
            }
        }
    }
}