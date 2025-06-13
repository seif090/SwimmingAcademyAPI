using SwimmingAcademy.DTOs;
using SwimmingAcademy.Models;

namespace SwimmingAcademy.Services.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<user>> GetAllUsersAsync();
        Task<user?> GetUserByIdAsync(int id);
        Task AddUserAsync(user user);
        Task UpdateUserAsync(user user);
        Task DeleteUserAsync(int id);
        Task<List<string>> GetBranchNamesForUserTypeAsync(short userTypeId);
        Task<user?> AuthenticateAsync(string userName, string password);
        Task<LoginResultDto?> LoginAsync(int UserId, string password);
        Task<UserLoginDetaisDto?> LoginWithActionsAsync(int UserId, string password);
        Task<List<UserActionDto>> GetAllowedActionsForUserOnSwimmerAsync(int userId, long swimmerId);
    }
}
