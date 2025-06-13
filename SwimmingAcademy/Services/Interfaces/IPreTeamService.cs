using SwimmingAcademy.DTOs;

namespace SwimmingAcademy.Services.Interfaces
{
    public interface IPreTeamService
    {
        Task<long> CreatePTeamAsync(
            short pTeamLevel,
            int coachId,
            string firstDay,
            string secondDay,
            string thirdDay,
            short site,
            int user,
            TimeSpan startTime,
            TimeSpan endTime);
        Task EndPreTeamAsync(long pteamId, int userId, short site);
        Task<PreTeamDetailsDto?> GetPTeamDetailsAsync(long pteamId);
        Task<List<ActionNameDto>> SearchActionsAsync(int userId, long pteamId, short userSite);
        Task<List<ShowPreTeamDto>> ShowPreTeamAsync(long? pteamId, string? fullName, short? level);
        Task<List<SwimmerDetailsTabDto>> GetSwimmerDetailsTabAsync(long pteamId);
        Task UpdatePTeamAsync(UpdatePreTeamDto dto);


    }
}
