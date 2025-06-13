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
        [HttpPost("add")]
        public async Task<ActionResult<long>> AddSwimmer([FromBody] AddSwimmerDto dto)
        {
            var id = await _swimmerService.AddSwimmerAsync(dto);
            return Ok(id);
        }
        [HttpPost("change-site")]
        public async Task<ActionResult<long>> ChangeSite([FromBody] ChangeSiteDto dto)
        {
            var id = await _swimmerService.ChangeSiteAsync(dto);
            return Ok(id);
        }
        [HttpDelete("drop/{swimmerId:long}")]
        public async Task<IActionResult> DropSwimmer(long swimmerId)
        {
            await _swimmerService.DropSwimmerAsync(swimmerId);
            return Ok();
        }
    }
}
