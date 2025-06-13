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

        [HttpPost("Create")]
        public async Task<IActionResult> CreateSchool([FromBody] CreateSchoolDto dto)
        {
            var schoolId = await _schoolService.CreateSchoolAsync(dto);
            return Ok(new { Message = "School created successfully.", SchoolID = schoolId });
        }
        [HttpPost("End")]
        public async Task<IActionResult> EndSchool([FromBody] EndSchoolDto dto)
        {
            var success = await _schoolService.EndSchoolAsync(dto);
            if (success)
                return Ok(new { Message = "School ended successfully." });

            return BadRequest(new { Message = "Failed to end the school." });
        }
        [HttpGet("Details/{schoolId}")]
        public async Task<ActionResult<SchoolDetailsTabDto>> GetSchoolDetails(long schoolId)
        {
            var result = await _schoolService.GetSchoolDetailsAsync(schoolId);
            if (result == null)
                return NotFound(new { Message = "School not found." });

            return Ok(result);
        }


        [HttpPost("SearchActions")]
        public async Task<ActionResult<List<SearchActionResponseDto>>> SearchActions([FromBody] SearchSchoolActionRequestDto request)
        {
            var result = await _schoolService.SearchSchoolActionsAsync(request);
            return Ok(result);
        }
        [HttpPost("Show")]
        public async Task<ActionResult<List<ShowSchoolResponseDto>>> ShowSchool([FromBody] ShowSchoolRequestDto request)
        {
            var result = await _schoolService.ShowSchoolAsync(request);
            return Ok(result);
        }

        [HttpGet("SwimmerDetails/{schoolId}")]
        public async Task<ActionResult<List<SwimmerDetailsTabDto>>> GetSchoolSwimmerDetails(long schoolId)
        {
            var result = await _schoolService.GetSchoolSwimmerDetailsAsync(schoolId);
            return Ok(result);
        }

        [HttpPut("Update")]
        public async Task<IActionResult> UpdateSchool([FromBody] UpdateSchoolDto dto)
        {
            var success = await _schoolService.UpdateSchoolAsync(dto);
            if (success)
                return Ok(new { Message = "School updated successfully." });

            return BadRequest(new { Message = "Update failed. Please check the SchoolID." });
        }

    }
}
