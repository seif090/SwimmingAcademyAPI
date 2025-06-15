using SwimmingAcademy.DTOs;

namespace SwimmingAcademy.Interfaces
{
    public interface IAuthRepository
    {
        Task<LoginResultDTO> LoginAsync(int userId, string password);

    }
}
