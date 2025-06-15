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
            var result = await _repo.GetFreeCoachesAsync(request);
            return Ok(result);
        }
    }
}
