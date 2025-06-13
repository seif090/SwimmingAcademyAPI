using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SwimmingAcademy.Data;
using SwimmingAcademy.DTOs;
using SwimmingAcademy.Models;
using SwimmingAcademy.Repositories.Interfaces;
using SwimmingAcademy.Services.Interfaces;
using System.Data;

namespace SwimmingAcademy.Services
{
    public class SwimmerService : ISwimmerService
    {
        private readonly ISwimmerRepository _repository;
        private readonly IMapper _mapper;
        private readonly SwimmingAcademyContext _context;
        private readonly IConfiguration _configuration;

        public SwimmerService(ISwimmerRepository repository, IMapper mapper, SwimmingAcademyContext context, IConfiguration configuration)
        {
            _repository = repository;
            _mapper = mapper;
            _context = context;
            _configuration = configuration;
        }

        public async Task<IEnumerable<SwimmerDto>> GetAllSwimmersAsync()
        {
            var swimmers = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<SwimmerDto>>(swimmers);
        }

        public async Task<SwimmerDto?> GetSwimmerById(long id)
        {
            var swimmer = await _repository.GetByIdAsync(id);
            return swimmer == null ? null : _mapper.Map<SwimmerDto>(swimmer);
        }

        public async Task<AddSwimmerResponseDto> AddSwimmerAsync(AddSwimmerDto dto)
        {
            long swimmerId;

            using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            using var cmd = new SqlCommand("[Swimmers].[Add_Swimmer]", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.AddWithValue("@UserID", dto.UserID);
            cmd.Parameters.AddWithValue("@Site", dto.Site);
            cmd.Parameters.AddWithValue("@FullName", dto.FullName);
            cmd.Parameters.AddWithValue("@BirthDate", dto.BirthDate);
            cmd.Parameters.AddWithValue("@Start_Level", dto.StartLevel);
            cmd.Parameters.AddWithValue("@Gender", dto.Gender);
            cmd.Parameters.AddWithValue("@club", dto.Club);
            cmd.Parameters.AddWithValue("@primaryPhone", dto.PrimaryPhone);
            cmd.Parameters.AddWithValue("@SecondaryPhone", (object?)dto.SecondaryPhone ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@PrimaryJop", dto.PrimaryJop);
            cmd.Parameters.AddWithValue("@SecondaryJop", (object?)dto.SecondaryJop ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@Email", dto.Email);
            cmd.Parameters.AddWithValue("@Adress", dto.Adress);

            await conn.OpenAsync();
            swimmerId = Convert.ToInt64(await cmd.ExecuteScalarAsync());

            return new AddSwimmerResponseDto
            {
                SwimmerID = swimmerId
            };
        }

        public async Task<ChangeSiteResponseDto> ChangeSwimmerSiteAsync(ChangeSwimmerSiteDto dto)
        {
            long swimmerId;

            using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            using var cmd = new SqlCommand("[Swimmers].[Change_Site]", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.AddWithValue("@swimmerID", dto.SwimmerID);
            cmd.Parameters.AddWithValue("@userID", dto.UserID);

            await conn.OpenAsync();

            swimmerId = Convert.ToInt64(await cmd.ExecuteScalarAsync());

            return new ChangeSiteResponseDto
            {
                SwimmerID = swimmerId
            };
        }

        public async Task<bool> DropSwimmerAsync(long swimmerId)
        {
            using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            using var cmd = new SqlCommand("[Swimmers].[drop_Swimmer]", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.AddWithValue("@swimmerID", swimmerId);

            await conn.OpenAsync();
            int rowsAffected = await cmd.ExecuteNonQueryAsync();

            return rowsAffected > 0;
        }

        public async Task<SwimmerInfoTabDto?> GetSwimmerInfoAsync(long swimmerId)
        {
            using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            using var cmd = new SqlCommand("[Swimmers].[InfoTap]", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.AddWithValue("@SwimmerID", swimmerId);

            await conn.OpenAsync();
            using var reader = await cmd.ExecuteReaderAsync();

            if (await reader.ReadAsync())
            {
                return new SwimmerInfoTabDto
                {
                    FullName = reader["FullName"].ToString() ?? "",
                    BirthDate = Convert.ToDateTime(reader["BirthDate"]),
                    Site = reader["Site"].ToString() ?? "",
                    CurrentLevel = reader["CurrentLevel"].ToString() ?? "",
                    StartLevel = reader["StartLevel"].ToString() ?? "",
                    CreatedAtDate = Convert.ToDateTime(reader["createdAtDate"]),
                    PrimaryJop = reader["PrimaryJop"].ToString() ?? "",
                    SecondaryJop = reader["SecondaryJop"]?.ToString(),
                    PrimaryPhone = reader["PrimaryPhone"].ToString() ?? "",
                    SecondaryPhone = reader["SecondaryPhone"]?.ToString(),
                    Club = reader["Club"].ToString() ?? ""
                };
            }

            return null;
        }

        public async Task<List<SwimmerLogTabDto>> GetSwimmerLogAsync(long swimmerId)
        {
            var logs = new List<SwimmerLogTabDto>();

            using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            using var cmd = new SqlCommand("[Swimmers].[LogTap]", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.AddWithValue("@SwimmerID", swimmerId);

            await conn.OpenAsync();
            using var reader = await cmd.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                logs.Add(new SwimmerLogTabDto
                {
                    ActionName = reader["ActionName"].ToString() ?? "",
                    PerformedBy = reader["FullName"].ToString() ?? "",
                    CreatedAtDate = Convert.ToDateTime(reader["CreatedAtDate"]),
                    Site = reader["Site"].ToString() ?? ""
                });
            }

            return logs;
        }

        public async Task<TechnicalTabResultDto?> GetTechnicalTabAsync(long swimmerId)
        {
            var result = new TechnicalTabResultDto();

            using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            using var cmd = new SqlCommand("[Swimmers].[TechnicalTap]", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.AddWithValue("@swimmerID", swimmerId);

            await conn.OpenAsync();
            using var reader = await cmd.ExecuteReaderAsync();

            if (reader.HasRows)
            {
                await reader.ReadAsync();

                // Detect type based on available fields
                if (reader.FieldCount == 7)
                {
                    result.IsSchool = true;
                    result.SchoolData = new SwimmerTechnicalSchoolTabDto
                    {
                        CoachName = reader["CoachName"].ToString(),
                        FirstDay = reader["FirstDay"].ToString(),
                        SecondDay = reader["SecondDay"].ToString(),
                        StartTime = (TimeSpan)reader["StartTime"],
                        EndTime = (TimeSpan)reader["EndTime"],
                        SwimmerLevel = reader["swimmerLevel"].ToString(),
                        Attendence = reader["Attendence"].ToString()
                    };
                }
                else if (reader.FieldCount == 9)
                {
                    result.IsPreTeam = true;
                    result.PreTeamData = new SwimmerTechnicalPreTeamTabDto
                    {
                        CoachName = reader["CoachName"].ToString(),
                        FirstDay = reader["FirstDay"].ToString(),
                        SecondDay = reader["SecondDay"].ToString(),
                        ThirdDay = reader["ThirdDay"].ToString(),
                        StartTime = (TimeSpan)reader["StartTime"],
                        EndTime = (TimeSpan)reader["EndTime"],
                        SwimmerLevel = reader["swimmerLevel"].ToString(),
                        Attendence = reader["Attendence"].ToString(),
                        LastStar = reader["LastStar"].ToString()
                    };
                }
            }

            return result;
        }
        public async Task<List<SearchActionResponseDto>> SearchSwimmerActionsAsync(SearchSwimmerActionRequestDto request)
        {
            var result = new List<SearchActionResponseDto>();

            using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            using var cmd = new SqlCommand("[Swimmers].[SearchActions]", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.AddWithValue("@UserID", request.UserID);
            cmd.Parameters.AddWithValue("@SwimmerID", request.SwimmerID);
            cmd.Parameters.AddWithValue("@userSite", request.UserSite);

            await conn.OpenAsync();
            using var reader = await cmd.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                result.Add(new SearchActionResponseDto
                {
                    ActionName = reader["ActionName"].ToString() ?? ""
                });
            }

            return result;
        }

        public async Task<List<ShowSwimmerResponseDto>> ShowSwimmersAsync(ShowSwimmerRequestDto request)
        {
            var result = new List<ShowSwimmerResponseDto>();

            using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            using var cmd = new SqlCommand("[Swimmers].[ShowSwimmers]", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.AddWithValue("@swimmerID", request.SwimmerID ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@FullName", request.FullName ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@year", request.Year ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@level", request.Level ?? (object)DBNull.Value);

            await conn.OpenAsync();
            using var reader = await cmd.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                result.Add(new ShowSwimmerResponseDto
                {
                    FullName = reader["FulllName"]?.ToString() ?? "", // matches DB column
                    Year = reader["Year"]?.ToString() ?? "",
                    CurrentLevel = reader["Current_Level"]?.ToString() ?? "",
                    CoachName = reader["CoachName"]?.ToString() ?? "",
                    Site = reader["Site"]?.ToString() ?? "",
                    Club = reader["Club"]?.ToString() ?? ""
                });
            }

            return result;
        }


        public async Task<UpdateSwimmerResponseDto> UpdateSwimmerAsync(UpdateSwimmerDto dto)
        {
            using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            using var cmd = new SqlCommand("[Swimmers].[Update_Swimmer]", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.AddWithValue("@swimmerID", dto.SwimmerID);
            cmd.Parameters.AddWithValue("@UserID", dto.UserID);
            cmd.Parameters.AddWithValue("@Site", dto.Site);
            cmd.Parameters.AddWithValue("@FullName", dto.FullName);
            cmd.Parameters.AddWithValue("@BirthDate", dto.BirthDate);
            cmd.Parameters.AddWithValue("@Gender", dto.Gender);
            cmd.Parameters.AddWithValue("@club", dto.Club);
            cmd.Parameters.AddWithValue("@primaryPhone", dto.PrimaryPhone);
            cmd.Parameters.AddWithValue("@SecondaryPhone", (object?)dto.SecondaryPhone ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@PrimaryJop", dto.PrimaryJop);
            cmd.Parameters.AddWithValue("@SecondaryJop", (object?)dto.SecondaryJop ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@Email", dto.Email);
            cmd.Parameters.AddWithValue("@Adress", dto.Adress);

            await conn.OpenAsync();
            await cmd.ExecuteNonQueryAsync();

            return new UpdateSwimmerResponseDto
            {
                SwimmerID = dto.SwimmerID
            };
        }

        public async Task<UpdateSwimmerLevelResponseDto> UpdateSwimmerLevelAsync(UpdateSwimmerLevelDto dto)
        {
            using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            using var cmd = new SqlCommand("[Swimmers].[UpdateSwimmerLevel]", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.AddWithValue("@swimmerID", dto.SwimmerID);
            cmd.Parameters.AddWithValue("@NewLevel", dto.NewLevel);
            cmd.Parameters.AddWithValue("@userID", dto.UserID);
            cmd.Parameters.AddWithValue("@site", dto.Site);

            await conn.OpenAsync();
            await cmd.ExecuteNonQueryAsync();

            return new UpdateSwimmerLevelResponseDto
            {
                SwimmerID = dto.SwimmerID,
                NewLevel = dto.NewLevel
            };
        }

        public async Task<ViewPossibleSchoolResponseDto> ViewPossibleSchoolsAsync(ViewPossibleSchoolRequestDto dto)
        {
            var result = new ViewPossibleSchoolResponseDto();

            using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            using var cmd = new SqlCommand("[Swimmers].[ViewPossible_School]", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.AddWithValue("@swimmerID", dto.SwimmerID);
            cmd.Parameters.AddWithValue("@Type", dto.Type);

            await conn.OpenAsync();
            using var reader = await cmd.ExecuteReaderAsync();

            // Read School options
            while (await reader.ReadAsync())
            {
                result.Schools.Add(new SchoolOptionsDto
                {
                    CoachName = reader["CoachName"].ToString() ?? "",
                    Dayes = reader["Dayes"].ToString() ?? "",
                    FromTo = reader["FromTo"].ToString() ?? ""
                });
            }

            // Move to second result set (Invoices)
            if (await reader.NextResultAsync())
            {
                while (await reader.ReadAsync())
                {
                    result.Invoices.Add(new InvoiceItemDto
                    {
                        ItemName = reader["ItemName"].ToString() ?? "",
                        DurationInMonths = Convert.ToInt16(reader["DurationInMonths"]),
                        Amount = Convert.ToDecimal(reader["Amount"])
                    });
                }
            }

            return result;
        }

    }
}
