using skolaris.Core.Entities;

namespace skolaris.Core.Interfaces;

public interface INoteService
{
    Task<Note?> GetNoteAsync(int idNote);
    Task<IEnumerable<Note>> GetNotesParCoursOffertAsync(int idCoursOffert);
    Task<IEnumerable<Note>> GetNotesParEleveAsync(int idEleve);
    Task<IEnumerable<Note>> GetNotesParEleveEtCoursOffertAsync(int idEleve, int idCoursOffert);

    Task<Note> SaisirNoteAsync(Note note);
    Task<Note> ModifierNoteAsync(int idNote, decimal nouvelleValeur);
    Task AjouterCommentaireAsync(int idNote, string commentaire);

    Task<decimal?> CalculerMoyenneCoursAsync(int idEleve, int idCoursOffert);
}
