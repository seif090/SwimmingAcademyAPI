using SwimmingAcademy.DTOs;

namespace SwimmingAcademy.Interfaces
{
    public interface IPreTeamRepository
    {
        Task<long> CreatePreTeamAsync(CreatePTeamRequest request);
        Task<IEnumerable<PTeamSearchResultDto>> SearchPTeamAsync(PTeamSearchRequest request);
        Task<IEnumerable<SwimmerPTeamDetailsDto>> GetSwimmerPTeamDetailsAsync(long pTeamId);
        Task<bool> UpdatePTeamAsync(UpdatePTeamRequest request);
        Task<bool> EndPTeamAsync(EndPTeamRequest request);
        Task<PTeamDetailsTabDto?> GetPTeamDetailsTabAsync(long pTeamId);
        Task<IEnumerable<ActionNameDto>> SearchActionsAsync(PreTeamActionSearchRequest request);

    }
}
