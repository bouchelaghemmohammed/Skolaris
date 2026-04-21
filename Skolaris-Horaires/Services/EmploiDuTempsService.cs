// HOR-04 / HOR-06 / HOR-08 — Service emploi du temps
using Skolaris.Models;

namespace Skolaris.Services;

public interface IEmploiDuTempsService
{
    // Créneaux — CRUD admin (HOR-04)
    Task<List<Creneau>> GetAllCreneauxAsync(int? coursOffertId = null);
    Task<Creneau?> GetCreneauByIdAsync(int id);
    Task<Creneau> CreateCreneauAsync(Creneau creneau);
    Task<Creneau> UpdateCreneauAsync(Creneau creneau);
    Task DeleteCreneauAsync(int id);

    // Vue élève (HOR-06) — filtrée par groupeId
    Task<List<Creneau>> GetCreneauxParGroupeAsync(int groupeId);

    // Vue enseignant (HOR-08) — filtrée par enseignantId
    Task<List<Creneau>> GetCreneauxParEnseignantAsync(int enseignantId);

    // Référentiel
    Task<List<CoursOffert>> GetCoursOffertsAsync();
}

public class EmploiDuTempsService : IEmploiDuTempsService
{
    // Seed de démonstration
    private readonly List<CoursOffert> _offerts;
    private readonly List<Creneau> _creneaux;
    private int _nextId = 10;

    public EmploiDuTempsService(ICoursService coursService)
    {
        // On réutilise les cours offerts du CoursService pour la cohérence
        // En prod : injecter un DbContext partagé
        _offerts = new List<CoursOffert>
        {
            new CoursOffert
            {
                Id = 1, CoursId = 1, EnseignantId = 1, GroupeId = 1, SessionId = 1,
                Cours      = new Cours     { Id = 1, Nom = "Algorithmique",  Code = "INF-101" },
                Enseignant = new Enseignant { Id = 1, NomComplet = "Marie Tremblay" },
                Groupe     = new Groupe    { Id = 1, Nom = "INF-A" },
                Session    = new Session   { Id = 1, Nom = "Automne 2024" }
            },
            new CoursOffert
            {
                Id = 2, CoursId = 2, EnseignantId = 2, GroupeId = 1, SessionId = 1,
                Cours      = new Cours     { Id = 2, Nom = "Base de données", Code = "INF-201" },
                Enseignant = new Enseignant { Id = 2, NomComplet = "Jean Dupont" },
                Groupe     = new Groupe    { Id = 1, Nom = "INF-A" },
                Session    = new Session   { Id = 1, Nom = "Automne 2024" }
            },
            new CoursOffert
            {
                Id = 3, CoursId = 3, EnseignantId = 3, GroupeId = 3, SessionId = 1,
                Cours      = new Cours     { Id = 3, Nom = "Comptabilité", Code = "GES-101" },
                Enseignant = new Enseignant { Id = 3, NomComplet = "Sophie Martin" },
                Groupe     = new Groupe    { Id = 3, Nom = "GES-A" },
                Session    = new Session   { Id = 1, Nom = "Automne 2024" }
            },
        };

        _creneaux = new List<Creneau>
        {
            new Creneau { Id=1, CoursOffertId=1, CoursOffert=_offerts[0], JourSemaine=DayOfWeek.Monday,    HeureDebut=new TimeOnly(8,30),  HeureFin=new TimeOnly(11,30), Salle="A-101" },
            new Creneau { Id=2, CoursOffertId=2, CoursOffert=_offerts[1], JourSemaine=DayOfWeek.Wednesday, HeureDebut=new TimeOnly(13,0),  HeureFin=new TimeOnly(16,0),  Salle="B-202" },
            new Creneau { Id=3, CoursOffertId=1, CoursOffert=_offerts[0], JourSemaine=DayOfWeek.Friday,    HeureDebut=new TimeOnly(8,30),  HeureFin=new TimeOnly(11,30), Salle="A-101" },
            new Creneau { Id=4, CoursOffertId=3, CoursOffert=_offerts[2], JourSemaine=DayOfWeek.Tuesday,   HeureDebut=new TimeOnly(9,0),   HeureFin=new TimeOnly(12,0),  Salle="C-010" },
        };
    }

    public Task<List<Creneau>> GetAllCreneauxAsync(int? coursOffertId = null)
    {
        var q = _creneaux.AsEnumerable();
        if (coursOffertId.HasValue) q = q.Where(c => c.CoursOffertId == coursOffertId);
        return Task.FromResult(q.OrderBy(c => c.JourSemaine).ThenBy(c => c.HeureDebut).ToList());
    }

    public Task<Creneau?> GetCreneauByIdAsync(int id) =>
        Task.FromResult(_creneaux.FirstOrDefault(c => c.Id == id));

    public Task<Creneau> CreateCreneauAsync(Creneau creneau)
    {
        creneau.Id = _nextId++;
        creneau.CoursOffert = _offerts.FirstOrDefault(o => o.Id == creneau.CoursOffertId);
        _creneaux.Add(creneau);
        return Task.FromResult(creneau);
    }

    public Task<Creneau> UpdateCreneauAsync(Creneau creneau)
    {
        var idx = _creneaux.FindIndex(c => c.Id == creneau.Id);
        if (idx >= 0)
        {
            creneau.CoursOffert = _offerts.FirstOrDefault(o => o.Id == creneau.CoursOffertId);
            _creneaux[idx] = creneau;
        }
        return Task.FromResult(creneau);
    }

    public Task DeleteCreneauAsync(int id)
    {
        _creneaux.RemoveAll(c => c.Id == id);
        return Task.CompletedTask;
    }

    // HOR-06 — filtrer par groupe via CoursOffert
    public Task<List<Creneau>> GetCreneauxParGroupeAsync(int groupeId)
    {
        var offertIds = _offerts.Where(o => o.GroupeId == groupeId).Select(o => o.Id).ToHashSet();
        var result = _creneaux
            .Where(c => offertIds.Contains(c.CoursOffertId))
            .OrderBy(c => c.JourSemaine).ThenBy(c => c.HeureDebut)
            .ToList();
        return Task.FromResult(result);
    }

    // HOR-08 — filtrer par enseignant via CoursOffert
    public Task<List<Creneau>> GetCreneauxParEnseignantAsync(int enseignantId)
    {
        var offertIds = _offerts.Where(o => o.EnseignantId == enseignantId).Select(o => o.Id).ToHashSet();
        var result = _creneaux
            .Where(c => offertIds.Contains(c.CoursOffertId))
            .OrderBy(c => c.JourSemaine).ThenBy(c => c.HeureDebut)
            .ToList();
        return Task.FromResult(result);
    }

    public Task<List<CoursOffert>> GetCoursOffertsAsync() =>
        Task.FromResult(_offerts.ToList());
}
