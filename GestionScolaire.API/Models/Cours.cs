using System.ComponentModel.DataAnnotations;

namespace GestionScolaire.API.Models
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
        public int IdNiveau { get; set; }

        [Required]
        public string Nom { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        [Range(1, 10)]
        public int Credit { get; set; }

        [Range(1, 200)]
        public int Duree { get; set; }

        public bool Actif { get; set; } = true; // Indicates if the course is active or not
    }
}
