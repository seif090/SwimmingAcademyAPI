using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SwimmingAcademy.DTOs;
using SwimmingAcademy.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace SwimmingAcademy.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _repo;
        private readonly ILogger<AuthController> _logger;

        public AuthController(IAuthRepository repo, ILogger<AuthController> logger)
        {
            _repo = repo;
            _logger = logger;
        }

        [HttpPost("login")]
        public async Task<IActionResult> UserLogIn([FromBody] UserLoginRequestDto request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var result = await _repo.UserLogInAsync(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "UserLogIn failed");
                return StatusCode(500, "An unexpected error occurred. Please try again later.");
            }
        }
    }

    
}

