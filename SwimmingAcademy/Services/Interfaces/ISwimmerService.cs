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

    }
}
