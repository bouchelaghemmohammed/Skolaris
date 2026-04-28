using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Skolaris.Models;

namespace Skolaris.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Programme> Programmes { get; set; }
        public DbSet<Cours> Cours { get; set; }
        public DbSet<CoursOffert> CoursOfferts { get; set; }
    }
}