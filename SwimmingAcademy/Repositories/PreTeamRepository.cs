using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using SwimmingAcademy.Data;
using SwimmingAcademy.DTOs;
using SwimmingAcademy.Interfaces;
using System.Data;

namespace SwimmingAcademy.Repositories
{
    public class PreTeamRepository : IPreTeamRepository
    {
        private readonly SwimmingAcademyContext _context;

        public PreTeamRepository(SwimmingAcademyContext context)
        {
            _context = context;
        }

        public async Task<long> CreatePreTeamAsync(CreatePTeamRequest req)
        {
            try
            {
                using var conn = _context.Database.GetDbConnection();
                using var command = conn.CreateCommand();

                command.CommandText = "[PreTeam].[Create_PTeam]";
                command.CommandType = CommandType.StoredProcedure;

                
                command.Parameters.AddRange(new[]
                {
                    new SqlParameter("@PTeamLevel", req.PreTeamLevel == default(short) ? (object)DBNull.Value : req.PreTeamLevel),
                    new SqlParameter("@CoachID", req.CoachID),
                    new SqlParameter("@FirstDay", req.FirstDay ?? (object)DBNull.Value),
                    new SqlParameter("@SecondDay", req.SecondDay ?? (object)DBNull.Value),
                    new SqlParameter("@ThirdDay", req.ThirdDay ?? (object)DBNull.Value),
                    new SqlParameter("@site", req.Site == default(short) ? (object)DBNull.Value : req.Site),
                    new SqlParameter("@user", req.User == default(int) ? (object)DBNull.Value : req.User),
                    new SqlParameter("@startTime", req.StartTime),
                    new SqlParameter("@EndTime", req.EndTime)
                });
                   

                if (conn.State != ConnectionState.Open)
                    await conn.OpenAsync();

                using var reader = await command.ExecuteReaderAsync();
                return await reader.ReadAsync() ? reader.GetInt64(0) : 0;
            }
            catch (Exception ex)
            {
                // log error or rethrow
                throw new Exception("Error while creating pre-team.", ex);
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
            }
            catch (Exception ex)
            {
                throw new Exception("Error while searching pre-teams.", ex);
            }

            return result;
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
            }
            catch (Exception ex)
            {
                throw new Exception("Error fetching swimmer pre-team details.", ex);
            }

            return result;
        }

        public async Task<bool> UpdatePTeamAsync(UpdatePTeamRequest req)
        {
            try
            {
                using var conn = _context.Database.GetDbConnection();
                using var command = conn.CreateCommand();

                command.CommandText = "[PreTeam].[Updated]";
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddRange(new[]
                {
                    new SqlParameter("@PTeamID", req.PTeamID),
                    new SqlParameter("@coachID", req.CoachID),
                    new SqlParameter("@FirstDay", req.FirstDay),
                    new SqlParameter("@SecondDay", req.SecondDay),
                    new SqlParameter("@ThirdDay", req.ThirdDay),
                    new SqlParameter("@StartTime", req.StartTime),
                    new SqlParameter("@EndTime", req.EndTime),
                    new SqlParameter("@site", req.Site == default(short) ?(object) DBNull.Value : req.Site),
                    new SqlParameter("@user", req.User == default(short) ?(object) DBNull.Value : req.User)
                });

                if (conn.State != ConnectionState.Open)
                    await conn.OpenAsync();

                await command.ExecuteNonQueryAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Error updating pre-team.", ex);
            }
        }

        public async Task<bool> EndPTeamAsync(EndPTeamRequest req)
        {
            try
            {
                using var conn = _context.Database.GetDbConnection();
                using var command = conn.CreateCommand();

                command.CommandText = "[PreTeam].[EndPreTeam]";
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddRange(new[]
                {
                    new SqlParameter("@PteamID", req.PTeamID),
                    new SqlParameter("@userID", req.UserID),
                    new SqlParameter("@site", req.Site == default(short) ? (object)DBNull.Value : req.Site)
                });

                if (conn.State != ConnectionState.Open)
                    await conn.OpenAsync();

                await command.ExecuteNonQueryAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Error ending pre-team.", ex);
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

                        StartTime = reader.IsDBNull(4)
                            ? ""
                            : TimeSpan.TryParse(reader.GetValue(4)?.ToString(), out var start)
                                ? start.ToString(@"hh\:mm")
                                : "",

                        EndTime = reader.IsDBNull(5)
                            ? ""
                            : TimeSpan.TryParse(reader.GetValue(5)?.ToString(), out var end)
                                ? end.ToString(@"hh\:mm")
                                : "",

                        IsEnded = !reader.IsDBNull(6) && reader.GetBoolean(6)
                    };
                }

                return null;
            }
            catch (Exception ex)
            {
                throw new Exception("Error getting pre-team tab details.", ex);
            }
        }

        public async Task<IEnumerable<ActionNameDto>> SearchActionsAsync(PreTeamActionSearchRequest req)
        {
            var result = new List<ActionNameDto>();

            try
            {
                using var conn = _context.Database.GetDbConnection();
                using var command = conn.CreateCommand();

                command.CommandText = "[PreTeam].[SerachActions]";
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add(new SqlParameter("@UserID", req.UserID));
                command.Parameters.Add(new SqlParameter("@PTeamID", req.PTeamID));
                command.Parameters.Add(new SqlParameter("@userSite", req.UserSite == default(short) ? (object)DBNull.Value : req.UserSite));

                if (conn.State != ConnectionState.Open)
                    await conn.OpenAsync();

                using var reader = await command.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    result.Add(new ActionNameDto
                    {
                        ActionName = reader.IsDBNull(0) ? "" : reader.GetString(0)
                    });
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error searching pre-team actions.", ex);
            }

            return result;
        }
}
}