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

            using var conn = _context.Database.GetDbConnection();
            using var command = conn.CreateCommand();
            command.CommandText = "dbo.UserLogIn";
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new SqlParameter("@UserID", userId));
            command.Parameters.Add(new SqlParameter("@Password", password));

            if (conn.State != ConnectionState.Open)
                await conn.OpenAsync();

            using var reader = await command.ExecuteReaderAsync();

            if (reader.HasRows)
            {
                // First result: message
                await reader.ReadAsync();
                result.Message = reader.GetString(0);

                // Second result: sites
                if (await reader.NextResultAsync())
                {
                    while (await reader.ReadAsync())
                        result.Sites.Add(reader.GetString(0));
                }

                // Third result: modules
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

            return result;
        }
    }
}
