using Microsoft.AspNetCore.Mvc;
using SwimmingAcademy.DTOs;
using SwimmingAcademy.Services.Interfaces;

namespace SwimmingAcademy.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SwimmersController : ControllerBase
    {
        private readonly ISwimmerService _swimmerService;

        public SwimmersController(ISwimmerService swimmerService)
        {
            _swimmerService = swimmerService;
        }

        // GET: api/swimmers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SwimmerDto>>> GetAll()
        {
            var swimmers = await _swimmerService.GetAllSwimmersAsync();
            return Ok(swimmers);
        }

        // GET: api/swimmers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SwimmerDto>> GetById(long id)
        {
            var swimmer = await _swimmerService.GetSwimmerById(id); // Fixed method name
            if (swimmer == null)
                return NotFound();

            return Ok(swimmer);
        }
    }
}
