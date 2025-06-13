using Microsoft.AspNetCore.Mvc;
using SwimmingAcademy.DTOs;
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

        [HttpPost("Free")]
        public async Task<ActionResult<List<FreeCoachDto>>> GetFreeCoaches([FromBody] FreeCoachRequestDto request)
        {
            var result = await _coachService.GetFreeCoachesAsync(request);
            return Ok(result);
        }
    }
}
