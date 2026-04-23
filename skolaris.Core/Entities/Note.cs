using skolaris.Core.Enums;

namespace skolaris.Core.Entities;

public class Note
{
    public int IdNote { get; set; }
    public int IdEleve { get; set; }
    public int IdCoursOffert { get; set; }
    public decimal Valeur { get; set; }
    public TypeNote Type { get; set; }
    public string? Commentaire { get; set; }
    public DateTime DateCreation { get; set; }
}
