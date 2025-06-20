using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using SwimmingAcademy.Data;
using SwimmingAcademy.DTOs;
using SwimmingAcademy.Interfaces;
using System.Data;

namespace SwimmingAcademy.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        private readonly SwimmingAcademyContext _context;

        public AuthRepository(SwimmingAcademyContext context)
        {
            _context = context;
        }

        public async Task<LoginResultDTO> LoginAsync(int userId, string password)
        {
            var result = new LoginResultDTO();

            try
            {
                using var conn = _context.Database.GetDbConnection();
                using var command = conn.CreateCommand();
                command.CommandText = "dbo.UserLogIn";
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add(new SqlParameter("@UserID", userId));
                command.Parameters.Add(new SqlParameter("@Password", password));

                if (conn.State != ConnectionState.Open)
                    await conn.OpenAsync();

                using var reader = await command.ExecuteReaderAsync();

                if (reader.HasRows && await reader.ReadAsync())
                {
                    result.Message = !reader.IsDBNull(0) ? reader.GetString(0) : string.Empty;

                    if (await reader.NextResultAsync())
                    {
                        while (await reader.ReadAsync())
                            result.Sites.Add(reader.GetString(0));
                    }

                    if (await reader.NextResultAsync())
                    {
                        while (await reader.ReadAsync())
                            result.Modules.Add(reader.GetString(0));
                    }
                }
                else
                {
                    result.Message = "Incorrect UserName / Password";
                }
            }
            catch (Exception ex)
            {
                // Optional: log or throw a known exception to the controller
                result.Message = $"Internal server error: {ex.Message}";
                // Optionally rethrow or return null to controller for 500
            }

            return result;
        }

    }
}
