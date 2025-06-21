using System.Threading.Tasks;
using SwimmingAcademy.DTOs;

namespace SwimmingAcademy.Interfaces
{
    public interface IAuthRepository
    {
        Task<UserLoginResponseDto> UserLogInAsync(UserLoginRequestDto request);
    }
}
