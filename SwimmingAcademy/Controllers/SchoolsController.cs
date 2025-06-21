using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SwimmingAcademy.DTOs;
using SwimmingAcademy.Interfaces;
using SwimmingAcademy.Repositories;

namespace SwimmingAcademy.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SchoolsController : ControllerBase
    {
        private readonly ISchoolRepository _repo;
        private readonly ILogger<SchoolsController> _logger;

        public SchoolsController(ISchoolRepository repo, ILogger<SchoolsController> logger)
        {
            _repo = repo;
            _logger = logger;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateSchool([FromBody] CreateSchoolRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var schoolId = await _repo.CreateSchoolAsync(request);
                return schoolId > 0
                    ? Ok(new { SchoolID = schoolId })
                    : BadRequest("Failed to create school.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating school");
                return StatusCode(500, "An unexpected error occurred. Please try again later.");
            }
        }

        [HttpPost("search")]
        public async Task<IActionResult> SearchSchools([FromBody] SchoolSearchRequest request)
        {
            int filtersUsed = 0;
            if (request.SchoolID != null) filtersUsed++;
            if (!string.IsNullOrWhiteSpace(request.FullName)) filtersUsed++;
            if (request.Level != null) filtersUsed++;
            if (request.Type != null) filtersUsed++;

            if (filtersUsed != 1)
                return BadRequest("Please provide exactly one filter: SchoolID, FullName, Level, or Type.");

            try
            {
                var result = await _repo.SearchSchoolsAsync(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in SearchSchools");
                return StatusCode(500, "An unexpected error occurred. Please try again later.");
            }
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateSchool([FromBody] UpdateSchoolRequest request)
        {
            try
            {
                var updated = await _repo.UpdateSchoolAsync(request);
                return updated ? Ok("School updated successfully.") : BadRequest("Update failed.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in UpdateSchool");
                return StatusCode(500, "An unexpected error occurred. Please try again later.");
            }
        }

        [HttpPost("end")]
        public async Task<IActionResult> EndSchool([FromBody] EndSchoolRequest request)
        {
            try
            {
                var result = await _repo.EndSchoolAsync(request);
                return result ? Ok("School ended successfully.") : BadRequest("Failed to end school.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in EndSchool");
                return StatusCode(500, "An unexpected error occurred. Please try again later.");
            }
        }

        [HttpGet("{schoolID}/details-tab")]
        public async Task<IActionResult> GetSchoolDetailsTab(long schoolID)
        {
            try
            {
                var result = await _repo.GetSchoolDetailsTabAsync(schoolID);
                return result == null ? NotFound("School not found.") : Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetSchoolDetailsTab");
                return StatusCode(500, "An unexpected error occurred. Please try again later.");
            }
        }

        [HttpGet("{schoolID}/swimmers")]
        public async Task<IActionResult> GetSchoolSwimmerDetails(long schoolID)
        {
            try
            {
                var result = await _repo.GetSchoolSwimmerDetailsAsync(schoolID);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetSchoolSwimmerDetails");
                return StatusCode(500, "An unexpected error occurred. Please try again later.");
            }
        }

        [HttpPost("search-actions")]
        public async Task<IActionResult> SearchSchoolActions([FromBody] SchoolActionSearchRequest request)
        {
            try
            {
                var actions = await _repo.SearchSchoolActionsAsync(request);
                return Ok(actions);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in SearchSchoolActions");
                return StatusCode(500, "An unexpected error occurred. Please try again later.");
            }
        }

        [HttpGet("{swimmerID}/possible-school/{type}")]
        public async Task<IActionResult> GetPossibleSchool(long swimmerID, short type)
        {
            try
            {
                var result = await _repo.GetPossibleSchoolOptionsAsync(swimmerID, type);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetPossibleSchool");
                return StatusCode(500, "An unexpected error occurred. Please try again later.");
            }
        }
    }
}