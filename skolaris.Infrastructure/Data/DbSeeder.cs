using Skolaris.Core.Entities;
using Skolaris.Core.Enums;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace Skolaris.Infrastructure.Data;

public static class DbSeeder
{
    public static async Task SeedAsync(ApplicationDbContext context)
    {
        await context.Database.MigrateAsync();

        // Force downgrade of existing SuperAdmin records to Admin to avoid crash after removing the Enum
        await context.Database.ExecuteSqlRawAsync("UPDATE Utilisateurs SET Role = 'Admin' WHERE Role = 'SuperAdmin'");

        if (!await context.Ecoles.AnyAsync())
        {
            context.Ecoles.Add(new Ecole
            {
                Nom = "Mon École",
                Adresse = "Adresse de l'école",
                Telephone = "00000000"
            });
            await context.SaveChangesAsync();
        }

        if (!await context.Utilisateurs.AnyAsync(u => u.Email == "moh@gmail.com"))
        {
            using var sha256 = SHA256.Create();
            var bytes = Encoding.UTF8.GetBytes("123456789" + "Skolaris_Salt_2024");
            var hash = Convert.ToBase64String(sha256.ComputeHash(bytes));

            context.Utilisateurs.Add(new Utilisateur
            {
                Nom = "Admin",
                Prenom = "Admin",
                Email = "moh@gmail.com",
                MotDePasse = hash,
                Role = RoleEnum.Admin,
                Actif = true,
                DateCreation = DateTime.Now
            });
            await context.SaveChangesAsync();
        }
    }
}
