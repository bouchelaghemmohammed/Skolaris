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
    }
}