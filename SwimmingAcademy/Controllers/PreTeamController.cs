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

        public PreTeamController(IPreTeamRepository repo)
        {
            _repo = repo;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreatePTeam([FromBody] CreatePTeamRequest request)
        {
            var pTeamId = await _repo.CreatePreTeamAsync(request);
            return Ok(new { PTeamID = pTeamId });
        }
        [HttpPost("search")]
        public async Task<IActionResult> SearchPTeam([FromBody] PTeamSearchRequest request)
        {
            // Optional: enforce one-filter-only logic here if needed
            var filtersUsed = new object[] { request.PTeamID, request.FullName, request.Level }.Count(x => x != null);
            if (filtersUsed != 1)
                return BadRequest("Please provide exactly one filter: PTeamID, FullName, or Level.");

            var result = await _repo.SearchPTeamAsync(request);
            return Ok(result);
        }
        [HttpGet("swimmer/{swimmerId}/details")]
        public async Task<IActionResult> GetSwimmerPTeamDetails(long swimmerId)
        {
            var result = await _repo.GetSwimmerPTeamDetailsAsync(swimmerId);
            return Ok(result);
        }
        [HttpPut("update")]
        public async Task<IActionResult> UpdatePTeam([FromBody] UpdatePTeamRequest request)
        {
            var updated = await _repo.UpdatePTeamAsync(request);
            return updated ? Ok("PreTeam updated successfully.") : BadRequest("Update failed.");
        }
        [HttpPost("end")]
        public async Task<IActionResult> EndPTeam([FromBody] EndPTeamRequest request)
        {
            var result = await _repo.EndPTeamAsync(request);
            return result ? Ok("PreTeam ended successfully.") : BadRequest("Failed to end PreTeam.");
        }
        [HttpGet("{pTeamId}/tab-details")]
        public async Task<IActionResult> GetPTeamDetailsTab(long pTeamId)
        {
            var result = await _repo.GetPTeamDetailsTabAsync(pTeamId);
            return result == null ? NotFound() : Ok(result);
        }
        [HttpPost("search-actions")]
        public async Task<IActionResult> SearchActions([FromBody] PreTeamActionSearchRequest request)
        {
            var actions = await _repo.SearchActionsAsync(request);
            return Ok(actions);
        }



    }
}
