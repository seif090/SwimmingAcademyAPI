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

                command.Parameters.Add(new SqlParameter("@Type", (object)req.Type));
                command.Parameters.Add(new SqlParameter("@startTime", req.StartTime));
                command.Parameters.Add(new SqlParameter("@FirstDay", (object?)req.FirstDay ?? DBNull.Value));
                command.Parameters.Add(new SqlParameter("@site", (object)req.Site));

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
                _logger.LogError(ex, "Error fetching free coaches");
                throw new Exception("An error occurred while fetching free coaches.");
            }

            return result;
        }

        public async Task<CoachDTO?> GetByIdAsync(int id)
        {
            try
            {
                return await _context.Coaches
                    .Where(c => c.CoachID == id)
                    .Select(c => new CoachDTO
                    {
                        CoachID = c.CoachID
                        // Map other properties as needed
                    })
                    .FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching coach by id {CoachID}", id);
                throw new Exception("An error occurred while fetching the coach.");
            }
        }

        public async Task<int> CreateCoachAsync(CoachDTO dto)
        {
            try
            {
                var coach = new Coach
                {
                    // Map properties from dto
                    // Example: FullName = dto.FullName,
                };
                _context.Coaches.Add(coach);
                await _context.SaveChangesAsync();
                return coach.CoachID;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating coach");
                throw new Exception("An error occurred while creating the coach.");
            }
        }

        public async Task<bool> UpdateCoachAsync(CoachDTO dto)
        {
            try
            {
                var coach = await _context.Coaches.FindAsync(dto.CoachID);
                if (coach == null)
                    return false;

                // Map updated properties from dto
                // Example: coach.FullName = dto.FullName;

                _context.Coaches.Update(coach);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating coach");
                throw new Exception("An error occurred while updating the coach.");
            }
        }

        public async Task<bool> DeleteCoachAsync(int coachId)
        {
            try
            {
                var coach = await _context.Coaches.FindAsync(coachId);
                if (coach == null)
                    return false;

                _context.Coaches.Remove(coach);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting coach");
                throw new Exception("An error occurred while deleting the coach.");
            }
        }
    }
}

