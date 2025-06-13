using SwimmingAcademy.DTOs;

namespace SwimmingAcademy.Services.Interfaces
{
    public interface IPreTeamService
    {
        Task<long> CreatePreTeamAsync(CreatePreTeamDto dto);

        Task<bool> EndPreTeamAsync(EndPreTeamDto dto); // Add this line
        Task<PreTeamDetailsDto?> GetPreTeamDetailsAsync(long preTeamId); // Add this
        Task<List<SearchActionResponseDto>> SearchActionsAsync(SearchActionRequestDto request); // New
        Task<List<ShowPreTeamResponseDto>> ShowPreTeamAsync(ShowPreTeamRequestDto request); // New
        Task<List<SwimmerDetailsTabDto>> GetSwimmerDetailsAsync(long pTeamId); // Add this
        Task<bool> UpdatePreTeamAsync(UpdatePreTeamDto dto); // ✅ NEW


    }
}
