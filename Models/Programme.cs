using System.ComponentModel.DataAnnotations; // For data annotations

namespace Skolaris.Models
{
    public class Programme
    {
        [Key] // Primary Key
        public int IdProgramme { get; set; }

        [Required]
        public string Nom { get; set; } = string.Empty; // Name of the program

        [Required]
        [StringLength(500, MinimumLength = 10)]
        public string Description { get; set; } = string.Empty; // Description of the program

        public List<Cours> Cours { get; set; } = new();

    }
}


