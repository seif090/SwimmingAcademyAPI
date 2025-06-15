using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using SwimmingAcademy.Data;
using SwimmingAcademy.DTOs;
using SwimmingAcademy.Interfaces;
using System.Data;

namespace SwimmingAcademy.Repositories
{
    public class SchoolRepository : ISchoolRepository
    {
        private readonly SwimmingAcademyContext _context;

        public SchoolRepository(SwimmingAcademyContext context)
        {
            _context = context;
        }

        public async Task<long> CreateSchoolAsync(CreateSchoolRequest req)
        {
            using var conn = _context.Database.GetDbConnection();
            using var command = conn.CreateCommand();
            command.CommandText = "[Schools].[Create_School]";
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddRange(new[]
            {
            new SqlParameter("@SchoolLevel", req.SchoolLevel),
            new SqlParameter("@coachID", req.CoachID),
            new SqlParameter("@FirstDay", req.FirstDay),
            new SqlParameter("@SecondDay", req.SecondDay),
            new SqlParameter("@StartTime", req.StartTime),
            new SqlParameter("@EndTime", req.EndTime),
            new SqlParameter("@Type", req.Type),
            new SqlParameter("@site", req.Site),
            new SqlParameter("@user", req.User)
        });

            if (conn.State != ConnectionState.Open)
                await conn.OpenAsync();

            // Use ExecuteReader to get SCOPE_IDENTITY
            using var reader = await command.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                return reader.GetInt64(0); // if SP is modified to return it
            }

            return 0; // if not returned, just indicate success
        }

        public async Task<IEnumerable<SchoolSearchResultDto>> SearchSchoolsAsync(SchoolSearchRequest req)
        {
            var result = new List<SchoolSearchResultDto>();

            using var conn = _context.Database.GetDbConnection();
            using var command = conn.CreateCommand();
            command.CommandText = "[Schools].[ShowSchool]";
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new SqlParameter("@SchoolID", (object?)req.SchoolID ?? DBNull.Value));
            command.Parameters.Add(new SqlParameter("@FullName", (object?)req.FullName ?? DBNull.Value));
            command.Parameters.Add(new SqlParameter("@level", (object?)req.Level ?? DBNull.Value));
            command.Parameters.Add(new SqlParameter("@type", (object?)req.Type ?? DBNull.Value));

            if (conn.State != ConnectionState.Open)
                await conn.OpenAsync();

            using var reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                result.Add(new SchoolSearchResultDto
                {
                    CoachName = reader.IsDBNull(0) ? "" : reader.GetString(0),
                    Level = reader.IsDBNull(1) ? "" : reader.GetString(1),
                    Type = reader.IsDBNull(2) ? "" : reader.GetString(2),
                    Days = reader.IsDBNull(3) ? "" : reader.GetString(3),
                    FromTo = reader.IsDBNull(4) ? "" : reader.GetString(4),
                    NumberCapacity = reader.IsDBNull(5) ? "" : reader.GetString(5)
                });
            }

            return result;
        }

        public async Task<bool> UpdateSchoolAsync(UpdateSchoolRequest req)
        {
            using var conn = _context.Database.GetDbConnection();
            using var command = conn.CreateCommand();

            command.CommandText = "[Schools].[Updated]";
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddRange(new[]
            {
        new SqlParameter("@schoolID", req.SchoolID),
        new SqlParameter("@coachID", req.CoachID),
        new SqlParameter("@FirstDay", req.FirstDay),
        new SqlParameter("@SecondDay", req.SecondDay),
        new SqlParameter("@StartTime", req.StartTime),
        new SqlParameter("@EndTime", req.EndTime),
        new SqlParameter("@Type", req.Type),
        new SqlParameter("@site", req.Site),
        new SqlParameter("@user", req.User)
    });

            if (conn.State != ConnectionState.Open)
                await conn.OpenAsync();

            await command.ExecuteNonQueryAsync();
            return true;
        }

        public async Task<bool> EndSchoolAsync(EndSchoolRequest req)
        {
            using var conn = _context.Database.GetDbConnection();
            using var command = conn.CreateCommand();

            command.CommandText = "[Schools].[EndSchool]";
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddRange(new[]
            {
        new SqlParameter("@schoolID", req.SchoolID),
        new SqlParameter("@userID", req.UserID),
        new SqlParameter("@site", req.Site)
    });

            if (conn.State != ConnectionState.Open)
                await conn.OpenAsync();

            await command.ExecuteNonQueryAsync();
            return true;
        }
        public async Task<SchoolDetailsTabDto?> GetSchoolDetailsTabAsync(long schoolID)
        {
            using var conn = _context.Database.GetDbConnection();
            using var command = conn.CreateCommand();

            command.CommandText = "[Schools].[SchoolDetalis_Tab]";
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@schoolID", schoolID));

            if (conn.State != ConnectionState.Open)
                await conn.OpenAsync();

            using var reader = await command.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                return new SchoolDetailsTabDto
                {
                    FullName = reader.IsDBNull(0) ? "" : reader.GetString(0),
                    FirstDay = reader.IsDBNull(1) ? "" : reader.GetString(1),
                    SecondDay = reader.IsDBNull(2) ? "" : reader.GetString(2),
                    StartTime = reader.IsDBNull(3) ? "" : TimeSpan.TryParse(reader.GetValue(3)?.ToString(), out var st) ? st.ToString(@"hh\:mm") : "",
                    EndTime = reader.IsDBNull(4) ? "" : TimeSpan.TryParse(reader.GetValue(4)?.ToString(), out var et) ? et.ToString(@"hh\:mm") : "",
                    Capacity = reader.IsDBNull(5) ? 0 : reader.GetInt32(5),
                    NumberOfSwimmers = reader.IsDBNull(6) ? 0 : reader.GetInt32(6),
                    IsEnded = !reader.IsDBNull(7) && reader.GetBoolean(7)
                };
            }

            return null;
        }

        public async Task<IEnumerable<SchoolSwimmerDetailsDto>> GetSchoolSwimmerDetailsAsync(long schoolID)
        {
            var result = new List<SchoolSwimmerDetailsDto>();

            using var conn = _context.Database.GetDbConnection();
            using var command = conn.CreateCommand();
            command.CommandText = "[Schools].[SwimmerDetails_Tab]";
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@schoolID", schoolID));

            if (conn.State != ConnectionState.Open)
                await conn.OpenAsync();

            using var reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                result.Add(new SchoolSwimmerDetailsDto
                {
                    FullName = reader.IsDBNull(0) ? "" : reader.GetString(0),
                    Attendence = reader.IsDBNull(1) ? "" : reader.GetString(1),
                    SwimmerLevel = reader.IsDBNull(2) ? "" : reader.GetString(2)
                });
            }

            return result;
        }

        public async Task<IEnumerable<ActionNameDto>> SearchSchoolActionsAsync(SchoolActionSearchRequest req)
        {
            var result = new List<ActionNameDto>();

            using var conn = _context.Database.GetDbConnection();
            using var command = conn.CreateCommand();

            command.CommandText = "[Schools].[SearchActions]";
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new SqlParameter("@UserID", req.UserID));
            command.Parameters.Add(new SqlParameter("@SchoolID", req.SchoolID));
            command.Parameters.Add(new SqlParameter("@userSite", req.UserSite));

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

            return result;
        }
        public async Task<ViewPossibleSchoolResponse> GetPossibleSchoolOptionsAsync(long swimmerID, short type)
        {
            var response = new ViewPossibleSchoolResponse
            {
                Schools = new List<PossibleSchoolDto>(),
                Invoices = new List<InvoiceSuggestionDto>()
            };

            using var conn = _context.Database.GetDbConnection();
            using var command = conn.CreateCommand();

            command.CommandText = "[Swimmers].[ViewPossible_School]";
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@swimmerID", swimmerID));
            command.Parameters.Add(new SqlParameter("@Type", type));

            if (conn.State != ConnectionState.Open)
                await conn.OpenAsync();

            using var reader = await command.ExecuteReaderAsync();

            // 1. School options
            while (await reader.ReadAsync())
            {
                response.Schools.Add(new PossibleSchoolDto
                {
                    CoachName = reader.IsDBNull(0) ? "" : reader.GetString(0),
                    Dayes = reader.IsDBNull(1) ? "" : reader.GetString(1),
                    FromTo = reader.IsDBNull(2) ? "" : reader.GetString(2)
                });
            }

            // 2. Invoice options
            if (await reader.NextResultAsync())
            {
                while (await reader.ReadAsync())
                {
                    response.Invoices.Add(new InvoiceSuggestionDto
                    {
                        ItemName = reader.IsDBNull(0) ? "" : reader.GetString(0),
                        DurationInMonths = reader.IsDBNull(1) ? (short)0 : reader.GetInt16(1),
                        Amount = reader.IsDBNull(2) ? 0 : reader.GetDecimal(2)
                    });
                }
            }

            return response;
        }


    }
}
