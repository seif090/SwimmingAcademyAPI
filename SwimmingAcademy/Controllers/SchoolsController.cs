using Microsoft.AspNetCore.Mvc;
using SwimmingAcademy.DTOs;
using SwimmingAcademy.Services.Interfaces;

namespace SwimmingAcademy.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SchoolsController : ControllerBase
    {
        private readonly ISchoolService _schoolService;

        public SchoolsController(ISchoolService schoolService)
        {
            _schoolService = schoolService;
        }

        [HttpPost("create")]
        public async Task<ActionResult<long>> CreateSchool([FromBody] CreateSchoolDto dto)
        {
            var id = await _schoolService.CreateSchoolAsync(dto);
            return Ok(id);
        }
        [HttpPost("end")]
        public async Task<IActionResult> EndSchool([FromBody] EndSchoolDto dto)
        {
            await _schoolService.EndSchoolAsync(dto);
            return Ok();
        }
        [HttpGet("details/{schoolId:long}")]
        public async Task<ActionResult<SchoolDetailsTabDto>> GetSchoolDetailsTab(long schoolId)
        {
            var details = await _schoolService.GetSchoolDetailsTabAsync(schoolId);
            if (details is null)
                return NotFound();
            return Ok(details);
        }
       
        [HttpGet("actions")]
        public async Task<ActionResult<List<ActionNameDto>>> SearchActions(
            [FromQuery] int userId,
            [FromQuery] long schoolId,
            [FromQuery] short userSite)
        {
            var actions = await _schoolService.SearchActionsAsync(userId, schoolId, userSite);
            return Ok(actions);
        }
        [HttpGet("swimmer-details/{schoolId:long}")]
        public async Task<ActionResult<List<SwimmerDetailsTabDto>>> GetSwimmerDetailsTab(long schoolId)
        {
            var details = await _schoolService.GetSwimmerDetailsTabAsync(schoolId);
            return Ok(details);
        }
        [HttpPut("update")]
        public async Task<IActionResult> UpdateSchool([FromBody] UpdateSchoolDto dto)
        {
            await _schoolService.UpdateSchoolAsync(dto);
            return Ok();
        }
    }
}
