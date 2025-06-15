using Microsoft.AspNetCore.Mvc;
using SwimmingAcademy.DTOs;
using SwimmingAcademy.Interfaces;

namespace SwimmingAcademy.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SchoolsController : ControllerBase
    {
        private readonly ISchoolRepository _repo;

        public SchoolsController(ISchoolRepository repo)
        {
            _repo = repo;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateSchool([FromBody] CreateSchoolRequest request)
        {
            var id = await _repo.CreateSchoolAsync(request);
            return id > 0 ? Ok(new { SchoolID = id }) : BadRequest("Creation failed.");
        }
        [HttpPost("search")]
        public async Task<IActionResult> SearchSchools([FromBody] SchoolSearchRequest request)
        {
            var filtersUsed = new object[] { request.SchoolID, request.FullName, request.Level, request.Type }
                .Count(x => x != null);

            if (filtersUsed != 1)
                return BadRequest("Please provide exactly one filter: SchoolID, FullName, Level, or Type.");

            var result = await _repo.SearchSchoolsAsync(request);
            return Ok(result);
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateSchool([FromBody] UpdateSchoolRequest request)
        {
            var updated = await _repo.UpdateSchoolAsync(request);
            return updated ? Ok("School updated successfully.") : BadRequest("Update failed.");
        }

        [HttpPost("end")]
        public async Task<IActionResult> EndSchool([FromBody] EndSchoolRequest request)
        {
            var result = await _repo.EndSchoolAsync(request);
            return result ? Ok("School ended successfully.") : BadRequest("Failed to end school.");
        }
        [HttpGet("{schoolID}/details-tab")]
        public async Task<IActionResult> GetSchoolDetailsTab(long schoolID)
        {
            var result = await _repo.GetSchoolDetailsTabAsync(schoolID);
            return result == null ? NotFound() : Ok(result);
        }
        [HttpGet("{schoolID}/swimmers")]
        public async Task<IActionResult> GetSchoolSwimmerDetails(long schoolID)
        {
            var result = await _repo.GetSchoolSwimmerDetailsAsync(schoolID);
            return Ok(result);
        }

        [HttpPost("search-actions")]
        public async Task<IActionResult> SearchSchoolActions([FromBody] SchoolActionSearchRequest request)
        {
            var actions = await _repo.SearchSchoolActionsAsync(request);
            return Ok(actions);
        }
        [HttpGet("{swimmerID}/possible-school/{type}")]
        public async Task<IActionResult> GetPossibleSchool(long swimmerID, short type)
        {
            var result = await _repo.GetPossibleSchoolOptionsAsync(swimmerID, type);
            return Ok(result);
        }


    }
}
