using skolaris.Core.Services;
using Xunit;

namespace skolaris.Tests;

public class BulletinCalculationServiceTests
{
    private readonly BulletinCalculationService _service = new();

    [Fact]
    public void CalculerMoyenneGenerale_EmptyList_ReturnsNull()
    {
        Assert.Null(_service.CalculerMoyenneGenerale(Array.Empty<decimal>()));
    }

    [Fact]
    public void CalculerMoyenneGenerale_SingleValue_ReturnsItself()
    {
        Assert.Equal(75m, _service.CalculerMoyenneGenerale(new[] { 75m }));
    }

    [Fact]
    public void CalculerMoyenneGenerale_ArithmeticMean()
    {
        var moyennes = new[] { 80m, 90m, 70m };

        Assert.Equal(80m, _service.CalculerMoyenneGenerale(moyennes));
    }

    [Fact]
    public void CalculerMoyenneGeneralePonderee_EmptyList_ReturnsNull()
    {
        Assert.Null(_service.CalculerMoyenneGeneralePonderee(
            Array.Empty<(decimal, int)>()));
    }

    [Fact]
    public void CalculerMoyenneGeneralePonderee_WeightedByCredits()
    {
        var cours = new[]
        {
            (Moyenne: 80m, Credits: 3),
            (Moyenne: 90m, Credits: 4),
            (Moyenne: 70m, Credits: 5)
        };

        var result = _service.CalculerMoyenneGeneralePonderee(cours);

        Assert.NotNull(result);
        Assert.Equal(79.1666666666666666666666667m, result!.Value, 6);
    }

    [Fact]
    public void CalculerMoyenneGeneralePonderee_AllCreditsZero_ReturnsNull()
    {
        var cours = new[]
        {
            (Moyenne: 80m, Credits: 0),
            (Moyenne: 90m, Credits: 0)
        };

        Assert.Null(_service.CalculerMoyenneGeneralePonderee(cours));
    }

    [Fact]
    public void CalculerMoyenneGeneralePonderee_NegativeCredits_Ignored()
    {
        var cours = new[]
        {
            (Moyenne: 80m, Credits: -3),
            (Moyenne: 90m, Credits: 1)
        };

        Assert.Equal(90m, _service.CalculerMoyenneGeneralePonderee(cours));
    }

    [Fact]
    public void CalculerMoyenneGeneralePonderee_EqualCredits_MatchesSimpleAverage()
    {
        var cours = new[]
        {
            (Moyenne: 60m, Credits: 3),
            (Moyenne: 80m, Credits: 3),
            (Moyenne: 100m, Credits: 3)
        };

        Assert.Equal(80m, _service.CalculerMoyenneGeneralePonderee(cours));
    }
}
