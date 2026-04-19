namespace skolaris.Core.Entities;

public class Bulletin
{
    public int IdBulletin { get; set; }
    public int IdEleve { get; set; }
    public int IdSession { get; set; }
    public decimal MoyenneGenerale { get; set; }
    public int Rang { get; set; }
    public string Mention { get; set; } = string.Empty;
    public string AppreciationGenerale { get; set; } = string.Empty;
    public ICollection<DetailBulletin> DetailBulletins { get; set; } = new List<DetailBulletin>();
}
