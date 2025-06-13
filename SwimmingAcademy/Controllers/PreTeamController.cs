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

        [HttpPost("create")]
        public async Task<ActionResult<long>> CreatePTeam([FromBody] CreatePTeamDto dto)
        {
            var id = await _preTeamService.CreatePTeamAsync(
                dto.PTeamLevel,
                dto.CoachID,
                dto.FirstDay,
                dto.SecondDay,
                dto.ThirdDay,
                dto.Site,
                dto.User,
                dto.StartTime,
                dto.EndTime);

            return Ok(id);
        }

        [HttpPost("end")]
        public async Task<IActionResult> EndPreTeam([FromBody] EndPreTeamDto dto)
        {
            await _preTeamService.EndPreTeamAsync(dto.PteamID, dto.UserID, dto.Site);
            return Ok();
        }

        [HttpGet("details/{pteamId:long}")]
        public async Task<ActionResult<PreTeamDetailsDto>> GetPTeamDetails(long pteamId)
        {
            var details = await _preTeamService.GetPTeamDetailsAsync(pteamId);
            if (details is null)
                return NotFound();
            return Ok();
        }

        [HttpGet("swimmer-details/{pteamId:long}")]
        public async Task<ActionResult<List<SwimmerDetailsTabDto>>> GetSwimmerDetailsTab(long pteamId)
        {
            var details = await _preTeamService.GetSwimmerDetailsTabAsync(pteamId);
            return Ok(details);
        }

        [HttpGet("actions")]
        public async Task<ActionResult<List<ActionNameDto>>> SearchActions(
            [FromQuery] int userId,
            [FromQuery] long pteamId,
            [FromQuery] short userSite)
        {
            var actions = await _preTeamService.SearchActionsAsync(userId, pteamId, userSite);
            return Ok(actions);
        }

        [HttpGet("show")]
        public async Task<ActionResult<List<ShowPreTeamDto>>> ShowPreTeam(
            [FromQuery] long? pteamId,
            [FromQuery] string? fullName,
            [FromQuery] short? level)
        {
            var result = await _preTeamService.ShowPreTeamAsync(pteamId, fullName, level);
            return Ok(result);
        }
    }
}
