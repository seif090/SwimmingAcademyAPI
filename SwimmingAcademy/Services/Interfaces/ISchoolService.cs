using SwimmingAcademy.DTOs;

namespace SwimmingAcademy.Services.Interfaces
{
    public interface ISchoolService
    {
        Task<long> CreateSchoolAsync(CreateSchoolDto dto);
        Task EndSchoolAsync(EndSchoolDto dto);
        Task<SchoolDetailsTabDto?> GetSchoolDetailsTabAsync(long schoolId);
        Task<List<ActionNameDto>> SearchActionsAsync(int userId, long schoolId, short userSite);
        Task<List<ShowSchoolDto>> ShowSchoolAsync(long? schoolId, string? fullName, short? level, short? type);
        Task<List<SwimmerDetailsTabDto>> GetSwimmerDetailsTabAsync(long schoolId);
        Task UpdateSchoolAsync(UpdateSchoolDto dto);

    }
}
