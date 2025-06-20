using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using SwimmingAcademy.Data;
using SwimmingAcademy.DTOs;
using SwimmingAcademy.Interfaces;
using System.Data;

namespace SwimmingAcademy.Repositories
{
    public class CoachRepository : ICoachRepository
    {
        private readonly SwimmingAcademyContext _context;

        public CoachRepository(SwimmingAcademyContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<FreeCoachDto>> GetFreeCoachesAsync(FreeCoachFilterRequest req)
        {
            var result = new List<FreeCoachDto>();

            try
            {
                if (req == null)
                    throw new ArgumentNullException(nameof(req));

                using var conn = _context.Database.GetDbConnection();
                using var command = conn.CreateCommand();
                command.CommandText = "[dbo].[FreeCoaches]";
                command.CommandType = CommandType.StoredProcedure;

                // Fix for CS0019: Explicitly cast 'short' to 'object' to avoid the error
                command.Parameters.Add(new SqlParameter("@Type", (object)req.Type ?? DBNull.Value));
                command.Parameters.Add(new SqlParameter("@startTime", req.StartTime));
                command.Parameters.Add(new SqlParameter("@FirstDay", req.FirstDay));
                command.Parameters.Add(new SqlParameter("@site", (object)req.Site ?? DBNull.Value));

                if (conn.State != ConnectionState.Open)
                    await conn.OpenAsync();

                using var reader = await command.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    result.Add(new FreeCoachDto
                    {
                        Name = !reader.IsDBNull(0) ? reader.GetString(0) : "Unknown"
                    });
                }
            }
            catch (Exception ex)
            {
                // Optional: log exception here
                throw new Exception("Error fetching free coaches", ex);
            }

            return result;
        }

    }

}

