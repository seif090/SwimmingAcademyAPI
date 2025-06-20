using Microsoft.AspNetCore.Mvc;
using SwimmingAcademy.DTOs;
using SwimmingAcademy.Interfaces;

namespace SwimmingAcademy.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SwimmersController : ControllerBase
    {
        private readonly ISwimmerRepository _repo;

        public SwimmersController(ISwimmerRepository repo)
        {
            _repo = repo;
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddSwimmer([FromBody] AddSwimmerRequestDTO request)
        {
            var swimmerId = await _repo.AddSwimmerAsync(request);
            return swimmerId > 0
                ? Ok(new { SwimmerID = swimmerId })
                : BadRequest("Failed to add swimmer.");
        }
        [HttpGet("{swimmerID}/info-tab")]
        public async Task<IActionResult> GetSwimmerInfoTab(long swimmerID)
        {
            var result = await _repo.GetSwimmerInfoTabAsync(swimmerID);
            return result is null ? NotFound() : Ok(result);
        }

        [HttpGet("{swimmerID}/logs")]
        public async Task<IActionResult> GetSwimmerLogs(long swimmerID)
        {
            var result = await _repo.GetSwimmerLogsAsync(swimmerID);
            return Ok(result);
        }

        [HttpPut("change-site")]
        public async Task<IActionResult> ChangeSite([FromBody] ChangeSwimmerSiteRequest request)
        {
            var result = await _repo.ChangeSwimmerSiteAsync(request);
            return result > 0 ? Ok(new { SwimmerID = result }) : BadRequest("Site change failed.");
        }
        [HttpDelete("{swimmerID}")]
        public async Task<IActionResult> DeleteSwimmer(long swimmerID)
        {
            var result = await _repo.DeleteSwimmerAsync(swimmerID);
            return result ? Ok("Swimmer deleted successfully.") : BadRequest("Failed to delete swimmer.");
        }

        [HttpPost("save-pteam-trans")]
        public async Task<IActionResult> SavePreTeamTransaction([FromBody] SavePteamTransRequest request)
        {
            var invoice = await _repo.SavePreTeamTransactionAsync(request);
            return invoice is null ? BadRequest("Failed to process transaction.") : Ok(invoice);
        }

        [HttpPost("save-school-trans")]
        public async Task<IActionResult> SaveSchoolTransaction([FromBody] SaveSchoolTransRequest request)
        {
            var invoice = await _repo.SaveSchoolTransactionAsync(request);
            return invoice is null
                ? BadRequest("Failed to save school transaction.")
                : Ok(invoice);
        }
        [HttpPost("search-actions")]
        public async Task<IActionResult> SearchSwimmerActions([FromBody] SwimmerActionSearchRequest request)
        {
            var actions = await _repo.SearchSwimmerActionsAsync(request);
            return Ok(actions);
        }
        [HttpPost("search")]
        public async Task<IActionResult> SearchSwimmers([FromBody] SwimmerSearchRequest request)
        {
            // Ensure null values are replaced with default values to avoid CS8601
            var filtersUsed = new object[]
            {
                request.SwimmerID ?? 0,
                request.FullName ?? string.Empty,
                request.Year ?? string.Empty,
                request.Level ?? (short)0
            }.Count(x => x != null);

            if (filtersUsed != 1)
                return BadRequest("Please provide exactly one filter: SwimmerID, FullName, Year, or Level.");

            var result = await _repo.SearchSwimmersAsync(request);
            return Ok(result);
        }
        [HttpGet("{swimmerID}/technical-tab")]
        public async Task<IActionResult> GetSwimmerTechnicalTab(long swimmerID)
        {
            var result = await _repo.GetTechnicalTapAsync(swimmerID);
            return result is null ? NotFound() : Ok(result);
        }
        [HttpPut("update")]
        public async Task<IActionResult> UpdateSwimmer([FromBody] UpdateSwimmerRequest request)
        {
            var updated = await _repo.UpdateSwimmerAsync(request);
            return updated ? Ok("Swimmer updated successfully.") : BadRequest("Update failed.");
        }
        [HttpPut("update-level")]
        public async Task<IActionResult> UpdateSwimmerLevel([FromBody] UpdateSwimmerLevelRequest request)
        {
            var success = await _repo.UpdateSwimmerLevelAsync(request);
            return success ? Ok("Swimmer level updated successfully.") : BadRequest("Failed to update level.");
        }
        [HttpGet("{swimmerID}/possible-preteam")]
        public async Task<IActionResult> GetPossiblePreTeam(long swimmerID)
        {
            var result = await _repo.GetPossiblePreTeamOptionsAsync(swimmerID);
            return Ok(result);
        }





    }
}
