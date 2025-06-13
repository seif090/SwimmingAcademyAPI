using SwimmingAcademy.Data;
using SwimmingAcademy.Models;
using SwimmingAcademy.DTOs;
using SwimmingAcademy.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
namespace SwimmingAcademy.Services
{
    public class UserService : IUserService
    {
        private readonly SwimmingAcademyContext _context;

        public UserService(SwimmingAcademyContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<user>> GetAllUsersAsync()
        {
            return await _context.users.ToListAsync();
        }

        public async Task<user?> GetUserByIdAsync(int id)
        {
            return await _context.users.FindAsync(id);
        }

        public async Task AddUserAsync(user user)
        {
            await _context.users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateUserAsync(user user)
        {
            _context.users.Update(user);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteUserAsync(int id)
        {
            var user = await _context.users.FindAsync(id);
            if (user != null)
            {
                _context.users.Remove(user);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<user?> AuthenticateAsync(string userName, string password)
        {
            var user = await _context.users
                .Include(u => u.UserType)
                .FirstOrDefaultAsync(u => u.fullname == userName && !u.disabled);

            if (user == null)
                return null;

            // Verify hashed password
            if (user.Password != password)
                return null;

            return user;
        }

        public async Task<List<string>> GetBranchNamesForUserTypeAsync(short userTypeId)
        {
            var actionIds = await _context.Users_Privs
                .Where(up => up.UserType.sub_id == userTypeId) // Fix: Use 'UserType.SubId' instead of 'UserTypeId'
                .Select(up => up.ActionID) // Fix: Correct property name 'ActionID'
                .ToListAsync();

            var branchActionNames = await _context.Actions
                .Where(a => actionIds.Contains(a.ActionID) && a.Module == "Branch" && !a.Disabled)
                .Select(a => a.ActionName)
                .ToListAsync();

            var branchNames = await _context.AppCodes
                .Where(ac => ac.sub_id >= 1 && branchActionNames.Contains(ac.description) && !ac.disabled)
                .Select(ac => ac.description)
                .ToListAsync();

            return branchNames;
        }

        public async Task<LoginResultDto?> LoginAsync(int UserId, string password)
        {
            var user = await _context.users
                .Include(u => u.SiteNavigation)
                .Include(u => u.UserType)
                .FirstOrDefaultAsync(u => u.userid == UserId && !u.disabled);

            if (user == null)
                return null;

            if (user.Password != password)
                return null;
            return new LoginResultDto
            {
                FullName = user.fullname,
                SiteSubId = user.SiteNavigation?.sub_id ?? 0,
                SiteDescription = user.SiteNavigation?.description ?? string.Empty,
                UserTypeSubId = user.UserType?.sub_id ?? 0,
                UserTypeDescription = user.UserType?.description ?? string.Empty
            };
        }
        public async Task<UserLoginDetaisDto?> LoginWithActionsAsync(int UserId, string password)
        {
            var user = await _context.users
                .Include(u => u.SiteNavigation)
                .Include(u => u.UserType)
                .FirstOrDefaultAsync(u => u.userid == UserId && !u.disabled);

            if (user == null)
                return null;

            // Use BCrypt for password verification
            if (user.Password != password)
                return null;

            // Get all action IDs allowed for this user's UserType
            var actionIds = await _context.Users_Privs
                .Where(up => up.UserTypeID == user.UserTypeID)
                .Select(up => up.ActionID)
                .ToListAsync();

            // Get the allowed actions with their names and modules
            var actions = await _context.Actions
                .Where(a => actionIds.Contains(a.ActionID) && !a.Disabled)
                .Select(a => new UserActionDto
                {
                    ActionId = a.ActionID,
                    ActionName = a.ActionName,
                    Module = a.Module
                })
                .ToListAsync();

            return new UserLoginDetaisDto
            {
                FullName = user.fullname,
                SiteSubId = user.SiteNavigation?.sub_id ?? 0,
                SiteDescription = user.SiteNavigation?.description ?? string.Empty,
                UserTypeSubId = user.UserType?.sub_id ?? 0,
                UserTypeDescription = user.UserType?.description ?? string.Empty,
                Actions = actions
            };
        }



        public async Task<List<UserActionDto>> GetAllowedActionsForUserOnSwimmerAsync(int userId, long swimmerId)
        {
            // Get the user and their UserTypeId
            var user = await _context.users.FirstOrDefaultAsync(u => u.userid == userId && !u.disabled);
            if (user == null)
                return new List<UserActionDto>();

            var userTypeId = user.UserTypeID;

            // Optionally, you can add logic here to further filter actions based on swimmer context

            // Get allowed ActionIds from UsersPriv
            var actionIds = await _context.Users_Privs
                .Where(up => up.UserTypeID == userTypeId)
                .Select(up => up.ActionID)
                .ToListAsync();

            // Get action details
            return await _context.Actions
                .Where(a => actionIds.Contains(a.ActionID) && !a.Disabled)
                .Select(a => new UserActionDto
                {
                    ActionId = a.ActionID,
                    ActionName = a.ActionName,
                    Module = a.Module
                })
                .ToListAsync();
        }
    }

    public class LoginResultDto
    {
        public string FullName { get; set; }
        public short SiteSubId { get; set; }
        public string SiteDescription { get; set; }
        public short UserTypeSubId { get; set; }
        public string UserTypeDescription { get; set; }
    }
}

