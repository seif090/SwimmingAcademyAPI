using Microsoft.AspNetCore.Mvc;
using SwimmingAcademy.Interfaces;

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
            var result = await _repo.LoginAsync(request.UserId, request.Password);
            return Ok(result);
        }
    }

    public class LoginRequest
    {
        public int UserId { get; set; }
        public string Password { get; set; }
    }
}

