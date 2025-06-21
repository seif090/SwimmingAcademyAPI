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

        [HttpGet("{id}")]
        public async Task<ActionResult<CoachDTO>> GetById(int id)
        {
            try
            {
                var coach = await _coachRepository.GetByIdAsync(id);
                if (coach == null)
                    return NotFound();
                return Ok(coach);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error fetching coach with id {id}");
                return StatusCode(500, "An unexpected error occurred. Please try again later.");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CoachDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var coachId = await _coachRepository.CreateCoachAsync(dto);
                return Ok(new { CoachID = coachId });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating coach");
                return StatusCode(500, "An unexpected error occurred. Please try again later.");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] CoachDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (id != dto.CoachID)
                return BadRequest("Coach ID mismatch.");

            try
            {
                var updated = await _coachRepository.UpdateCoachAsync(dto);
                return updated ? Ok("Coach updated successfully.") : NotFound("Coach not found.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error updating coach with id {id}");
                return StatusCode(500, "An unexpected error occurred. Please try again later.");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var deleted = await _coachRepository.DeleteCoachAsync(id);
                return deleted ? Ok("Coach deleted successfully.") : NotFound("Coach not found.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error deleting coach with id {id}");
                return StatusCode(500, "An unexpected error occurred. Please try again later.");
            }
        }
    }
}
