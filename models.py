from flask_sqlalchemy import SQLAlchemy
from datetime import datetime

db = SQLAlchemy()


class Classe(db.Model):
    __tablename__ = 'classes'
    id = db.Column(db.Integer, primary_key=True)
    nom = db.Column(db.String(50), nullable=False)
    niveau = db.Column(db.String(50), nullable=False)
    annee_scolaire = db.Column(db.String(9), nullable=False, default='2025-2026')
    eleves = db.relationship('Eleve', backref='classe', lazy=True)

    def __repr__(self):
        return f'<Classe {self.nom}>'


class Matiere(db.Model):
    __tablename__ = 'matieres'
    id = db.Column(db.Integer, primary_key=True)
    nom = db.Column(db.String(100), nullable=False)
    coefficient = db.Column(db.Float, nullable=False, default=1.0)
    enseignant_id = db.Column(db.Integer, db.ForeignKey('enseignants.id'), nullable=True)
    notes = db.relationship('Note', backref='matiere', lazy=True)

    def __repr__(self):
        return f'<Matiere {self.nom}>'


class Enseignant(db.Model):
    __tablename__ = 'enseignants'
    id = db.Column(db.Integer, primary_key=True)
    matricule = db.Column(db.String(20), unique=True, nullable=False)
    nom = db.Column(db.String(100), nullable=False)
    prenom = db.Column(db.String(100), nullable=False)
    genre = db.Column(db.String(10), nullable=False, default='M')
    date_naissance = db.Column(db.Date, nullable=True)
    telephone = db.Column(db.String(20), nullable=True)
    email = db.Column(db.String(120), nullable=True)
    matieres = db.relationship('Matiere', backref='enseignant', lazy=True)
    date_creation = db.Column(db.DateTime, default=datetime.utcnow)

    def __repr__(self):
        return f'<Enseignant {self.nom} {self.prenom}>'


class Eleve(db.Model):
    __tablename__ = 'eleves'
    id = db.Column(db.Integer, primary_key=True)
    matricule = db.Column(db.String(20), unique=True, nullable=False)
    nom = db.Column(db.String(100), nullable=False)
    prenom = db.Column(db.String(100), nullable=False)
    genre = db.Column(db.String(10), nullable=False, default='M')
    date_naissance = db.Column(db.Date, nullable=True)
    adresse = db.Column(db.String(200), nullable=True)
    telephone_parent = db.Column(db.String(20), nullable=True)
    classe_id = db.Column(db.Integer, db.ForeignKey('classes.id'), nullable=True)
    notes = db.relationship('Note', backref='eleve', lazy=True)
    date_inscription = db.Column(db.DateTime, default=datetime.utcnow)

    def __repr__(self):
        return f'<Eleve {self.nom} {self.prenom}>'


class Note(db.Model):
    __tablename__ = 'notes'
    id = db.Column(db.Integer, primary_key=True)
    eleve_id = db.Column(db.Integer, db.ForeignKey('eleves.id'), nullable=False)
    matiere_id = db.Column(db.Integer, db.ForeignKey('matieres.id'), nullable=False)
    valeur = db.Column(db.Float, nullable=False)
    trimestre = db.Column(db.Integer, nullable=False, default=1)
    annee_scolaire = db.Column(db.String(9), nullable=False, default='2025-2026')
    date_saisie = db.Column(db.DateTime, default=datetime.utcnow)

    def __repr__(self):
        return f'<Note {self.valeur}>'
