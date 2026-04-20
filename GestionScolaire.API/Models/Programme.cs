using System.ComponentModel.DataAnnotations; // For data annotations

namespace GestionScolaire.API.Models
{
    public class Programme
    {
        [Key] // Primary Key
        public int IdProgramme { get; set; }

        [Required]
        public string Nom { get; set; } = string.Empty; // Name of the program

        public List<Cours> Cours { get; set; } = new();

    }
}

