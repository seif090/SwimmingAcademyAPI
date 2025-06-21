using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SwimmingAcademy.Data;
using SwimmingAcademy.DTOs;
using SwimmingAcademy.Helpers;
using SwimmingAcademy.Interfaces;
using SwimmingAcademy.Models;
using System.Data;

namespace SwimmingAcademy.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        private readonly SwimmingAcademyContext _context;
        private readonly JwtTokenGenerator _jwtTokenGenerator;
        private readonly IPasswordHasher<user> _passwordHasher;
        private readonly ILogger<AuthRepository> _logger;

        public AuthRepository(SwimmingAcademyContext context, JwtTokenGenerator jwtTokenGenerator, IPasswordHasher<user> passwordHasher, ILogger<AuthRepository> logger)
        {
            _context = context;
            _jwtTokenGenerator = jwtTokenGenerator;
            _passwordHasher = passwordHasher;
            _logger = logger;
        }

        public async Task<UserLoginResponseDto> UserLogInAsync(UserLoginRequestDto request)
        {
            var response = new UserLoginResponseDto();
            try
            {
                using var conn = _context.Database.GetDbConnection();
                using var command = conn.CreateCommand();

                command.CommandText = "[dbo].[UserLogIn]";
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add(new SqlParameter("@UserID", request.UserID));
                command.Parameters.Add(new SqlParameter("@Password", request.Password));

                if (conn.State != ConnectionState.Open)
                    await conn.OpenAsync();

                using var reader = await command.ExecuteReaderAsync();

                // 1st result: Message
                if (await reader.ReadAsync())
                    response.Message = reader.GetValue(0)?.ToString() ?? "";

                // 2nd result: Sites
                if (await reader.NextResultAsync())
                {
                    while (await reader.ReadAsync())
                        response.Sites.Add(reader.GetValue(0)?.ToString() ?? "");
                }

                // 3rd result: Modules
                if (await reader.NextResultAsync())
                {
                    while (await reader.ReadAsync())
                        response.Modules.Add(reader.GetValue(0)?.ToString() ?? "");
                }

                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in UserLogInAsync");
                throw new Exception("An error occurred while logging in.");
            }
        }
    }
}
