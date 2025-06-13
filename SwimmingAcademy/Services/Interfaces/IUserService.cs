using SwimmingAcademy.DTOs;
using SwimmingAcademy.Models;

namespace SwimmingAcademy.Services.Interfaces
{
    public interface IUserService
    {
        Task<LoginResponseDto> LoginAsync(LoginDto request);

    }
}
