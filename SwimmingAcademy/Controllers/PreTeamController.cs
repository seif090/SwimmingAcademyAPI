using Microsoft.AspNetCore.Mvc;
using SwimmingAcademy.DTOs;
using SwimmingAcademy.Services.Interfaces;

namespace SwimmingAcademy.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PreTeamController : ControllerBase
    {
        private readonly IPreTeamService _preTeamService;

        public PreTeamController(IPreTeamService preTeamService)
        {
            _preTeamService = preTeamService;
        }

        [HttpPost("Create")]
        public async Task<IActionResult> CreatePreTeam([FromBody] CreatePreTeamDto dto)
        {
            var pTeamId = await _preTeamService.CreatePreTeamAsync(dto);
            return Ok(new { Message = "PreTeam created successfully", PTeamID = pTeamId });
        }

        [HttpPost("End")]
        public async Task<IActionResult> EndPreTeam([FromBody] EndPreTeamDto dto)
        {
            var success = await _preTeamService.EndPreTeamAsync(dto);
            if (success)
                return Ok(new { Message = "PreTeam ended successfully." });
            else
                return BadRequest(new { Message = "Failed to end PreTeam." });
        }

        [HttpGet("Details/{preTeamId}")]
        public async Task<ActionResult<PreTeamDetailsDto>> GetPreTeamDetails(long preTeamId)
        {
            var details = await _preTeamService.GetPreTeamDetailsAsync(preTeamId);
            if (details == null)
                return NotFound(new { Message = "PreTeam not found." });

            return Ok(details);
        }

        [HttpGet("SwimmerDetails/{pTeamId}")]
        public async Task<ActionResult<List<SwimmerDetailsTabDto>>> GetSwimmerDetails(long pTeamId)
        {
            var result = await _preTeamService.GetSwimmerDetailsAsync(pTeamId);
            return Ok(result);
        }


        [HttpPost("SearchActions")]
        public async Task<ActionResult<List<SearchActionResponseDto>>> SearchActions([FromBody] SearchActionRequestDto request)
        {
            var result = await _preTeamService.SearchActionsAsync(request);
            return Ok(result);
        }


        [HttpPost("Show")]
        public async Task<ActionResult<List<ShowPreTeamResponseDto>>> ShowPreTeam([FromBody] ShowPreTeamRequestDto request)
        {
            var result = await _preTeamService.ShowPreTeamAsync(request);
            return Ok(result);
        }
        [HttpPut("Update")]
        public async Task<IActionResult> UpdatePreTeam([FromBody] UpdatePreTeamDto dto)
        {
            var success = await _preTeamService.UpdatePreTeamAsync(dto);
            if (success)
                return Ok(new { Message = "PreTeam updated successfully." });

            return BadRequest(new { Message = "Update failed. Please check PTeamID." });
        }


    }
}
