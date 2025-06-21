using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SwimmingAcademy.Data;
using SwimmingAcademy.DTOs;
using SwimmingAcademy.Interfaces;
using System.Data;

namespace SwimmingAcademy.Repositories
{
    public class SchoolRepository : ISchoolRepository
    {
        private readonly SwimmingAcademyContext _context;
        private readonly ILogger<SchoolRepository> _logger;

        public SchoolRepository(SwimmingAcademyContext context, ILogger<SchoolRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<long> CreateSchoolAsync(CreateSchoolRequest req)
        {
            try
            {
                using var conn = _context.Database.GetDbConnection();
                using var command = conn.CreateCommand();
                command.CommandText = "[Schools].[Create_School]";
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddRange(new[]
                {
                    new SqlParameter("@SchoolLevel", req.SchoolLevel),
                    new SqlParameter("@coachID", req.CoachID),
                    new SqlParameter("@FirstDay", req.FirstDay ?? (object)DBNull.Value),
                    new SqlParameter("@SecondDay", req.SecondDay ?? (object)DBNull.Value),
                    new SqlParameter("@StartTime", req.StartTime),
                    new SqlParameter("@EndTime", req.EndTime),
                    new SqlParameter("@Type", req.Type),
                    new SqlParameter("@site", req.Site),
                    new SqlParameter("@user", req.User)
                });

                if (conn.State != ConnectionState.Open)
                    await conn.OpenAsync();

                var result = await command.ExecuteScalarAsync();
                return result is null ? 0 : Convert.ToInt64(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in CreateSchoolAsync");
                throw new Exception("An error occurred while creating the school.");
            }
        }





        public async Task<bool> UpdateSchoolAsync(UpdateSchoolRequest req)
        {
            try
            {
                using var conn = _context.Database.GetDbConnection();
                using var command = conn.CreateCommand();
                command.CommandText = "[Schools].[Updated]";
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddRange(new[]
                {
            new SqlParameter("@schoolID", req.SchoolID),
            new SqlParameter("@coachID", req.CoachID),
            new SqlParameter("@FirstDay", req.FirstDay ?? (object)DBNull.Value),
            new SqlParameter("@SecondDay", req.SecondDay ?? (object)DBNull.Value),
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
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in UpdateSchoolAsync for SchoolID {SchoolID}", req.SchoolID);
                throw new Exception("An error occurred while updating the school.");
            }
        }

        public async Task<bool> EndSchoolAsync(EndSchoolRequest req)
        {
            try
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
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in EndSchoolAsync for SchoolID {SchoolID}", req.SchoolID);
                throw new Exception("An error occurred while ending the school.");
            }
        }

        public async Task<IEnumerable<SchoolSearchResultDto>> SearchSchoolsAsync(SchoolSearchRequest req)
        {
            var result = new List<SchoolSearchResultDto>();
            try
            {
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
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in SearchSchoolsAsync");
                throw new Exception("An error occurred while searching for schools.");
            }
        }
        public async Task<SchoolDetailsTabDto?> GetSchoolDetailsTabAsync(long schoolID)
        {
            try
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
                        NumberOfSwimmers = reader.IsDBNull(6) ? "0" : reader.GetString(6),
                        IsEnded = !reader.IsDBNull(7) && reader.GetBoolean(7)
                    };
                }
                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetSchoolDetailsTabAsync for SchoolID {SchoolID}", schoolID);
                throw new Exception("An error occurred while retrieving school details.");
            }
        }

        public async Task<IEnumerable<SchoolSwimmerDetailsDto>> GetSchoolSwimmerDetailsAsync(long schoolID)
        {
            var result = new List<SchoolSwimmerDetailsDto>();
            try
            {
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
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetSchoolSwimmerDetailsAsync for SchoolID {SchoolID}", schoolID);
                throw new Exception("An error occurred while retrieving school swimmer details.");
            }
        }
        public async Task<IEnumerable<ActionNameDto>> SearchSchoolActionsAsync(SchoolActionSearchRequest req)
        {
            var result = new List<ActionNameDto>();
            try
            {
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
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in SearchSchoolActionsAsync for UserID {UserID}, SchoolID {SchoolID}, UserSite {UserSite}", req.UserID, req.SchoolID, req.UserSite);
                throw new Exception("An error occurred while searching for school actions.");
            }
        }

        public async Task<ViewPossibleSchoolResponse> GetPossibleSchoolOptionsAsync(long swimmerID, short type)
        {
            var response = new ViewPossibleSchoolResponse
            {
                Schools = new List<PossibleSchoolDto>(),
                Invoices = new List<InvoiceSuggestionDto>()
            };
            try
            {
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
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetPossibleSchoolOptionsAsync");
                throw new Exception("An error occurred while retrieving possible school options.");
            }
        }
    }
}