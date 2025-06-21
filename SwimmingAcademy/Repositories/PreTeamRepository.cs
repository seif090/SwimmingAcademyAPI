using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SwimmingAcademy.Data;
using SwimmingAcademy.DTOs;
using SwimmingAcademy.Interfaces;
using System.Data;
using System.Threading.Tasks;

namespace SwimmingAcademy.Repositories
{
    public class PreTeamRepository : IPreTeamRepository
    {
        private readonly SwimmingAcademyContext _context;
        private readonly ILogger<PreTeamRepository> _logger;

        public PreTeamRepository(SwimmingAcademyContext context, ILogger<PreTeamRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<bool> EndPTeamAsync(EndPreTeamRequest request)
        {
            try
            {
                using var conn = _context.Database.GetDbConnection();
                using var command = conn.CreateCommand();
                command.CommandText = "[PreTeam].[EndPreTeam]";
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add(new SqlParameter("@PteamID", request.PteamID));
                command.Parameters.Add(new SqlParameter("@userID", request.UserID));
                command.Parameters.Add(new SqlParameter("@site", request.Site));

                if (conn.State != ConnectionState.Open)
                    await conn.OpenAsync();

                await command.ExecuteNonQueryAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error ending PreTeam for PteamID {PteamID}", request.PteamID);
                throw new Exception("An error occurred while ending the PreTeam.");
            }
        }
        public async Task<IEnumerable<ActionNameDto>> SearchActionsAsync(PreTeamActionSearchRequest request)
        {
            var actions = new List<ActionNameDto>();
            try
            {
                using var conn = _context.Database.GetDbConnection();
                using var command = conn.CreateCommand();

                command.CommandText = "[PreTeam].[SerachActions]";
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add(new SqlParameter("@UserID", request.UserID));
                command.Parameters.Add(new SqlParameter("@PTeamID", request.PTeamID));
                command.Parameters.Add(new SqlParameter("@userSite", request.UserSite));

                if (conn.State != ConnectionState.Open)
                    await conn.OpenAsync();

                using var reader = await command.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    actions.Add(new ActionNameDto
                    {
                        ActionName = reader.IsDBNull(0) ? "" : reader.GetString(0)
                    });
                }
                return actions;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error searching PreTeam actions for UserID {UserID}, PTeamID {PTeamID}, UserSite {UserSite}", request.UserID, request.PTeamID, request.UserSite);
                throw new Exception("An error occurred while searching PreTeam actions.");
            }
        }
        public async Task<IEnumerable<PTeamSearchResultDto>> SearchPTeamAsync(PTeamSearchRequest request)
        {
            var result = new List<PTeamSearchResultDto>();
            try
            {
                using var conn = _context.Database.GetDbConnection();
                using var command = conn.CreateCommand();

                command.CommandText = "[PreTeam].[ShowPTeam]";
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add(new SqlParameter("@PTeamID", (object?)request.PTeamID ?? DBNull.Value));
                command.Parameters.Add(new SqlParameter("@FullName", (object?)request.FullName ?? DBNull.Value));
                command.Parameters.Add(new SqlParameter("@level", (object?)request.Level ?? DBNull.Value));

                if (conn.State != ConnectionState.Open)
                    await conn.OpenAsync();

                using var reader = await command.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    result.Add(new PTeamSearchResultDto
                    {
                        PTeamID = reader.IsDBNull(0) ? 0 : reader.GetInt64(0),
                        CoachName = reader.IsDBNull(1) ? "" : reader.GetString(1),
                        Level = reader.IsDBNull(2) ? "" : reader.GetString(2),
                        Days = reader.IsDBNull(3) ? "" : reader.GetString(3),
                        FromTo = reader.IsDBNull(4) ? "" : reader.GetString(4)
                    });
                }
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error searching PreTeam with request {@Request}", request);
                throw new Exception("An error occurred while searching PreTeam.");
            }
        }
        public async Task<IEnumerable<SwimmerPTeamDetailsDto>> GetSwimmerPTeamDetailsAsync(long pTeamId)
        {
            var result = new List<SwimmerPTeamDetailsDto>();
            try
            {
                using var conn = _context.Database.GetDbConnection();
                using var command = conn.CreateCommand();

                command.CommandText = "[PreTeam].[SwimmerDetails_Tab]";
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add(new SqlParameter("@pteamID", pTeamId));

                if (conn.State != ConnectionState.Open)
                    await conn.OpenAsync();

                using var reader = await command.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    result.Add(new SwimmerPTeamDetailsDto
                    {
                        FullName = reader.IsDBNull(0) ? "" : reader.GetString(0),
                        Attendance = reader.IsDBNull(1) ? "" : reader.GetString(1),
                        SwimmerLevel = reader.IsDBNull(2) ? "" : reader.GetString(2),
                        LastStar = reader.IsDBNull(3) ? "" : reader.GetString(3)
                    });
                }
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching swimmer PreTeam details for PTeamID {PTeamID}", pTeamId);
                throw new Exception("An error occurred while fetching swimmer PreTeam details.");
            }
        }
        public async Task<bool> UpdatePTeamAsync(UpdatePTeamRequest request)
        {
            try
            {
                using var conn = _context.Database.GetDbConnection();
                using var command = conn.CreateCommand();

                command.CommandText = "[PreTeam].[Updated]";
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add(new SqlParameter("@PTeamID", request.PTeamID));
                command.Parameters.Add(new SqlParameter("@coachID", request.CoachID));
                command.Parameters.Add(new SqlParameter("@FirstDay", request.FirstDay));
                command.Parameters.Add(new SqlParameter("@SecondDay", request.SecondDay));
                command.Parameters.Add(new SqlParameter("@ThirdDay", request.ThirdDay));
                command.Parameters.Add(new SqlParameter("@StartTime", request.StartTime));
                command.Parameters.Add(new SqlParameter("@EndTime", request.EndTime));
                command.Parameters.Add(new SqlParameter("@site", request.Site));
                command.Parameters.Add(new SqlParameter("@user", request.User));

                if (conn.State != ConnectionState.Open)
                    await conn.OpenAsync();

                await command.ExecuteNonQueryAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating PreTeam with ID {PTeamID}", request.PTeamID);
                throw new Exception("An error occurred while updating the PreTeam.");
            }
        }
        public async Task<long> CreatePreTeamAsync(CreatePTeamRequest request)
        {
            try
            {
                using var conn = _context.Database.GetDbConnection();
                using var command = conn.CreateCommand();

                command.CommandText = "[PreTeam].[Create_PTeam]";
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add(new SqlParameter("@PTeamLevel", request.PreTeamLevel));
                command.Parameters.Add(new SqlParameter("@CoachID", request.CoachID));
                command.Parameters.Add(new SqlParameter("@FirstDay", request.FirstDay ?? (object)DBNull.Value));
                command.Parameters.Add(new SqlParameter("@SecondDay", request.SecondDay ?? (object)DBNull.Value));
                command.Parameters.Add(new SqlParameter("@ThirdDay", request.ThirdDay ?? (object)DBNull.Value));
                command.Parameters.Add(new SqlParameter("@site", request.Site));
                command.Parameters.Add(new SqlParameter("@user", request.User));
                command.Parameters.Add(new SqlParameter("@startTime", request.StartTime));
                command.Parameters.Add(new SqlParameter("@EndTime", request.EndTime));

                if (conn.State != ConnectionState.Open)
                    await conn.OpenAsync();

                var result = await command.ExecuteScalarAsync();
                return result is null ? 0 : Convert.ToInt64(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating PreTeam");
                throw new Exception("An error occurred while creating the PreTeam.");
            }
        }
        public async Task<PTeamDetailsTabDto?> GetPTeamDetailsTabAsync(long pTeamId)
        {
            try
            {
                using var conn = _context.Database.GetDbConnection();
                using var command = conn.CreateCommand();

                command.CommandText = "[PreTeam].[PTeamDetails_tab]";
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add(new SqlParameter("@PTeamID", pTeamId));

                if (conn.State != ConnectionState.Open)
                    await conn.OpenAsync();

                using var reader = await command.ExecuteReaderAsync();
                if (await reader.ReadAsync())
                {
                    return new PTeamDetailsTabDto
                    {
                        FullName = reader.IsDBNull(0) ? "" : reader.GetString(0),
                        FirstDay = reader.IsDBNull(1) ? "" : reader.GetString(1),
                        SecondDay = reader.IsDBNull(2) ? "" : reader.GetString(2),
                        ThirdDay = reader.IsDBNull(3) ? "" : reader.GetString(3),
                        StartTime = reader.IsDBNull(4) ? "" : reader.GetValue(4)?.ToString() ?? "",
                        EndTime = reader.IsDBNull(5) ? "" : reader.GetValue(5)?.ToString() ?? "",
                        IsEnded = !reader.IsDBNull(6) && reader.GetBoolean(6)
                    };
                }
                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching PTeam details tab for PTeamID {PTeamID}", pTeamId);
                throw new Exception("An error occurred while fetching PTeam details tab.");
            }
        }
    }
}