// Service CRUD Cours
using Skolaris.Models;

namespace Skolaris.Services;

public interface ICoursService
{
    // Catalogue de cours
    Task<List<Cours>> GetAllCoursAsync();
    Task<Cours?> GetCoursByIdAsync(int id);
    Task<Cours> CreateCoursAsync(Cours cours);
    Task<Cours> UpdateCoursAsync(Cours cours);
    Task DeleteCoursAsync(int id);

    // Cours offerts (liaison cours pr groupe-enseignant-session)
    Task<List<CoursOffert>> GetCoursOffertsAsync(int? sessionId = null, int? groupeId = null);
    Task<CoursOffert?> GetCoursOffertByIdAsync(int id);
    Task<CoursOffert> CreateCoursOffertAsync(CoursOffert co);
    Task<CoursOffert> UpdateCoursOffertAsync(CoursOffert co);
    Task DeleteCoursOffertAsync(int id);

    Task<List<Niveau>> GetNiveauxAsync();
    Task<List<Programme>> GetProgrammesAsync();
    Task<List<Enseignant>> GetEnseignantsAsync();
    Task<List<Groupe>> GetGroupesAsync();
    Task<List<Session>> GetSessionsAsync();
}

/// <summary>
/// Implémentation in-memory — remplacer par EF Core / DbContext en prod.
/// </summary>
public class CoursService : ICoursService
{
    private readonly List<Niveau> _niveaux = new()
    {
        new Niveau { Id = 1, Nom = "Débutant" },
        new Niveau { Id = 2, Nom = "Intermédiaire" },
        new Niveau { Id = 3, Nom = "Avancé" },
    };

    private readonly List<Programme> _programmes = new()
    {
        new Programme { Id = 1, Nom = "Informatique" },
        new Programme { Id = 2, Nom = "Gestion" },
        new Programme { Id = 3, Nom = "Design" },
    };

    private readonly List<Enseignant> _enseignants = new()
    {
        new Enseignant { Id = 1, NomComplet = "Marie Tremblay" },
        new Enseignant { Id = 2, NomComplet = "Jean Dupont" },
        new Enseignant { Id = 3, NomComplet = "Sophie Martin" },
    };

    private readonly List<Groupe> _groupes = new()
    {
        new Groupe { Id = 1, Nom = "INF-A" },
        new Groupe { Id = 2, Nom = "INF-B" },
        new Groupe { Id = 3, Nom = "GES-A" },
    };

    private readonly List<Session> _sessions = new()
    {
        new Session { Id = 1, Nom = "Automne 2024" },
        new Session { Id = 2, Nom = "Hiver 2025" },
    };

    private readonly List<Cours> _cours = new()
    {
        new Cours { Id = 1, Nom = "Algorithmique", Code = "INF-101", NiveauId = 1, ProgrammeId = 1, HeuresTotal = 45 },
        new Cours { Id = 2, Nom = "Base de données", Code = "INF-201", NiveauId = 2, ProgrammeId = 1, HeuresTotal = 60 },
        new Cours { Id = 3, Nom = "Comptabilité", Code = "GES-101", NiveauId = 1, ProgrammeId = 2, HeuresTotal = 45 },
    };

    private readonly List<CoursOffert> _offerts = new();
    private int _coursNextId = 10;
    private int _offertNextId = 10;

    // Cours catalogue
    public Task<List<Cours>> GetAllCoursAsync() => Task.FromResult(_cours.ToList());

    public Task<Cours?> GetCoursByIdAsync(int id) =>
        Task.FromResult(_cours.FirstOrDefault(c => c.Id == id));

    public Task<Cours> CreateCoursAsync(Cours cours)
    {
        cours.Id = _coursNextId++;
        _cours.Add(cours);
        return Task.FromResult(cours);
    }

    public Task<Cours> UpdateCoursAsync(Cours cours)
    {
        var idx = _cours.FindIndex(c => c.Id == cours.Id);
        if (idx >= 0) _cours[idx] = cours;
        return Task.FromResult(cours);
    }

    public Task DeleteCoursAsync(int id)
    {
        _cours.RemoveAll(c => c.Id == id);
        return Task.CompletedTask;
    }

    // Cours offerts 
    public Task<List<CoursOffert>> GetCoursOffertsAsync(int? sessionId = null, int? groupeId = null)
    {
        var q = _offerts.AsEnumerable();
        if (sessionId.HasValue) q = q.Where(o => o.SessionId == sessionId);
        if (groupeId.HasValue)  q = q.Where(o => o.GroupeId  == groupeId);
        return Task.FromResult(q.ToList());
    }

    public Task<CoursOffert?> GetCoursOffertByIdAsync(int id) =>
        Task.FromResult(_offerts.FirstOrDefault(o => o.Id == id));

    public Task<CoursOffert> CreateCoursOffertAsync(CoursOffert co)
    {
        co.Id = _offertNextId++;
        co.Cours       = _cours.FirstOrDefault(c => c.Id == co.CoursId);
        co.Enseignant  = _enseignants.FirstOrDefault(e => e.Id == co.EnseignantId);
        co.Groupe      = _groupes.FirstOrDefault(g => g.Id == co.GroupeId);
        co.Session     = _sessions.FirstOrDefault(s => s.Id == co.SessionId);
        _offerts.Add(co);
        return Task.FromResult(co);
    }

    public Task<CoursOffert> UpdateCoursOffertAsync(CoursOffert co)
    {
        var idx = _offerts.FindIndex(o => o.Id == co.Id);
        if (idx >= 0)
        {
            co.Cours      = _cours.FirstOrDefault(c => c.Id == co.CoursId);
            co.Enseignant = _enseignants.FirstOrDefault(e => e.Id == co.EnseignantId);
            co.Groupe     = _groupes.FirstOrDefault(g => g.Id == co.GroupeId);
            co.Session    = _sessions.FirstOrDefault(s => s.Id == co.SessionId);
            _offerts[idx] = co;
        }
        return Task.FromResult(co);
    }

    public Task DeleteCoursOffertAsync(int id)
    {
        _offerts.RemoveAll(o => o.Id == id);
        return Task.CompletedTask;
    }

    public Task<List<Niveau>>     GetNiveauxAsync()     => Task.FromResult(_niveaux.ToList());
    public Task<List<Programme>>  GetProgrammesAsync()  => Task.FromResult(_programmes.ToList());
    public Task<List<Enseignant>> GetEnseignantsAsync() => Task.FromResult(_enseignants.ToList());
    public Task<List<Groupe>>     GetGroupesAsync()     => Task.FromResult(_groupes.ToList());
    public Task<List<Session>>    GetSessionsAsync()    => Task.FromResult(_sessions.ToList());
}
