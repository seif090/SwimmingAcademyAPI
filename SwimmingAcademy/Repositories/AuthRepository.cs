using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using SwimmingAcademy.Data;
using SwimmingAcademy.DTOs;
using SwimmingAcademy.Helpers;
using SwimmingAcademy.Interfaces;
using SwimmingAcademy.Models;
using System.Data;

namespace SwimmingAcademy.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        private readonly SwimmingAcademyContext _context;
        private readonly JwtTokenGenerator _jwtTokenGenerator;
        private readonly IPasswordHasher<user> _passwordHasher;

        public AuthRepository(SwimmingAcademyContext context, JwtTokenGenerator jwtTokenGenerator, IPasswordHasher<user> passwordHasher)
        {
            _context = context;
            _jwtTokenGenerator = jwtTokenGenerator;
            _passwordHasher = passwordHasher;
        }

        public async Task<LoginResultDTO> LoginAsync(int userId, string password)
        {
            var user = await _context.users.FirstOrDefaultAsync(u => u.UserId == userId);
            if (user == null)
                throw new InvalidOperationException("User not found");

            var verificationResult = _passwordHasher.VerifyHashedPassword(user, user.Password, password);
            if (verificationResult != Microsoft.AspNetCore.Identity.PasswordVerificationResult.Success)
                throw new UnauthorizedAccessException("Invalid password");

            var token = _jwtTokenGenerator.GenerateToken(user);

            return new LoginResultDTO
            {
                Message = "Login successful",
                // Add other properties as needed
                // e.g., Token = token
            };
        }
    }
}
