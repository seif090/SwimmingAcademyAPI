using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SwimmingAcademy.DTOs;
using SwimmingAcademy.Services.Interfaces;
using SwimmingAcademy.Models;

namespace SwimmingAcademy.Controllers
{
    
        [Authorize]
        [ApiController]
        [Route("api/[controller]")]
        public class UserController : ControllerBase
        {
            private readonly IUserService service;
            private readonly IConfiguration _config;

            public UserController(IUserService service, IConfiguration config)
            {
                this.service = service;
                _config = config;
            }

        [HttpPost("Login")]
        public async Task<ActionResult<LoginResponseDto>> Login(LoginDto request)
        {
            var result = await service.LoginAsync(request);

            if (result.Message == "Log in succsess")
                return Ok(result);

            return BadRequest(result);
        }
    }
}
