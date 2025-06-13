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
        [HttpPost("Add")]
        public async Task<ActionResult<AddSwimmerResponseDto>> AddSwimmer([FromBody] AddSwimmerDto dto)
        {
            var result = await _swimmerService.AddSwimmerAsync(dto);
            return Ok(result);
        }
        [HttpPut("ChangeSite")]
        public async Task<ActionResult<ChangeSiteResponseDto>> ChangeSite([FromBody] ChangeSwimmerSiteDto dto)
        {
            var result = await _swimmerService.ChangeSwimmerSiteAsync(dto);
            return Ok(result);
        }
        [HttpDelete("Delete/{swimmerId}")]
        public async Task<ActionResult<DropSwimmerResponseDto>> DeleteSwimmer(long swimmerId)
        {
            var success = await _swimmerService.DropSwimmerAsync(swimmerId);

            if (success)
                return Ok(new DropSwimmerResponseDto());
            else
                return NotFound(new { Message = "Swimmer not found or already deleted." });
        }

        [HttpGet("Info/{swimmerId}")]
        public async Task<ActionResult<SwimmerInfoTabDto>> GetSwimmerInfo(long swimmerId)
        {
            var info = await _swimmerService.GetSwimmerInfoAsync(swimmerId);
            if (info == null)
                return NotFound(new { Message = "Swimmer not found." });

            return Ok(info);
        }

        [HttpGet("Log/{swimmerId}")]
        public async Task<ActionResult<List<SwimmerLogTabDto>>> GetSwimmerLog(long swimmerId)
        {
            var log = await _swimmerService.GetSwimmerLogAsync(swimmerId);
            return Ok(log);
        }
        [HttpPost("SearchActions")]
        public async Task<ActionResult<List<SearchActionResponseDto>>> SearchSwimmerActions([FromBody] SearchSwimmerActionRequestDto request)
        {
            var actions = await _swimmerService.SearchSwimmerActionsAsync(request);
            return Ok(actions);
        }
        [HttpPost("Search")]
        public async Task<ActionResult<List<ShowSwimmerResponseDto>>> SearchSwimmers([FromBody] ShowSwimmerRequestDto request)
        {
            var result = await _swimmerService.ShowSwimmersAsync(request);
            return Ok(result);
        }

        [HttpPut("Update")]
        public async Task<ActionResult<UpdateSwimmerResponseDto>> UpdateSwimmer([FromBody] UpdateSwimmerDto dto)
        {
            var response = await _swimmerService.UpdateSwimmerAsync(dto);
            return Ok(response);
        }
        [HttpPut("UpdateLevel")]
        public async Task<ActionResult<UpdateSwimmerLevelResponseDto>> UpdateSwimmerLevel([FromBody] UpdateSwimmerLevelDto dto)
        {
            var response = await _swimmerService.UpdateSwimmerLevelAsync(dto);
            return Ok(response);
        }

        [HttpPost("ViewPossibleSchool")]
        public async Task<ActionResult<ViewPossibleSchoolResponseDto>> ViewPossibleSchool([FromBody] ViewPossibleSchoolRequestDto dto)
        {
            var result = await _swimmerService.ViewPossibleSchoolsAsync(dto);
            return Ok(result);
        }

        [HttpGet("SwimmerTechnicalTab/{swimmerId}")]
        public async Task<ActionResult<TechnicalTabResultDto>> GetSwimmerTechnicalTab(long swimmerId)
        {
            var result = await _swimmerService.GetTechnicalTabAsync(swimmerId);
            if (result == null)
                return NotFound("Swimmer not found or no technical data available.");

            return Ok(result);
        }
    }
}
