namespace skolaris.Core.Services;

public class BulletinCalculationService
{
    public decimal? CalculerMoyenneGenerale(IEnumerable<decimal> moyennesParCours)
    {
        var list = moyennesParCours.ToList();
        if (list.Count == 0)
        {
            return null;
        }

        return list.Average();
    }

    public decimal? CalculerMoyenneGeneralePonderee(
        IEnumerable<(decimal Moyenne, int Credits)> coursList)
    {
        decimal weightedSum = 0m;
        int totalCredits = 0;

        foreach (var (moyenne, credits) in coursList)
        {
            if (credits <= 0)
            {
                continue;
            }

            weightedSum += moyenne * credits;
            totalCredits += credits;
        }

        if (totalCredits == 0)
        {
            return null;
        }

        return weightedSum / totalCredits;
    }
}
