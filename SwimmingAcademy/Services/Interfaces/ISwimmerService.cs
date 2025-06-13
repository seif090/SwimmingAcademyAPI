using SwimmingAcademy.DTOs;

namespace SwimmingAcademy.Services.Interfaces
{
    public interface ISwimmerService
    {
        Task<IEnumerable<SwimmerDto>> GetAllSwimmersAsync();
        Task<SwimmerDto?> GetSwimmerById(long id);
        Task<AddSwimmerResponseDto> AddSwimmerAsync(AddSwimmerDto dto);
        Task<ChangeSiteResponseDto> ChangeSwimmerSiteAsync(ChangeSwimmerSiteDto dto); // ✅ New
        Task<bool> DropSwimmerAsync(long swimmerId); // ✅ New
        Task<SwimmerInfoTabDto?> GetSwimmerInfoAsync(long swimmerId); // ✅ NEW
        Task<List<SwimmerLogTabDto>> GetSwimmerLogAsync(long swimmerId); // ✅ NEW
        Task<TechnicalTabResultDto?> GetTechnicalTabAsync(long swimmerId);
        Task<List<SearchActionResponseDto>> SearchSwimmerActionsAsync(SearchSwimmerActionRequestDto request); // ✅ NEW
        Task<List<ShowSwimmerResponseDto>> ShowSwimmersAsync(ShowSwimmerRequestDto request); // ✅ NEW
        Task<UpdateSwimmerResponseDto> UpdateSwimmerAsync(UpdateSwimmerDto dto); // ✅ NEW
        Task<UpdateSwimmerLevelResponseDto> UpdateSwimmerLevelAsync(UpdateSwimmerLevelDto dto);
        Task<ViewPossibleSchoolResponseDto> ViewPossibleSchoolsAsync(ViewPossibleSchoolRequestDto dto);

    }
}
