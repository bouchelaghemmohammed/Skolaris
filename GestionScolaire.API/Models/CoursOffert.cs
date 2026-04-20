using System.ComponentModel.DataAnnotations;

namespace GestionScolaire.API.Models
{
    public class CoursOffert
    {
        [Key]
        public int IdCoursOffert { get; set; }

        // FK => Cours
        [Range(1, int.MaxValue)]
        public int IdCours { get; set; }

        public Cours? Cours { get; set; }

        // FK => Groupe
        // No [Range] or navigation so we keep it simple for now
        public int IdGroupe { get; set; }

        // FK => Enseignant
        // Same thing here, we keep it simple for now
        public int IdEnseignant { get; set; }

        // Session (simple for now, later to be modified)
        // FK
        public int IdSession { get; set; }

        [Required]
        public string ModeEnseignement { get; set; } = string.Empty; // Exemple: "Présentiel", "En ligne", "Hybride"

        public bool Actif { get; set; } = true; // Indicates if the offered course is active or not


        // TODO for later:
        // Modifying IdSession to be a FK to a Session table later on
    }
}

// Db currently has:
// IdCoursOffert (PK),
// IdCours (FK),
// IdGroupe (FK),
// IdSession (to be modified later to be a FK),
// IdEnseignant (FK),
// ModeEnseignement,
// Actif
