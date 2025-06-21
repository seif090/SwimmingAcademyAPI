using SwimmingAcademy.DTOs;

namespace SwimmingAcademy.Interfaces
{
    public interface ICoachRepository
    {
        Task<IEnumerable<FreeCoachDto>> GetFreeCoachesAsync(FreeCoachFilterRequest request);
    }
}