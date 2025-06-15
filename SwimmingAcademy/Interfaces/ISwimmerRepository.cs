using SwimmingAcademy.DTOs;
namespace SwimmingAcademy.Interfaces
{
    public interface ISwimmerRepository
    {
        Task<long> AddSwimmerAsync(AddSwimmerRequestDTO request);
        Task<SwimmerInfoTabDto?> GetSwimmerInfoTabAsync(long swimmerID);
        Task<IEnumerable<SwimmerLogDto>> GetSwimmerLogsAsync(long swimmerID);
        Task<long> ChangeSwimmerSiteAsync(ChangeSwimmerSiteRequest request);
        Task<bool> DeleteSwimmerAsync(long swimmerID);
        Task<InvoiceResultDto?> SavePreTeamTransactionAsync(SavePteamTransRequest request);
        Task<InvoiceResultDto?> SaveSchoolTransactionAsync(SaveSchoolTransRequest request);
        Task<IEnumerable<ActionNameDto>> SearchSwimmerActionsAsync(SwimmerActionSearchRequest request);
        Task<IEnumerable<SwimmerSearchResultDto>> SearchSwimmersAsync(SwimmerSearchRequest request);
        Task<object?> GetTechnicalTapAsync(long swimmerID); // generic return
        Task<bool> UpdateSwimmerAsync(UpdateSwimmerRequest request);
        Task<bool> UpdateSwimmerLevelAsync(UpdateSwimmerLevelRequest request);
        Task<ViewPossiblePreTeamResponse> GetPossiblePreTeamOptionsAsync(long swimmerID);


    }
}
