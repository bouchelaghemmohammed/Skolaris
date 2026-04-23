using skolaris.Core.Entities;

namespace skolaris.Core.Interfaces;

public interface IBulletinService
{
    Task<Bulletin?> GetBulletinAsync(int idEleve, int idSession);
    Task<IEnumerable<Bulletin>> GetBulletinsParEleveAsync(int idEleve);

    Task<Bulletin> GenererBulletinAsync(int idEleve, int idSession);
    Task<IEnumerable<Bulletin>> GenererBulletinsParGroupeAsync(int idGroupe, int idSession);

    Task<byte[]> GenererPdfAsync(int idBulletin);
    Task EnvoyerParCourrielAsync(int idBulletin);
}
