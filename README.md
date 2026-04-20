#  GestionScolaire

Application complète de gestion scolaire construite avec **Blazor Server .NET 8**, **MySQL**, et **MudBlazor**.

---

## 📋 Table des matières

1. [Architecture du projet](#architecture)
3. [Télécharger le projet sur votre ordinateur](#telecharger)
4. [Prérequis](#prerequis)
5. [Configuration et lancement](#configuration)
6. [Structure des fichiers](#structure)

---

## 🏗️ Architecture du projet <a name="architecture"></a>

```
Skolaris/
├── GestionScolaire/            # Projet Blazor Web App (.NET 8) — Frontend
├── GestionScolaire.Core/       # Class Library — Entités, Interfaces, DTOs
├── GestionScolaire.Infrastructure/ # Class Library — DbContext MySQL, Services
└── GestionScolaire.slnx        # Fichier solution
```

---

## 🔀 Étape 1 : Fusionner la branche dans main (GitHub) <a name="fusion-github"></a>

### Option A — Via l'interface GitHub (recommandé)

1. Ouvrez votre navigateur et allez sur :
   **https://github.com/bouchelaghemmohammed/Skolaris**

2. Cliquez sur l'onglet **"Pull requests"**

3. Ouvrez la Pull Request nommée **"Add complete Gestion Scolaire system..."**

4. Faites défiler vers le bas et cliquez sur **"Merge pull request"**

5. Confirmez en cliquant sur **"Confirm merge"**

6. ✅ La branche est maintenant fusionnée dans `main` !

### Option B — Via Git en ligne de commande

```bash
# 1. Clonez le dépôt (si pas encore fait)
git clone https://github.com/bouchelaghemmohammed/Skolaris.git
cd Skolaris

# 2. Basculez sur la branche dev
git checkout dev

# 3. Poussez les modifications sur GitHub
git push origin dev
```

---


## ⚙️ Prérequis <a name="prerequis"></a>

Installez les outils suivants avant de lancer le projet :

| Outil | Version | Téléchargement |
|-------|---------|----------------|
| .NET SDK | 8.0+ | https://dotnet.microsoft.com/download/dotnet/8.0 |
| MySQL Server | 8.0+ | https://dev.mysql.com/downloads/mysql/ |
| Visual Studio 2022 | 17.8+ | https://visualstudio.microsoft.com/ |
| Git | Dernière version | https://git-scm.com/downloads |

---

## 🚀 Configuration et lancement 

### 1. Configurer la base de données MySQL

```sql
-- Ouvrez MySQL Workbench ou la ligne de commande MySQL
CREATE DATABASE skolaris;
```

### 2. Mettre à jour la chaîne de connexion

Ouvrez le fichier `Skolaris/appsettings.json` et vérifiez :

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Port=3306;Database=skolaris;User=root;Password=VOTRE_MOT_DE_PASSE;"
  }
}
```

> Remplacez `VOTRE_MOT_DE_PASSE` par le mot de passe de votre MySQL.

### 3. Créer les tables (migrations EF Core)

```bash
# Dans le dossier racine du projet
cd GestionScolaire

# Installer les outils EF Core (une seule fois)
dotnet tool install --global dotnet-ef

# Créer la migration initiale
dotnet ef migrations add InitialCreate --project skolaris.Infrastructure --startup-project skolaris

# Appliquer la migration (crée les tables dans MySQL)
dotnet ef database update --project skolaris.Infrastructure --startup-project skolaris
```

### 4. Lancer l'application

#### Avec Visual Studio 2022

1. Ouvrez `skolaris.slnx` dans Visual Studio 2022
2. Définissez `skolaris` comme projet de démarrage
3. Appuyez sur **F5** ou cliquez sur ▶️ **Démarrer**

#### Avec la ligne de commande

```bash
cd skolaris
dotnet run --project skolaris
```

5. Ouvrez votre navigateur sur **http://localhost:5000**

### 5. Créer votre premier compte

1. Accédez à **http://localhost:5000/register**
2. Créez un compte avec le rôle **Super Admin**
3. Connectez-vous sur **http://localhost:5000/login**

---

## 📁 Structure des fichiers <a name="structure"></a>

```
GestionScolaire/
│
├── GestionScolaire.Core/
│   ├── Entities/          # 22 classes d'entités (Eleve, Cours, Note, etc.)
│   ├── Enums/             # 10 énumérations (RoleEnum, TypeNoteEnum, etc.)
│   ├── Interfaces/        # 12 interfaces de services
│   └── DTOs/              # Objets de transfert de données
│
├── GestionScolaire.Infrastructure/
│   ├── Data/
│   │   └── ApplicationDbContext.cs   # DbContext MySQL (Fluent API)
│   ├── Services/          # 12 implémentations de services
│   └── Migrations/        # Migrations EF Core (générées)
│
└── GestionScolaire/
    ├── Components/
    │   ├── Layout/
    │   │   ├── MainLayout.razor    # Layout principal (sidebar + appbar)
    │   │   └── AuthLayout.razor    # Layout pour login/register
    │   ├── Pages/
    │   │   ├── Dashboard.razor     # /
    │   │   ├── Login.razor         # /login
    │   │   ├── Register.razor      # /register
    │   │   ├── Eleves.razor        # /eleves
    │   │   ├── Enseignants.razor   # /enseignants
    │   │   ├── CoursPage.razor     # /cours
    │   │   ├── Notes.razor         # /notes
    │   │   ├── Absences.razor      # /absences
    │   │   ├── Bulletins.razor     # /bulletins
    │   │   ├── EmploiDuTemps.razor # /emploi-du-temps
    │   │   ├── Inscriptions.razor  # /inscriptions
    │   │   ├── Messagerie.razor    # /messagerie
    │   │   ├── Statistiques.razor  # /statistiques
    │   │   ├── Export.razor        # /export
    │   │   └── Admin/
    │   │       └── Utilisateurs.razor  # /admin/utilisateurs
    │   └── App.razor
    ├── wwwroot/
    │   └── app.css         # Styles MudBlazor personnalisés (thème bleu)
    ├── Program.cs           # Configuration DI, auth, EF Core
    └── appsettings.json     # Configuration MySQL
```

---

## 🔑 Comptes et rôles

| Rôle | Description | Accès |
|------|-------------|-------|
| **Admin École** | Gestion de l'école | Élèves, enseignants, cours, etc. |
| **Enseignant** | Gestion pédagogique | Notes, absences, emploi du temps |
| **Élève / Parent** | Consultation | Bulletins, notes, absences personnelles |

---

## 🛠️ Technologies utilisées

- **Blazor Server** (.NET 8) — Framework web interactif
- **MudBlazor 7.6.0** — Composants Material Design
- **Entity Framework Core 8** — ORM
- **Pomelo.EntityFrameworkCore.MySql 8** — Driver MySQL
- **MySQL 8** — Base de données
- **Cookie Authentication** — Authentification sécurisée

---

## ❓ Problèmes fréquents

**Erreur de connexion MySQL :**
```
Vérifiez que MySQL est démarré et que le mot de passe dans appsettings.json est correct.
```

**Erreur "dotnet-ef not found" :**
```bash
dotnet tool install --global dotnet-ef
```

**Port déjà utilisé :**
```bash
# Changez le port dans skolaris/Properties/launchSettings.json
```
