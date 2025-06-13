using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SwimmingAcademy.Data;
using SwimmingAcademy.DTOs;
using SwimmingAcademy.Models;
using SwimmingAcademy.Services.Interfaces;
using System.Data;
namespace SwimmingAcademy.Services
{
    public class UserService : IUserService
    {
        private readonly SwimmingAcademyContext _context;
        private readonly IConfiguration _configuration;

        public UserService(SwimmingAcademyContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<LoginResponseDto> LoginAsync(LoginDto request)
        {
            var response = new LoginResponseDto();

            using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            using var cmd = new SqlCommand("[dbo].[UserLogIn]", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.AddWithValue("@UserID", request.UserId);
            cmd.Parameters.AddWithValue("@Password", request.Password);

            await conn.OpenAsync();

            using var reader = await cmd.ExecuteReaderAsync();

            // 1st Result: Status Message
            if (reader.Read())
            {
                response.Message = reader.GetString(0);
            }

            // If login failed or user is disabled, no need to continue
            if (response.Message != "Log in succsess")
                return response;

            // 2nd Result Set: Sites
            if (await reader.NextResultAsync())
            {
                while (await reader.ReadAsync())
                {
                    response.Sites.Add(reader.GetString(0));
                }
            }

            // 3rd Result Set: Modules
            if (await reader.NextResultAsync())
            {
                while (await reader.ReadAsync())
                {
                    response.Modules.Add(reader.GetString(0));
                }
            }

            return response;
        }
    }
}

