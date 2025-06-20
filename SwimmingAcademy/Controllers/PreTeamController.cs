using Microsoft.AspNetCore.Mvc;
using SwimmingAcademy.DTOs;
using SwimmingAcademy.Interfaces;

namespace SwimmingAcademy.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PreTeamController : ControllerBase
    {
        private readonly IPreTeamRepository _repo;
        // private readonly ILogger<PreTeamController> _logger;

        public PreTeamController(IPreTeamRepository repo /*, ILogger<PreTeamController> logger*/)
        {
            _repo = repo;
            // _logger = logger;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreatePTeam([FromBody] CreatePTeamRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var pTeamId = await _repo.CreatePreTeamAsync(request);
                return Ok(new { PTeamID = pTeamId });
            }
            catch (Exception ex)
            {
                // _logger.LogError(ex, "Error in CreatePTeam");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost("search")]
        public async Task<IActionResult> SearchPTeam([FromBody] PTeamSearchRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var filtersUsed = new object?[] { request.PTeamID, request.FullName, request.Level }
                .Count(x => x != null);

            if (filtersUsed != 1)
                return BadRequest("Please provide exactly one filter: PTeamID, FullName, or Level.");

            try
            {
                var result = await _repo.SearchPTeamAsync(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                // _logger.LogError(ex, "Error in SearchPTeam");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("swimmer/{swimmerId}/details")]
        public async Task<IActionResult> GetSwimmerPTeamDetails(long swimmerId)
        {
            try
            {
                var result = await _repo.GetSwimmerPTeamDetailsAsync(swimmerId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                // _logger.LogError(ex, "Error in GetSwimmerPTeamDetails for swimmerId {swimmerId}", swimmerId);
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdatePTeam([FromBody] UpdatePTeamRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var updated = await _repo.UpdatePTeamAsync(request);
                return updated ? Ok("PreTeam updated successfully.") : BadRequest("Update failed.");
            }
            catch (Exception ex)
            {
                // _logger.LogError(ex, "Error in UpdatePTeam");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost("end")]
        public async Task<IActionResult> EndPTeam([FromBody] EndPTeamRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var result = await _repo.EndPTeamAsync(request);
                return result ? Ok("PreTeam ended successfully.") : BadRequest("Failed to end PreTeam.");
            }
            catch (Exception ex)
            {
                // _logger.LogError(ex, "Error in EndPTeam");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("{pTeamId}/tab-details")]
        public async Task<IActionResult> GetPTeamDetailsTab(long pTeamId)
        {
            try
            {
                var result = await _repo.GetPTeamDetailsTabAsync(pTeamId);
                return result == null ? NotFound("Details not found.") : Ok(result);
            }
            catch (Exception ex)
            {
                // _logger.LogError(ex, "Error in GetPTeamDetailsTab for PTeamID {pTeamId}", pTeamId);
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost("search-actions")]
        public async Task<IActionResult> SearchActions([FromBody] PreTeamActionSearchRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var actions = await _repo.SearchActionsAsync(request);
                return Ok(actions);
            }
            catch (Exception ex)
            {
                // _logger.LogError(ex, "Error in SearchActions");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
