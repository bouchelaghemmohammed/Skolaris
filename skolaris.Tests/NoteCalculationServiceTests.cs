using skolaris.Core.Entities;
using skolaris.Core.Enums;
using skolaris.Core.Services;
using Xunit;

namespace skolaris.Tests;

public class NoteCalculationServiceTests
{
    private readonly NoteCalculationService _service = new();

    [Fact]
    public void CalculerMoyenneSimple_EmptyList_ReturnsNull()
    {
        Assert.Null(_service.CalculerMoyenneSimple(Array.Empty<Note>()));
    }

    [Fact]
    public void CalculerMoyenneSimple_ArithmeticMean()
    {
        var notes = new[]
        {
            new Note { Valeur = 80m, Type = TypeNote.Devoir },
            new Note { Valeur = 90m, Type = TypeNote.Examen },
            new Note { Valeur = 70m, Type = TypeNote.Interro }
        };

        Assert.Equal(80m, _service.CalculerMoyenneSimple(notes));
    }

    [Fact]
    public void CalculerMoyenneSimple_SingleNote_ReturnsItsValue()
    {
        var notes = new[] { new Note { Valeur = 17.5m, Type = TypeNote.Projet } };

        Assert.Equal(17.5m, _service.CalculerMoyenneSimple(notes));
    }

    [Fact]
    public void CalculerMoyenneParType_EmptyList_ReturnsNull()
    {
        var poids = new Dictionary<TypeNote, decimal>
        {
            [TypeNote.Devoir] = 0.3m,
            [TypeNote.Examen] = 0.7m
        };

        Assert.Null(_service.CalculerMoyenneParType(Array.Empty<Note>(), poids));
    }

    [Fact]
    public void CalculerMoyenneParType_AveragesWithinTypeThenApplyWeights()
    {
        var notes = new[]
        {
            new Note { Valeur = 80m, Type = TypeNote.Devoir },
            new Note { Valeur = 90m, Type = TypeNote.Examen },
            new Note { Valeur = 70m, Type = TypeNote.Examen }
        };
        var poids = new Dictionary<TypeNote, decimal>
        {
            [TypeNote.Devoir] = 0.3m,
            [TypeNote.Examen] = 0.7m
        };

        var result = _service.CalculerMoyenneParType(notes, poids);

        Assert.Equal(80m, result);
    }

    [Fact]
    public void CalculerMoyenneParType_WeightsNotSummingToOne_NormalizedByTotal()
    {
        var notes = new[]
        {
            new Note { Valeur = 80m, Type = TypeNote.Devoir },
            new Note { Valeur = 90m, Type = TypeNote.Examen }
        };
        var poids = new Dictionary<TypeNote, decimal>
        {
            [TypeNote.Devoir] = 2m,
            [TypeNote.Examen] = 3m
        };

        var result = _service.CalculerMoyenneParType(notes, poids);

        Assert.Equal(86m, result);
    }

    [Fact]
    public void CalculerMoyenneParType_TypeWithoutWeight_Ignored()
    {
        var notes = new[]
        {
            new Note { Valeur = 80m, Type = TypeNote.Devoir },
            new Note { Valeur = 100m, Type = TypeNote.Projet }
        };
        var poids = new Dictionary<TypeNote, decimal>
        {
            [TypeNote.Devoir] = 1m
        };

        var result = _service.CalculerMoyenneParType(notes, poids);

        Assert.Equal(80m, result);
    }

    [Fact]
    public void CalculerMoyenneParType_NoMatchingWeights_ReturnsNull()
    {
        var notes = new[]
        {
            new Note { Valeur = 80m, Type = TypeNote.Devoir }
        };
        var poids = new Dictionary<TypeNote, decimal>
        {
            [TypeNote.Examen] = 1m
        };

        Assert.Null(_service.CalculerMoyenneParType(notes, poids));
    }

    [Fact]
    public void CalculerMoyenneParType_ZeroWeight_Ignored()
    {
        var notes = new[]
        {
            new Note { Valeur = 60m, Type = TypeNote.Devoir },
            new Note { Valeur = 90m, Type = TypeNote.Examen }
        };
        var poids = new Dictionary<TypeNote, decimal>
        {
            [TypeNote.Devoir] = 0m,
            [TypeNote.Examen] = 1m
        };

        var result = _service.CalculerMoyenneParType(notes, poids);

        Assert.Equal(90m, result);
    }}
