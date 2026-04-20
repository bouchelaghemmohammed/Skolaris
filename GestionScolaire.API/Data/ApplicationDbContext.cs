using Microsoft.EntityFrameworkCore; // Using EF Core
using GestionScolaire.API.Models; // Importing the Programme model to use it in DbSet<Programme>

namespace GestionScolaire.API.Data
{
	public class ApplicationDbContext : DbContext
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
			: base(options)

		{
		}

		// Adding DbSet properties for each model to represent tables in the database
		public DbSet<Programme> Programmes { get; set; } // Collection of objects DbSet<Programme> // Table: Programmes in SQL
		public DbSet<Cours> Cours { get; set; } // Table: Cours in SQL
		public DbSet<CoursOffert> CoursOfferts { get; set; } // Table: CoursOfferts in SQL
	}
}
