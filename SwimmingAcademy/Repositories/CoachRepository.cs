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

            using var conn = _context.Database.GetDbConnection();
            using var command = conn.CreateCommand();
            command.CommandText = "[dbo].[FreeCoaches]";
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new SqlParameter("@Type", req.Type));
            command.Parameters.Add(new SqlParameter("@startTime", req.StartTime));
            command.Parameters.Add(new SqlParameter("@FirstDay", req.FirstDay));
            command.Parameters.Add(new SqlParameter("@site", req.Site));

            if (conn.State != ConnectionState.Open)
                await conn.OpenAsync();

            using var reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                result.Add(new FreeCoachDto
                {
                    Name = reader.GetString(0)
                });
            }

            return result;
        }
    }

}

