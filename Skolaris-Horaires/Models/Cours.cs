// CRUD Cours HOR-03
namespace Skolaris.Models;

public class Cours
{
    public int Id { get; set; }
    public string Nom { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string? Code { get; set; }
    public int NiveauId { get; set; }
    public Niveau? Niveau { get; set; }
    public int ProgrammeId { get; set; }
    public Programme? Programme { get; set; }
    public int HeuresTotal { get; set; }
    public ICollection<CoursOffert> CoursOfferts { get; set; } = new List<CoursOffert>();
}

public class CoursOffert
{
    public int Id { get; set; }
    public int CoursId { get; set; }
    public Cours? Cours { get; set; }
    public int EnseignantId { get; set; }
    public Enseignant? Enseignant { get; set; }
    public int GroupeId { get; set; }
    public Groupe? Groupe { get; set; }
    public int SessionId { get; set; }
    public Session? Session { get; set; }
    public ICollection<Creneau> Creneaux { get; set; } = new List<Creneau>();
    public ICollection<Inscription> Inscriptions { get; set; } = new List<Inscription>();
}

// Emploi du temps HOR-04/06/08
public class Creneau
{
    public int Id { get; set; }
    public int CoursOffertId { get; set; }
    public CoursOffert? CoursOffert { get; set; }
    public DayOfWeek JourSemaine { get; set; }
    public TimeOnly HeureDebut { get; set; }
    public TimeOnly HeureFin { get; set; }
    public string? Salle { get; set; }
}

public class Niveau   { public int Id { get; set; } public string Nom { get; set; } = ""; }
public class Programme { public int Id { get; set; } public string Nom { get; set; } = ""; }
public class Enseignant { public int Id { get; set; } public string NomComplet { get; set; } = ""; }
public class Groupe   { public int Id { get; set; } public string Nom { get; set; } = ""; public ICollection<Inscription> Inscriptions { get; set; } = new List<Inscription>(); }
public class Session  { public int Id { get; set; } public string Nom { get; set; } = ""; }
public class Inscription { public int Id { get; set; } public int EleveId { get; set; } public int CoursOffertId { get; set; } }
public class Eleve    { public int Id { get; set; } public string NomComplet { get; set; } = ""; public int GroupeId { get; set; } }
