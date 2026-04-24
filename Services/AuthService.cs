using Microsoft.AspNetCore.Identity;
using Skolaris.Data;
using Skolaris.Models;

namespace Skolaris.Services
{
    public class AuthService
    {
        private readonly ApplicationDbContext _context;
        private readonly PasswordHasher<User> _passwordHasher;

        public AuthService(ApplicationDbContext context)
        {
            _context = context;
            _passwordHasher = new PasswordHasher<User>();
        }

        public User? Login(string email, string password)
        {
            string emailSaisi = email.Trim().ToLower();

            var user = _context.Users.FirstOrDefault(u =>
                u.Email.ToLower() == emailSaisi);

            if (user == null)
                return null;

            if (!user.IsActive)
                return null;

            var result = _passwordHasher.VerifyHashedPassword(
                user,
                user.PasswordHash,
                password
            );

            if (result == PasswordVerificationResult.Failed)
                return null;

            return user;
        }
    }
}