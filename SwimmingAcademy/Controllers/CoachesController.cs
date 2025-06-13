using Microsoft.AspNetCore.Mvc;
using SwimmingAcademy.Services.Interfaces;

namespace SwimmingAcademy.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CoachesController : ControllerBase
    {
        private readonly ICoachService _coachService;

        public CoachesController(ICoachService coachService)
        {
            _coachService = coachService;
        }

        [HttpGet("free")]
        public async Task<ActionResult<IEnumerable<string>>> GetFreeCoaches(
            [FromQuery] short type,
            [FromQuery] TimeSpan startTime,
            [FromQuery] string firstDay,
            [FromQuery] short site)
        {
            var freeCoaches = await _coachService.GetFreeCoachesAsync(type, startTime, firstDay, site);
            return Ok(freeCoaches);
        }
    }
}
