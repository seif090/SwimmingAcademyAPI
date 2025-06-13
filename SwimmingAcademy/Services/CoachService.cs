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
    public class CoachService : ICoachService
    {
        private readonly SwimmingAcademyContext _context;
        private readonly IConfiguration _configuration;

        public CoachService(SwimmingAcademyContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<List<FreeCoachDto>> GetFreeCoachesAsync(FreeCoachRequestDto request)
        {
            var freeCoaches = new List<FreeCoachDto>();

            using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            using var cmd = new SqlCommand("[dbo].[FreeCoaches]", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.AddWithValue("@Type", request.Type);
            cmd.Parameters.AddWithValue("@startTime", request.StartTime);
            cmd.Parameters.AddWithValue("@FirstDay", request.FirstDay);
            cmd.Parameters.AddWithValue("@site", request.Site);

            await conn.OpenAsync();

            using var reader = await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                var coach = new FreeCoachDto
                {
                    Name = reader["Name"].ToString() ?? ""
                };
                freeCoaches.Add(coach);
            }

            return freeCoaches;
        }
    }
}
