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
            using var conn = _context.Database.GetDbConnection();
            using var command = conn.CreateCommand();

            command.CommandText = "[PreTeam].[Create_PTeam]";
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddRange(new[]
            {
        new SqlParameter("@PTeamLevel", req.PreTeamLevel),
        new SqlParameter("@CoachID", req.CoachID),
        new SqlParameter("@FirstDay", req.FirstDay),
        new SqlParameter("@SecondDay", req.SecondDay),
        new SqlParameter("@ThirdDay", req.ThirdDay),
        new SqlParameter("@site", req.Site),
        new SqlParameter("@user", req.User),
        new SqlParameter("@startTime", req.StartTime),
        new SqlParameter("@EndTime", req.EndTime)
    });

            if (conn.State != ConnectionState.Open)
                await conn.OpenAsync();

            using var reader = await command.ExecuteReaderAsync();
            if (await reader.ReadAsync())
                return reader.GetInt64(0); // If the SP returns something (optional)

            return 0;
        }
        public async Task<IEnumerable<PTeamSearchResultDto>> SearchPTeamAsync(PTeamSearchRequest request)
        {
            var result = new List<PTeamSearchResultDto>();

            using var conn = _context.Database.GetDbConnection();
            using var command = conn.CreateCommand();
            command.CommandText = "[PreTeam].[ShowPTeam]";
            command.CommandType = CommandType.StoredProcedure;

            // Supply only one parameter as per stored procedure logic
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
                    CoachName = reader.IsDBNull(0) ? "" : reader.GetString(0),
                    Level = reader.IsDBNull(1) ? "" : reader.GetString(1),
                    Days = reader.IsDBNull(2) ? "" : reader.GetString(2),
                    FromTo = reader.IsDBNull(3) ? "" : reader.GetString(3)
                });
            }

            return result;
        }
        public async Task<IEnumerable<SwimmerPTeamDetailsDto>> GetSwimmerPTeamDetailsAsync(long pTeamId)

        {
            var result = new List<SwimmerPTeamDetailsDto>();

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

        public async Task<bool> UpdatePTeamAsync(UpdatePTeamRequest req)
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
        new SqlParameter("@site", req.Site),
        new SqlParameter("@user", req.User)
    });

            if (conn.State != ConnectionState.Open)
                await conn.OpenAsync();

            await command.ExecuteNonQueryAsync();
            return true;
        }
        public async Task<bool> EndPTeamAsync(EndPTeamRequest req)
        {
            using var conn = _context.Database.GetDbConnection();
            using var command = conn.CreateCommand();

            command.CommandText = "[PreTeam].[EndPreTeam]";
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddRange(new[]
            {
        new SqlParameter("@PteamID", req.PTeamID),
        new SqlParameter("@userID", req.UserID),
        new SqlParameter("@site", req.Site)
    });

            if (conn.State != ConnectionState.Open)
                await conn.OpenAsync();

            await command.ExecuteNonQueryAsync();
            return true;
        }
        public async Task<PTeamDetailsTabDto?> GetPTeamDetailsTabAsync(long pTeamId)
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
                    FullName = reader.IsDBNull(0) ? string.Empty : reader.GetString(0),
                    FirstDay = reader.IsDBNull(1) ? string.Empty : reader.GetString(1),
                    SecondDay = reader.IsDBNull(2) ? string.Empty : reader.GetString(2),
                    ThirdDay = reader.IsDBNull(3) ? string.Empty : reader.GetString(3),

                    StartTime = reader.IsDBNull(4)
                        ? ""
                        : TimeSpan.TryParse(reader.GetValue(4).ToString(), out var start)
                            ? start.ToString(@"hh\:mm")
                            : "",

                    EndTime = reader.IsDBNull(5)
                        ? ""
                        : TimeSpan.TryParse(reader.GetValue(5).ToString(), out var end)
                            ? end.ToString(@"hh\:mm")
                            : "",

                    IsEnded = !reader.IsDBNull(6) && reader.GetBoolean(6)
                };
            }

            return null;
        }



        public async Task<IEnumerable<ActionNameDto>> SearchActionsAsync(PreTeamActionSearchRequest req)
        {
            var result = new List<ActionNameDto>();

            using var conn = _context.Database.GetDbConnection();
            using var command = conn.CreateCommand();

            command.CommandText = "[PreTeam].[SerachActions]";
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new SqlParameter("@UserID", req.UserID));
            command.Parameters.Add(new SqlParameter("@PTeamID", req.PTeamID));
            command.Parameters.Add(new SqlParameter("@userSite", req.UserSite));

            if (conn.State != ConnectionState.Open)
                await conn.OpenAsync();

            using var reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                result.Add(new ActionNameDto
                {
                    ActionName = reader.GetString(0)
                });
            }

            return result;
        }


    }
}
