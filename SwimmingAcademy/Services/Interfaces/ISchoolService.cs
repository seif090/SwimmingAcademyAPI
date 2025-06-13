using SwimmingAcademy.DTOs;

namespace SwimmingAcademy.Services.Interfaces
{
    public interface ISchoolService
    {
        Task<long> CreateSchoolAsync(CreateSchoolDto dto);
        Task<bool> EndSchoolAsync(EndSchoolDto dto);
        Task<SchoolDetailsTabDto?> GetSchoolDetailsAsync(long schoolId); // ✅ New
        Task<List<SearchActionResponseDto>> SearchSchoolActionsAsync(SearchSchoolActionRequestDto request); // ✅ New
        Task<List<ShowSchoolResponseDto>> ShowSchoolAsync(ShowSchoolRequestDto request); // ✅ NEW
        Task<List<SwimmerDetailsTabDto>> GetSchoolSwimmerDetailsAsync(long schoolId); // ✅ NEW
        Task<bool> UpdateSchoolAsync(UpdateSchoolDto dto); // ✅ NEW

    }
}
