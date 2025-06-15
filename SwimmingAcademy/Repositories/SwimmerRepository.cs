using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using SwimmingAcademy.Data;
using SwimmingAcademy.DTOs;
using SwimmingAcademy.Interfaces;
using System.Data;

namespace SwimmingAcademy.Repositories
{
    public class SwimmerRepository : ISwimmerRepository
    {
        private readonly SwimmingAcademyContext _context;

        public SwimmerRepository(SwimmingAcademyContext context)
        {
            _context = context;
        }

        public async Task<long> AddSwimmerAsync(AddSwimmerRequestDTO req)
        {
            using var conn = _context.Database.GetDbConnection();
            using var command = conn.CreateCommand();

            command.CommandText = "[Swimmers].[Add_Swimmer]";
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddRange(new[]
            {
        new SqlParameter("@UserID", req.UserID),
        new SqlParameter("@Site", req.Site),
        new SqlParameter("@FullName", req.FullName),
        new SqlParameter("@BirthDate", req.BirthDate),
        new SqlParameter("@Start_Level", req.StartLevel),
        new SqlParameter("@Gender", req.Gender),
        new SqlParameter("@club", req.Club),
        new SqlParameter("@primaryPhone", req.PrimaryPhone),
        new SqlParameter("@SecondaryPhone", (object?)req.SecondaryPhone ?? DBNull.Value),
        new SqlParameter("@PrimaryJop", req.PrimaryJop),
        new SqlParameter("@SecondaryJop", (object?)req.SecondaryJop ?? DBNull.Value),
        new SqlParameter("@Email", req.Email),
        new SqlParameter("@Adress", req.Adress)
    });

            if (conn.State != ConnectionState.Open)
                await conn.OpenAsync();

            using var reader = await command.ExecuteReaderAsync();
            if (await reader.ReadAsync())
                return reader.GetInt64(0);

            return 0;
        }

        public async Task<SwimmerInfoTabDto?> GetSwimmerInfoTabAsync(long swimmerID)
        {
            using var conn = _context.Database.GetDbConnection();
            using var command = conn.CreateCommand();
            command.CommandText = "[Swimmers].[InfoTap]";
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@SwimmerID", swimmerID));

            if (conn.State != ConnectionState.Open)
                await conn.OpenAsync();

            using var reader = await command.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                return new SwimmerInfoTabDto
                {
                    FullName = reader.IsDBNull(0) ? "" : reader.GetString(0),
                    BirthDate = reader.IsDBNull(1) ? default : reader.GetDateTime(1),
                    Site = reader.IsDBNull(2) ? "" : reader.GetString(2),
                    CurrentLevel = reader.IsDBNull(3) ? "" : reader.GetString(3),
                    StartLevel = reader.IsDBNull(4) ? "" : reader.GetString(4),
                    CreatedAtDate = reader.IsDBNull(5) ? default : reader.GetDateTime(5),
                    PrimaryJop = reader.IsDBNull(6) ? "" : reader.GetString(6),
                    SecondaryJop = reader.IsDBNull(7) ? "" : reader.GetString(7),
                    PrimaryPhone = reader.IsDBNull(8) ? "" : reader.GetString(8),
                    SecondaryPhone = reader.IsDBNull(9) ? "" : reader.GetString(9),
                    Club = reader.IsDBNull(10) ? "" : reader.GetString(10)
                };
            }

            return null;
        }

        public async Task<IEnumerable<SwimmerLogDto>> GetSwimmerLogsAsync(long swimmerID)
        {
            var logs = new List<SwimmerLogDto>();

            using var conn = _context.Database.GetDbConnection();
            using var command = conn.CreateCommand();

            command.CommandText = "[Swimmers].[LogTap]";
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@SwimmerID", swimmerID));

            if (conn.State != ConnectionState.Open)
                await conn.OpenAsync();

            using var reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                logs.Add(new SwimmerLogDto
                {
                    ActionName = reader.IsDBNull(0) ? "" : reader.GetString(0),
                    PerformedBy = reader.IsDBNull(1) ? "" : reader.GetString(1),
                    CreatedAtDate = reader.IsDBNull(2) ? default : reader.GetDateTime(2),
                    Site = reader.IsDBNull(3) ? "" : reader.GetString(3)
                });
            }

            return logs;
        }

        public async Task<long> ChangeSwimmerSiteAsync(ChangeSwimmerSiteRequest req)
        {
            using var conn = _context.Database.GetDbConnection();
            using var command = conn.CreateCommand();

            command.CommandText = "[Swimmers].[Change_Site]";
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddRange(new[]
            {
        new SqlParameter("@swimmerID", req.SwimmerID),
        new SqlParameter("@userID", req.UserID),
        new SqlParameter("@Site", req.Site)
    });

            if (conn.State != ConnectionState.Open)
                await conn.OpenAsync();

            using var reader = await command.ExecuteReaderAsync();
            if (await reader.ReadAsync())
                return reader.GetInt64(0); // return swimmer ID

            return 0;
        }

        public async Task<bool> DeleteSwimmerAsync(long swimmerID)
        {
            using var conn = _context.Database.GetDbConnection();
            using var command = conn.CreateCommand();

            command.CommandText = "[Swimmers].[drop_Swimmer]";
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@swimmerID", swimmerID));

            if (conn.State != ConnectionState.Open)
                await conn.OpenAsync();

            await command.ExecuteNonQueryAsync();
            return true;
        }

        public async Task<InvoiceResultDto?> SavePreTeamTransactionAsync(SavePteamTransRequest req)
        {
            using var conn = _context.Database.GetDbConnection();
            using var command = conn.CreateCommand();

            command.CommandText = "[Swimmers].[SavePteam_Trans]";
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddRange(new[]
            {
        new SqlParameter("@swimmerID", req.SwimmerID),
        new SqlParameter("@PTeamID", req.PTeamID),
        new SqlParameter("@DuarationsInMonths", req.DuarationsInMonths),
        new SqlParameter("@user", req.User),
        new SqlParameter("@site", req.Site)
    });

            if (conn.State != ConnectionState.Open)
                await conn.OpenAsync();

            using var reader = await command.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                return new InvoiceResultDto
                {
                    InvItem = reader.IsDBNull(0) ? "" : reader.GetValue(0).ToString(),
                    Value = reader.IsDBNull(1) ? 0 : Convert.ToDecimal(reader.GetValue(1))
                };
            }

            return null;
        }

        public async Task<InvoiceResultDto?> SaveSchoolTransactionAsync(SaveSchoolTransRequest req)
        {
            using var conn = _context.Database.GetDbConnection();
            using var command = conn.CreateCommand();

            command.CommandText = "[Swimmers].[SaveSchool_Trans]";
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddRange(new[]
            {
        new SqlParameter("@swimmerID", req.SwimmerID),
        new SqlParameter("@SchoolID", req.SchoolID),
        new SqlParameter("@DuarationsInMonths", req.DuarationsInMonths),
        new SqlParameter("@user", req.User),
        new SqlParameter("@site", req.Site)
    });

            if (conn.State != ConnectionState.Open)
                await conn.OpenAsync();

            using var reader = await command.ExecuteReaderAsync();
            InvoiceResultDto? result = null;

            // First result set: invoice item
            if (await reader.ReadAsync())
            {
                result = new InvoiceResultDto
                {
                    InvItem = reader.IsDBNull(0) ? "" : reader.GetValue(0).ToString(),
                    Value = reader.IsDBNull(1) ? 0 : Convert.ToDecimal(reader.GetValue(1))
                };
            }

            // Skip second result set (Trans) — unless needed
            return result;
        }
        public async Task<IEnumerable<ActionNameDto>> SearchSwimmerActionsAsync(SwimmerActionSearchRequest req)
        {
            var actions = new List<ActionNameDto>();

            using var conn = _context.Database.GetDbConnection();
            using var command = conn.CreateCommand();

            command.CommandText = "[Swimmers].[SearchActions]";
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new SqlParameter("@UserID", req.UserID));
            command.Parameters.Add(new SqlParameter("@SwimmerID", req.SwimmerID));
            command.Parameters.Add(new SqlParameter("@userSite", req.UserSite));

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
        public async Task<IEnumerable<SwimmerSearchResultDto>> SearchSwimmersAsync(SwimmerSearchRequest req)
        {
            var result = new List<SwimmerSearchResultDto>();

            using var conn = _context.Database.GetDbConnection();
            using var command = conn.CreateCommand();
            command.CommandText = "[Swimmers].[ShowSwimmers]";
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new SqlParameter("@swimmerID", (object?)req.SwimmerID ?? DBNull.Value));
            command.Parameters.Add(new SqlParameter("@FullName", (object?)req.FullName ?? DBNull.Value));
            command.Parameters.Add(new SqlParameter("@year", (object?)req.Year ?? DBNull.Value));
            command.Parameters.Add(new SqlParameter("@level", (object?)req.Level ?? DBNull.Value));

            if (conn.State != ConnectionState.Open)
                await conn.OpenAsync();

            using var reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                result.Add(new SwimmerSearchResultDto
                {
                    FullName = reader.IsDBNull(0) ? "" : reader.GetString(0),
                    Year = reader.IsDBNull(1) ? "" : reader.GetValue(1).ToString(),
                    CurrentLevel = reader.IsDBNull(2) ? "" : reader.GetString(2),
                    CoachName = reader.IsDBNull(3) ? "" : reader.GetString(3),
                    Site = reader.IsDBNull(4) ? "" : reader.GetString(4),
                    Club = reader.IsDBNull(5) ? "" : reader.GetString(5)
                });
            }

            return result;
        }
        public async Task<object?> GetTechnicalTapAsync(long swimmerID)
        {
            using var conn = _context.Database.GetDbConnection();
            using var command = conn.CreateCommand();

            command.CommandText = "[Swimmers].[TechnicalTap]";
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@swimmerID", swimmerID));

            if (conn.State != ConnectionState.Open)
                await conn.OpenAsync();

            using var reader = await command.ExecuteReaderAsync();

            if (await reader.ReadAsync())
            {
                if (reader.FieldCount == 7) // School shape
                {
                    return new SwimmerTechnicalSchoolDto
                    {
                        CoachName = reader.IsDBNull(0) ? "" : reader.GetString(0),
                        FirstDay = reader.IsDBNull(1) ? "" : reader.GetString(1),
                        SecondDay = reader.IsDBNull(2) ? "" : reader.GetString(2),
                        StartTime = reader.IsDBNull(3) ? TimeSpan.Zero : Convert.ToDateTime(reader.GetValue(3)).TimeOfDay,
                        EndTime = reader.IsDBNull(4) ? TimeSpan.Zero : Convert.ToDateTime(reader.GetValue(4)).TimeOfDay,
                        SwimmerLevel = reader.IsDBNull(5) ? "" : reader.GetString(5),
                        Attendence = reader.IsDBNull(6) ? "" : reader.GetString(6)
                    };
                }
                else if (reader.FieldCount == 9) // PreTeam shape
                {
                    return new SwimmerTechnicalPreTeamDto
                    {
                        CoachName = reader.IsDBNull(0) ? "" : reader.GetString(0),
                        FirstDay = reader.IsDBNull(1) ? "" : reader.GetString(1),
                        SecondDay = reader.IsDBNull(2) ? "" : reader.GetString(2),
                        ThirdDay = reader.IsDBNull(3) ? "" : reader.GetString(3),
                        StartTime = reader.IsDBNull(4) ? TimeSpan.Zero : Convert.ToDateTime(reader.GetValue(4)).TimeOfDay,
                        EndTime = reader.IsDBNull(5) ? TimeSpan.Zero : Convert.ToDateTime(reader.GetValue(5)).TimeOfDay,
                        SwimmerLevel = reader.IsDBNull(6) ? "" : reader.GetString(6),
                        Attendence = reader.IsDBNull(7) ? "" : reader.GetString(7),
                        LastStar = reader.IsDBNull(8) ? "" : reader.GetString(8)
                    };
                }
            }

            return null;
        }
        public async Task<bool> UpdateSwimmerAsync(UpdateSwimmerRequest req)
        {
            using var conn = _context.Database.GetDbConnection();
            using var command = conn.CreateCommand();

            command.CommandText = "[Swimmers].[Update_Swimmer]";
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddRange(new[]
            {
        new SqlParameter("@swimmerID", req.SwimmerID),
        new SqlParameter("@UserID", req.UserID),
        new SqlParameter("@Site", req.Site),
        new SqlParameter("@FullName", req.FullName),
        new SqlParameter("@BirthDate", req.BirthDate),
        new SqlParameter("@Gender", req.Gender),
        new SqlParameter("@club", req.Club),
        new SqlParameter("@primaryPhone", req.PrimaryPhone),
        new SqlParameter("@SecondaryPhone", (object?)req.SecondaryPhone ?? DBNull.Value),
        new SqlParameter("@PrimaryJop", req.PrimaryJop),
        new SqlParameter("@SecondaryJop", (object?)req.SecondaryJop ?? DBNull.Value),
        new SqlParameter("@Email", req.Email),
        new SqlParameter("@Adress", req.Adress)
    });

            if (conn.State != ConnectionState.Open)
                await conn.OpenAsync();

            await command.ExecuteNonQueryAsync();
            return true;
        }
        public async Task<bool> UpdateSwimmerLevelAsync(UpdateSwimmerLevelRequest req)
        {
            using var conn = _context.Database.GetDbConnection();
            using var command = conn.CreateCommand();

            command.CommandText = "[Swimmers].[UpdateSwimmerLevel]";
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddRange(new[]
            {
        new SqlParameter("@swimmerID", req.SwimmerID),
        new SqlParameter("@NewLevel", req.NewLevel),
        new SqlParameter("@userID", req.UserID),
        new SqlParameter("@site", req.Site)
    });

            if (conn.State != ConnectionState.Open)
                await conn.OpenAsync();

            await command.ExecuteNonQueryAsync();
            return true;
        }
        public async Task<ViewPossiblePreTeamResponse> GetPossiblePreTeamOptionsAsync(long swimmerID)
        {
            var response = new ViewPossiblePreTeamResponse
            {
                PreTeams = new List<PossiblePreTeamDto>(),
                Invoices = new List<InvoiceSuggestionDto>()
            };

            using var conn = _context.Database.GetDbConnection();
            using var command = conn.CreateCommand();

            command.CommandText = "[Swimmers].[ViewPossible_Pteam]";
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@swimmerID", swimmerID));

            if (conn.State != ConnectionState.Open)
                await conn.OpenAsync();

            using var reader = await command.ExecuteReaderAsync();

            // First result set: PreTeams
            while (await reader.ReadAsync())
            {
                response.PreTeams.Add(new PossiblePreTeamDto
                {
                    CoachName = reader.IsDBNull(0) ? "" : reader.GetString(0),
                    Dayes = reader.IsDBNull(1) ? "" : reader.GetString(1),
                    FromTo = reader.IsDBNull(2) ? "" : reader.GetString(2)
                });
            }

            // Move to next result set: Invoices
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
