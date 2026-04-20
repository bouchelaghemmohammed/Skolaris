using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Skolaris.Infrastructure.Data;

public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
{
    public ApplicationDbContext CreateDbContext(string[] args)
    {
        // pour  EF Core migrations.
        // Override connection string via environment variable MIGRATIONS_CONNECTION_STRING if needed.
        var connectionString = Environment.GetEnvironmentVariable("MIGRATIONS_CONNECTION_STRING")
            ?? "Server=localhost;Database=gestion_scolaire;User=root;Password=root;";

        var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
        optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));

        return new ApplicationDbContext(optionsBuilder.Options);
    }
}
