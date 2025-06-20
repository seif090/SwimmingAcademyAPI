using Microsoft.AspNetCore.Mvc;
using SwimmingAcademy.DTOs;
using SwimmingAcademy.Interfaces;

namespace SwimmingAcademy.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CoachesController : ControllerBase
    {
        private readonly ICoachRepository _repo;

        public CoachesController(ICoachRepository repo)
        {
            _repo = repo;
        }

        [HttpPost("free")]
        public async Task<IActionResult> GetFreeCoaches([FromBody] FreeCoachFilterRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var result = await _repo.GetFreeCoachesAsync(request);

                if (result == null || !result.Any())
                    return NotFound("No free coaches found.");

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

    }
}
