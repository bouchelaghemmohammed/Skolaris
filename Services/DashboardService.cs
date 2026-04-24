using Skolaris.Data;
using Skolaris.ViewModels;

namespace Skolaris.Services
{
    public class DashboardService
    {
        private readonly ApplicationDbContext _context;

        public DashboardService(ApplicationDbContext context)
        {
            _context = context;
        }

        public DashboardStatsViewModel GetAdminStats()
        {
            return new DashboardStatsViewModel
            {
                TotalUsers = _context.Users.Count(),

                TotalAdmins = _context.Users.Count(u => u.Role == "ADMIN"),

                TotalEnseignants = _context.Users.Count(u => u.Role == "ENSEIGNANT"),

                TotalEleves = _context.Users.Count(u => u.Role == "ELEVE"),

                ActiveUsers = _context.Users.Count(u => u.IsActive),

                InactiveUsers = _context.Users.Count(u => !u.IsActive)
            };
        }
    }
}