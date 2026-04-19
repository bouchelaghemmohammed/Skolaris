using skolaris.Core.Entities;
using skolaris.Core.Enums;

namespace skolaris.Core.Services;

public class NoteCalculationService
{
    public decimal? CalculerMoyenneSimple(IEnumerable<Note> notes)
    {
        var list = notes.ToList();
        if (list.Count == 0)
        {
            return null;
        }

        return list.Average(n => n.Valeur);
    }

    public decimal? CalculerMoyenneParType(
        IEnumerable<Note> notes,
        IReadOnlyDictionary<TypeNote, decimal> ponderations)
    {
        var groupes = notes.GroupBy(n => n.Type).ToList();
        if (groupes.Count == 0)
        {
            return null;
        }

        decimal weightedSum = 0m;
        decimal totalWeight = 0m;

        foreach (var groupe in groupes)
        {
            if (!ponderations.TryGetValue(groupe.Key, out var poids) || poids <= 0m)
            {
                continue;
            }

            var moyenneType = groupe.Average(n => n.Valeur);
            weightedSum += moyenneType * poids;
            totalWeight += poids;
        }

        if (totalWeight == 0m)
        {
            return null;
        }

        return weightedSum / totalWeight;
    }
}
