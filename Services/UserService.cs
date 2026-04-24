using Skolaris.Data;
using Skolaris.ViewModels;

namespace Skolaris.Services
{
    public class UserService
    {
        private readonly ApplicationDbContext _context;

        public UserService(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<UserListViewModel> GetAllUsers()
        {
            return _context.Users
                .Select(u => new UserListViewModel
                {
                    Id = u.Id,
                    Nom = u.Nom,
                    Email = u.Email,
                    Role = u.Role,
                    IsActive = u.IsActive
                })
                .ToList();
        }

        public bool ToggleActive(int id, int currentUserId)
        {
            var user = _context.Users.FirstOrDefault(u => u.Id == id);

            if (user == null)
                return false;

            if (user.Id == currentUserId)
                return false;

            user.IsActive = !user.IsActive;
            _context.SaveChanges();

            return true;
        }

        public bool ChangeRole(int id, string role)
        {
            var rolesValides = new List<string>
            {
                "ADMIN",
                "ENSEIGNANT",
                "ELEVE"
            };

            if (!rolesValides.Contains(role))
                return false;

            var user = _context.Users.FirstOrDefault(u => u.Id == id);

            if (user == null)
                return false;

            user.Role = role;
            _context.SaveChanges();

            return true;
        }
    }
}