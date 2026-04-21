Skolaris - Secion Horaires 

Module de gestion des horaires et de l'emploi du temps pour la plateforme scolaire **Skolaris**.

---

## Tickets couverts

| Ticket | Description | Heures |
|--------|-------------|--------|
| HOR-03 | CRUD Cours (catalogue) | 10h |
| HOR-04 | Emploi du temps — Vue Admin | 4h |
| HOR-06 | Emploi du temps — Vue Élève | 4h |
| HOR-08 | Emploi du temps — Vue Enseignant | 4h |

---

## Pages & Routes

| Page | URL |
|------|-----|
| Page principale | http://localhost:5220/ |
| Admin — Emploi du temps | http://localhost:5220/admin/emploi-du-temps |
| Admin — Catalogue de cours | http://localhost:5220/admin/cours |
| Élève — Mon emploi du temps | http://localhost:5220/eleve/emploi-du-temps |
| Enseignant — Mes créneaux | http://localhost:5220/enseignant/emploi-du-temps |

---

## Structure des fichiers

```
Skolaris/
├── Components/
│   └── Horaires/
│       ├── GrilleHebdomadaire.razor   # Composant grille réutilisable
│       ├── Cours.razor                # HOR-03 — CRUD Cours (Admin)
│       ├── EmploiDuTempsAdmin.razor   # HOR-04 — Vue Admin
│       ├── EmploiDuTempsEleve.razor   # HOR-06 — Vue Élève
│       └── EmploiDuTempsEnseignant.razor # HOR-08 — Vue Enseignant
├── Models/
│   └── Cours.cs                       # Modèles : Cours, CoursOffert, Creneau...
├── Services/
│   ├── CoursService.cs                # CRUD catalogue de cours
│   └── EmploiDuTempsService.cs        # Gestion des créneaux
└── Program_DI.cs                      # Enregistrement des services (DI)
```

---

## Installation

### Prérequis
- [.NET 10.0](https://dotnet.microsoft.com/download)
- Visual Studio 2022

### Étapes

1. **Cloner le repo**
   ```bash
   git clone https://github.com/bouchelaghemmohammed/Skolaris.git
   cd Skolaris
   git checkout dev
   ```

2. **Copier les fichiers** dans votre projet Blazor existant en respectant la structure ci-dessus

3. **Enregistrer les services** — ajouter dans `Program.cs` :
   ```csharp
   builder.Services.AddScoped<ICoursService, CoursService>();
   builder.Services.AddScoped<IEmploiDuTempsService, EmploiDuTempsService>();
   ```

4. **Lancer le projet**
   ```bash
   dotnet build
   dotnet run
   ```
   Ou appuyer sur **F5** dans Visual Studio.

---

## Stack technique

- **Backend** : ASP.NET Core (.NET 10)
- **Frontend** : Blazor Web App
- **UI** : Bootstrap 5

---

## Notes

- Les services utilisent des données **in-memory** pour la démo — à remplacer par EF Core + DbContext en production.
- Les vues Élève et Enseignant utilisent un ID de démo codé en dur (`GroupeIdDemo`, `EnseignantIdDemo`) — à remplacer par le contexte d'authentification (`ClaimsPrincipal`).

---

## Auteur

**Dev 3 — Horaires**  
Projet scolaire Skolaris
