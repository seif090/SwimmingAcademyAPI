using SwimmingAcademy.DTOs;

namespace SwimmingAcademy.Services.Interfaces
{
    public interface ICoachService
    {
        Task<List<FreeCoachDto>> GetFreeCoachesAsync(FreeCoachRequestDto request);
    }
}
