using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SwimmingAcademy.DTOs;
using SwimmingAcademy.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SwimmingAcademy.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CoachesController : ControllerBase
    {
        private readonly ICoachRepository _coachRepository;
        private readonly ILogger<CoachesController> _logger;

        public CoachesController(ICoachRepository coachRepository, ILogger<CoachesController> logger)
        {
            _coachRepository = coachRepository;
            _logger = logger;
        }

        [HttpPost("free")]
        public async Task<IActionResult> GetFreeCoaches([FromBody] FreeCoachFilterRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var result = await _coachRepository.GetFreeCoachesAsync(request);
                if (result == null || !result.Any())
                    return NotFound("No free coaches found.");
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching free coaches");
                return StatusCode(500, "An unexpected error occurred. Please try again later.");
            }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<FreeCoachDto>>> GetAll()
        {
            try
            {
                var defaultRequest = new FreeCoachFilterRequest();
                var coaches = await _coachRepository.GetFreeCoachesAsync(defaultRequest);
                return Ok(coaches);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching coaches");
                return StatusCode(500, "An unexpected error occurred. Please try again later.");
            }
        }

        
    }
}
