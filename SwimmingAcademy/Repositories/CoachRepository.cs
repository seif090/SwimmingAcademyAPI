using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SwimmingAcademy.Data;
using SwimmingAcademy.DTOs;
using SwimmingAcademy.Interfaces;
using SwimmingAcademy.Models;
using System.Data;

namespace SwimmingAcademy.Repositories
{
    public class CoachRepository : ICoachRepository
    {
        private readonly SwimmingAcademyContext _context;
        private readonly ILogger<CoachRepository> _logger;

        public CoachRepository(SwimmingAcademyContext context, ILogger<CoachRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IEnumerable<FreeCoachDto>> GetFreeCoachesAsync(FreeCoachFilterRequest request)
        {
            var result = new List<FreeCoachDto>();
            try
            {
                using var conn = _context.Database.GetDbConnection();
                using var command = conn.CreateCommand();

                command.CommandText = "[dbo].[FreeCoaches]";
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add(new SqlParameter("@Type", request.Type));
                command.Parameters.Add(new SqlParameter("@startTime", request.StartTime));
                command.Parameters.Add(new SqlParameter("@FirstDay", request.FirstDay));
                command.Parameters.Add(new SqlParameter("@site", request.Site));

                if (conn.State != ConnectionState.Open)
                    await conn.OpenAsync();

                using var reader = await command.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    result.Add(new FreeCoachDto
                    {
                        Name = reader.IsDBNull(0) ? "" : reader.GetString(0)
                    });
                }

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetFreeCoachesAsync");
                throw new Exception("An error occurred while retrieving free coaches.");
            }
        }
    }
}

