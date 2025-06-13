using SwimmingAcademy.DTOs;

namespace SwimmingAcademy.Services.Interfaces
{
    public interface ISwimmerService
    {
        Task<IEnumerable<SwimmerDto>> GetAllSwimmersAsync();
        Task<SwimmerDto?> GetSwimmerById(long id);
        Task<long> AddSwimmerAsync(AddSwimmerDto dto);
        Task<long> ChangeSiteAsync(ChangeSiteDto dto);
        Task DropSwimmerAsync(long swimmerId);
        Task<SwimmerInfoTabDto?> GetSwimmerInfoTabAsync(long swimmerId);
        Task<List<SwimmerLogTabDto>> GetSwimmerLogTabAsync(long swimmerId);
        Task<object?> GetSwimmerTechnicalTabAsync(long swimmerId);
        Task<List<ActionNameDto>> SearchActionsAsync(int userId, long swimmerId, short userSite);
        Task<List<ShowSwimmerDto>> ShowSwimmersAsync(long? swimmerId, string? fullName, string? year, short? level);
        Task UpdateSwimmerAsync(UpdateSwimmerDto dto);
        Task UpdateSwimmerLevelAsync(UpdateSwimmerLevelDto dto);
        Task<ViewPossibleSchoolResultDto> ViewPossibleSchoolAsync(long swimmerId, short type);

    }
}
