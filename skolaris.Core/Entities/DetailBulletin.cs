namespace skolaris.Core.Entities;

public class DetailBulletin
{
    public int IdDetailBulletin { get; set; }
    public int IdBulletin { get; set; }
    public int IdNote { get; set; }
    public int IdCoursOffert { get; set; }
    public string Appreciation { get; set; } = string.Empty;
}
