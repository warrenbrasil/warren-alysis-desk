using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace warren_analysis_desk
{
    [Authorize]
    [ApiController]
    [Tags("Bing News Extractor")]
    [Route("api/bing-news-extractor")]
    public class BingNewsExtractorController : ControllerBase
    {
        private readonly IBingNewsExtractorService _bingNewsExtractorService;

        public BingNewsExtractorController(IBingNewsExtractorService bingNewsExtractorService)
        {
            _bingNewsExtractorService = bingNewsExtractorService;
        }

        [HttpGet]
        public async Task<IActionResult> GetBingNews()
        {
            try
            {
                var newsList = await _bingNewsExtractorService.GetBingNews();
                return Ok(newsList);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while fetching all news.", details = ex.Message });
            }
        }
    }
}
