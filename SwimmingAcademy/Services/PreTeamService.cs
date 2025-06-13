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
    public class PreTeamService : IPreTeamService
    {
        private readonly SwimmingAcademyContext _context;
        private readonly IConfiguration _configuration;

        public PreTeamService(SwimmingAcademyContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<long> CreatePreTeamAsync(CreatePreTeamDto dto)
        {
            long createdPTeamId = 0;

            using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            using var cmd = new SqlCommand("[PreTeam].[Create_PTeam]", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.AddWithValue("@PTeamLevel", dto.PTeamLevel);
            cmd.Parameters.AddWithValue("@CoachID", dto.CoachID);
            cmd.Parameters.AddWithValue("@FirstDay", dto.FirstDay);
            cmd.Parameters.AddWithValue("@SecondDay", dto.SecondDay);
            cmd.Parameters.AddWithValue("@ThirdDay", dto.ThirdDay);
            cmd.Parameters.AddWithValue("@site", dto.Site);
            cmd.Parameters.AddWithValue("@user", dto.User);
            cmd.Parameters.AddWithValue("@startTime", dto.StartTime);
            cmd.Parameters.AddWithValue("@EndTime", dto.EndTime);

            // Get PTeamID from output of SCOPE_IDENTITY
            cmd.Parameters.Add("@PteamID", SqlDbType.BigInt).Direction = ParameterDirection.Output;

            await conn.OpenAsync();
            await cmd.ExecuteNonQueryAsync();

            // Return the identity of inserted PreTeam.Info record
            return Convert.ToInt64(cmd.Parameters["@PteamID"].Value);
        }

        public async Task<bool> EndPreTeamAsync(EndPreTeamDto dto)
        {
            using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            using var cmd = new SqlCommand("[PreTeam].[EndPreTeam]", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.AddWithValue("@PteamID", dto.PteamID);
            cmd.Parameters.AddWithValue("@userID", dto.UserID);
            cmd.Parameters.AddWithValue("@site", dto.Site);

            await conn.OpenAsync();
            int rowsAffected = await cmd.ExecuteNonQueryAsync();

            return rowsAffected > 0; // Return true if SP executed
        }

        public async Task<PreTeamDetailsDto?> GetPreTeamDetailsAsync(long preTeamId)
        {
            using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            using var cmd = new SqlCommand("[PreTeam].[PTeamDetails_tab]", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.AddWithValue("@PTeamID", preTeamId);

            await conn.OpenAsync();
            using var reader = await cmd.ExecuteReaderAsync();

            if (await reader.ReadAsync())
            {
                return new PreTeamDetailsDto
                {
                    CoachName = reader["FullName"].ToString() ?? "",
                    FirstDay = reader["FirstDay"].ToString() ?? "",
                    SecondDay = reader["SecondDay"].ToString() ?? "",
                    ThirdDay = reader["ThirdDay"].ToString() ?? "",
                    StartTime = (TimeSpan)reader["StartTime"],
                    EndTime = (TimeSpan)reader["EndTime"],
                    IsEnded = Convert.ToBoolean(reader["ISEnded"])
                };
            }

            return null;
        }

        public async Task<List<SearchActionResponseDto>> SearchActionsAsync(SearchActionRequestDto request)
        {
            var actions = new List<SearchActionResponseDto>();

            using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            using var cmd = new SqlCommand("[PreTeam].[SerachActions]", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.AddWithValue("@UserID", request.UserID);
            cmd.Parameters.AddWithValue("@PTeamID", request.PTeamID);
            cmd.Parameters.AddWithValue("@userSite", request.UserSite);

            await conn.OpenAsync();
            using var reader = await cmd.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                actions.Add(new SearchActionResponseDto
                {
                    ActionName = reader["ActionName"].ToString() ?? ""
                });
            }

            return actions;
        }

        public async Task<List<ShowPreTeamResponseDto>> ShowPreTeamAsync(ShowPreTeamRequestDto request)
        {
            var result = new List<ShowPreTeamResponseDto>();

            using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            using var cmd = new SqlCommand("[PreTeam].[ShowPTeam]", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.AddWithValue("@PTeamID", request.PTeamID ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@FullName", request.FullName ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@level", request.Level ?? (object)DBNull.Value);

            await conn.OpenAsync();
            using var reader = await cmd.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                result.Add(new ShowPreTeamResponseDto
                {
                    CoachName = reader["CoachName"].ToString() ?? "",
                    Level = reader["Level"].ToString() ?? "",
                    Days = reader["Days"].ToString() ?? "",
                    FromTo = reader["FromTo"].ToString() ?? ""
                });
            }

            return result;
        }

        public async Task<List<SwimmerDetailsTabDto>> GetSwimmerDetailsAsync(long pTeamId)
        {
            var result = new List<SwimmerDetailsTabDto>();

            using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            using var cmd = new SqlCommand("[PreTeam].[SwimmerDetails_Tab]", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.AddWithValue("@pteamID", pTeamId);

            await conn.OpenAsync();
            using var reader = await cmd.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                result.Add(new SwimmerDetailsTabDto
                {
                    FullName = reader["FullName"].ToString() ?? "",
                    Attendence = reader["Attendence"].ToString() ?? "",
                    SwimmerLevel = reader["SwimmerLevel"].ToString() ?? "",
                    LastStar = reader["LastStar"].ToString() ?? ""
                });
            }

            return result;
        }

        public async Task<bool> UpdatePreTeamAsync(UpdatePreTeamDto dto)
        {
            using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            using var cmd = new SqlCommand("[PreTeam].[Updated]", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.AddWithValue("@PTeamID", dto.PTeamID);
            cmd.Parameters.AddWithValue("@coachID", dto.CoachID);
            cmd.Parameters.AddWithValue("@FirstDay", dto.FirstDay);
            cmd.Parameters.AddWithValue("@SecondDay", dto.SecondDay);
            cmd.Parameters.AddWithValue("@ThirdDay", dto.ThirdDay);
            cmd.Parameters.AddWithValue("@StartTime", dto.StartTime);
            cmd.Parameters.AddWithValue("@EndTime", dto.EndTime);
            cmd.Parameters.AddWithValue("@site", dto.Site);
            cmd.Parameters.AddWithValue("@user", dto.User);

            await conn.OpenAsync();
            int rowsAffected = await cmd.ExecuteNonQueryAsync();

            return rowsAffected > 0;
        }

    }
}
