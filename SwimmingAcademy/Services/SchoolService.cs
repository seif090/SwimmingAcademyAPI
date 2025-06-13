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
    public class SchoolService : ISchoolService
    {
        private readonly SwimmingAcademyContext _context;
        private readonly IConfiguration _configuration;

        public SchoolService(SwimmingAcademyContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<long> CreateSchoolAsync(CreateSchoolDto dto)
        {
            long schoolId = 0;

            using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            using var cmd = new SqlCommand("[Schools].[Create_School]", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.AddWithValue("@SchoolLevel", dto.SchoolLevel);
            cmd.Parameters.AddWithValue("@coachID", dto.CoachID);
            cmd.Parameters.AddWithValue("@FirstDay", dto.FirstDay);
            cmd.Parameters.AddWithValue("@SecondDay", dto.SecondDay);
            cmd.Parameters.AddWithValue("@StartTime", dto.StartTime);
            cmd.Parameters.AddWithValue("@EndTime", dto.EndTime);
            cmd.Parameters.AddWithValue("@Type", dto.Type);
            cmd.Parameters.AddWithValue("@site", dto.Site);
            cmd.Parameters.AddWithValue("@user", dto.User);

            // To capture the SchoolID if returned by SELECT SCOPE_IDENTITY()
            await conn.OpenAsync();

            using var getIdCmd = new SqlCommand("SELECT CAST(SCOPE_IDENTITY() AS BIGINT);", conn);
            await cmd.ExecuteNonQueryAsync();
            schoolId = Convert.ToInt64(await getIdCmd.ExecuteScalarAsync());

            return schoolId;
        }
        public async Task<bool> EndSchoolAsync(EndSchoolDto dto)
        {
            using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            using var cmd = new SqlCommand("[Schools].[EndSchool]", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.AddWithValue("@schoolID", dto.SchoolID);
            cmd.Parameters.AddWithValue("@userID", dto.UserID);
            cmd.Parameters.AddWithValue("@site", dto.Site);

            await conn.OpenAsync();
            int rows = await cmd.ExecuteNonQueryAsync();

            return rows > 0;
        }

        public async Task<SchoolDetailsTabDto?> GetSchoolDetailsAsync(long schoolId)
        {
            using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            using var cmd = new SqlCommand("[Schools].[SchoolDetalis_Tab]", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.AddWithValue("@schoolID", schoolId);

            await conn.OpenAsync();
            using var reader = await cmd.ExecuteReaderAsync();

            if (await reader.ReadAsync())
            {
                return new SchoolDetailsTabDto
                {
                    CoachName = reader["FullName"].ToString() ?? "",
                    FirstDay = reader["FirstDay"].ToString() ?? "",
                    SecondDay = reader["SecondDay"].ToString() ?? "",
                    StartTime = (TimeSpan)reader["StartTime"],
                    EndTime = (TimeSpan)reader["EndTime"],
                    Capacity = Convert.ToInt32(reader["Capacity"]),
                    NumberOfSwimmers = Convert.ToInt32(reader["NumberOfSwimmers"]),
                    IsEnded = Convert.ToBoolean(reader["ISEnded"])
                };
            }

            return null;
        }

        public async Task<List<SearchActionResponseDto>> SearchSchoolActionsAsync(SearchSchoolActionRequestDto request)
        {
            var actions = new List<SearchActionResponseDto>();

            using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            using var cmd = new SqlCommand("[Schools].[SearchActions]", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.AddWithValue("@UserID", request.UserID);
            cmd.Parameters.AddWithValue("@SchoolID", request.SchoolID);
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

        public async Task<List<ShowSchoolResponseDto>> ShowSchoolAsync(ShowSchoolRequestDto request)
        {
            var result = new List<ShowSchoolResponseDto>();

            using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            using var cmd = new SqlCommand("[Schools].[ShowSchool]", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.AddWithValue("@SchoolID", request.SchoolID ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@FullName", request.FullName ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@level", request.Level ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@type", request.Type ?? (object)DBNull.Value);

            await conn.OpenAsync();
            using var reader = await cmd.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                result.Add(new ShowSchoolResponseDto
                {
                    CoachName = reader["CoachName"].ToString() ?? "",
                    Level = reader["Level"].ToString() ?? "",
                    Type = reader["Type"].ToString() ?? "",
                    Days = reader["Days"].ToString() ?? "",
                    FromTo = reader["FromTo"].ToString() ?? "",
                    NumberCapacity = reader["NumberCapacity"].ToString() ?? ""
                });
            }

            return result;
        }

        public async Task<List<SwimmerDetailsTabDto>> GetSchoolSwimmerDetailsAsync(long schoolId)
        {
            var result = new List<SwimmerDetailsTabDto>();

            using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            using var cmd = new SqlCommand("[Schools].[SwimmerDetails_Tab]", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.AddWithValue("@schoolID", schoolId);

            await conn.OpenAsync();
            using var reader = await cmd.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                result.Add(new SwimmerDetailsTabDto
                {
                    FullName = reader["FullName"].ToString() ?? "",
                    Attendence = reader["Attendence"].ToString() ?? "",
                    SwimmerLevel = reader["SwimmerLevel"].ToString() ?? ""
                });
            }

            return result;
        }

        public async Task<bool> UpdateSchoolAsync(UpdateSchoolDto dto)
        {
            using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            using var cmd = new SqlCommand("[Schools].[Updated]", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.AddWithValue("@schoolID", dto.SchoolID);
            cmd.Parameters.AddWithValue("@coachID", dto.CoachID);
            cmd.Parameters.AddWithValue("@FirstDay", dto.FirstDay);
            cmd.Parameters.AddWithValue("@SecondDay", dto.SecondDay);
            cmd.Parameters.AddWithValue("@StartTime", dto.StartTime);
            cmd.Parameters.AddWithValue("@EndTime", dto.EndTime);
            cmd.Parameters.AddWithValue("@Type", dto.Type);
            cmd.Parameters.AddWithValue("@site", dto.Site);
            cmd.Parameters.AddWithValue("@user", dto.User);

            await conn.OpenAsync();
            int rowsAffected = await cmd.ExecuteNonQueryAsync();

            return rowsAffected > 0;
        }


    }
}
