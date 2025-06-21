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
        public async Task<ActionResult<LoginResultDTO>> Login([FromBody] LoginRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var result = await _repo.LoginAsync(request.UserId, request.Password!);

                if (result == null)
                    return Unauthorized("Invalid credentials.");

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during login for user {UserId}", request.UserId);
                // Generic error for production
                return StatusCode(500, "An unexpected error occurred. Please try again later.");
            }
        }
    }

    public class LoginRequest
    {
        public int UserId { get; set; }
        [Required]
        public string? Password { get; set; }
    }
}

