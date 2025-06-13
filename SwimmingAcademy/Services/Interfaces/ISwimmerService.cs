using SwimmingAcademy.DTOs;

namespace SwimmingAcademy.Services.Interfaces
{
    public interface ISwimmerService
    {
        Task<IEnumerable<SwimmerDto>> GetAllSwimmersAsync();
        Task<SwimmerDto?> GetSwimmerById(long id);
    }
}
