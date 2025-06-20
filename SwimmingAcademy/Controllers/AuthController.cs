using Microsoft.AspNetCore.Mvc;
using SwimmingAcademy.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace SwimmingAcademy.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _repo;

        public AuthController(IAuthRepository repo)
        {
            _repo = repo;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            if (string.IsNullOrEmpty(request.Password))
            {
                return BadRequest("Password is required.");
            }

            try
            {
                var result = await _repo.LoginAsync(request.UserId, request.Password);

                if (result == null)
                {
                    return Unauthorized("Invalid credentials.");
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                // Log the exception using ILogger (recommended) or return error
                return StatusCode(500, $"Internal server error: {ex.Message}");
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

