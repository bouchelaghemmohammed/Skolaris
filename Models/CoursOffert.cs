using System.ComponentModel.DataAnnotations;

namespace Skolaris.Models
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
        // No navigation so we keep it simple for now
        [Range(1, int.MaxValue)]
        public int IdGroupe { get; set; }

        // FK = > Session
        // No naviation so we keep it simple for now
        [Range(1, int.MaxValue)]
        public int IdSession { get; set; }

        // FK => Enseignant
        // No navigation so we keep it simple for now
        [Range(1, int.MaxValue)]
        public int IdEnseignant { get; set; }

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
