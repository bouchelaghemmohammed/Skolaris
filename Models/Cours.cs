using System.ComponentModel.DataAnnotations;

namespace Skolaris.Models
{
    public class Cours
    {
        [Key]
        public int IdCours { get; set; }

        // FK => Programme
        [Range(1, int.MaxValue)]
        public int IdProgramme { get; set; }

        public Programme? Programme { get; set; } // Programme will be null initially // Navigation property 

        // Later to be modified, it is also a FK => Niveau
        [Range(1, int.MaxValue)]
        public int IdNiveau { get; set; }

        [Required]
        [StringLength(100)]
        public string Nom { get; set; } = string.Empty;

        [Required]
        [StringLength(500, MinimumLength = 10)]
        public string Description { get; set; } = string.Empty;

        [Range(1, 10)]
        public int Credit { get; set; }

        [Range(1, 200)]
        public int Duree { get; set; }

        public bool Actif { get; set; } = true; // Indicates if the course is active or not
    }
}
