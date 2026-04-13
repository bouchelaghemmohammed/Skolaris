from flask import Flask, render_template, request, redirect, url_for, flash
from models import db, Eleve, Enseignant, Classe, Matiere, Note
from datetime import date
import os

app = Flask(__name__)
app.config['SECRET_KEY'] = os.environ.get('SECRET_KEY', 'skolaris-secret-key-2025')
app.config['SQLALCHEMY_DATABASE_URI'] = os.environ.get('DATABASE_URL', 'sqlite:///skolaris.db')
app.config['SQLALCHEMY_TRACK_MODIFICATIONS'] = False

db.init_app(app)

with app.app_context():
    db.create_all()


# ── Dashboard ────────────────────────────────────────────────────────────────
@app.route('/')
def index():
    nb_eleves = Eleve.query.count()
    nb_enseignants = Enseignant.query.count()
    nb_classes = Classe.query.count()
    nb_matieres = Matiere.query.count()
    return render_template('index.html',
                           nb_eleves=nb_eleves,
                           nb_enseignants=nb_enseignants,
                           nb_classes=nb_classes,
                           nb_matieres=nb_matieres)


# ── Élèves ───────────────────────────────────────────────────────────────────
@app.route('/eleves')
def eleves():
    q = request.args.get('q', '')
    if q:
        items = Eleve.query.filter(
            (Eleve.nom.ilike(f'%{q}%')) | (Eleve.prenom.ilike(f'%{q}%')) | (Eleve.matricule.ilike(f'%{q}%'))
        ).all()
    else:
        items = Eleve.query.order_by(Eleve.nom).all()
    return render_template('students/index.html', eleves=items, q=q)


@app.route('/eleves/ajouter', methods=['GET', 'POST'])
def eleve_ajouter():
    classes = Classe.query.order_by(Classe.nom).all()
    if request.method == 'POST':
        dob_str = request.form.get('date_naissance')
        dob = date.fromisoformat(dob_str) if dob_str else None
        eleve = Eleve(
            matricule=request.form['matricule'],
            nom=request.form['nom'],
            prenom=request.form['prenom'],
            genre=request.form.get('genre', 'M'),
            date_naissance=dob,
            adresse=request.form.get('adresse'),
            telephone_parent=request.form.get('telephone_parent'),
            classe_id=request.form.get('classe_id') or None,
        )
        db.session.add(eleve)
        try:
            db.session.commit()
            flash('Élève ajouté avec succès.', 'success')
            return redirect(url_for('eleves'))
        except Exception:
            db.session.rollback()
            flash('Matricule déjà utilisé ou données invalides.', 'danger')
    return render_template('students/form.html', eleve=None, classes=classes, action='Ajouter')


@app.route('/eleves/<int:id>/modifier', methods=['GET', 'POST'])
def eleve_modifier(id):
    eleve = Eleve.query.get_or_404(id)
    classes = Classe.query.order_by(Classe.nom).all()
    if request.method == 'POST':
        dob_str = request.form.get('date_naissance')
        eleve.matricule = request.form['matricule']
        eleve.nom = request.form['nom']
        eleve.prenom = request.form['prenom']
        eleve.genre = request.form.get('genre', 'M')
        eleve.date_naissance = date.fromisoformat(dob_str) if dob_str else None
        eleve.adresse = request.form.get('adresse')
        eleve.telephone_parent = request.form.get('telephone_parent')
        eleve.classe_id = request.form.get('classe_id') or None
        try:
            db.session.commit()
            flash('Élève modifié avec succès.', 'success')
            return redirect(url_for('eleves'))
        except Exception:
            db.session.rollback()
            flash('Erreur lors de la modification.', 'danger')
    return render_template('students/form.html', eleve=eleve, classes=classes, action='Modifier')


@app.route('/eleves/<int:id>/supprimer', methods=['POST'])
def eleve_supprimer(id):
    eleve = Eleve.query.get_or_404(id)
    db.session.delete(eleve)
    db.session.commit()
    flash('Élève supprimé.', 'success')
    return redirect(url_for('eleves'))


# ── Enseignants ──────────────────────────────────────────────────────────────
@app.route('/enseignants')
def enseignants():
    q = request.args.get('q', '')
    if q:
        items = Enseignant.query.filter(
            (Enseignant.nom.ilike(f'%{q}%')) | (Enseignant.prenom.ilike(f'%{q}%')) | (Enseignant.matricule.ilike(f'%{q}%'))
        ).all()
    else:
        items = Enseignant.query.order_by(Enseignant.nom).all()
    return render_template('teachers/index.html', enseignants=items, q=q)


@app.route('/enseignants/ajouter', methods=['GET', 'POST'])
def enseignant_ajouter():
    if request.method == 'POST':
        dob_str = request.form.get('date_naissance')
        enseignant = Enseignant(
            matricule=request.form['matricule'],
            nom=request.form['nom'],
            prenom=request.form['prenom'],
            genre=request.form.get('genre', 'M'),
            date_naissance=date.fromisoformat(dob_str) if dob_str else None,
            telephone=request.form.get('telephone'),
            email=request.form.get('email'),
        )
        db.session.add(enseignant)
        try:
            db.session.commit()
            flash('Enseignant ajouté avec succès.', 'success')
            return redirect(url_for('enseignants'))
        except Exception:
            db.session.rollback()
            flash('Matricule déjà utilisé ou données invalides.', 'danger')
    return render_template('teachers/form.html', enseignant=None, action='Ajouter')


@app.route('/enseignants/<int:id>/modifier', methods=['GET', 'POST'])
def enseignant_modifier(id):
    enseignant = Enseignant.query.get_or_404(id)
    if request.method == 'POST':
        dob_str = request.form.get('date_naissance')
        enseignant.matricule = request.form['matricule']
        enseignant.nom = request.form['nom']
        enseignant.prenom = request.form['prenom']
        enseignant.genre = request.form.get('genre', 'M')
        enseignant.date_naissance = date.fromisoformat(dob_str) if dob_str else None
        enseignant.telephone = request.form.get('telephone')
        enseignant.email = request.form.get('email')
        try:
            db.session.commit()
            flash('Enseignant modifié avec succès.', 'success')
            return redirect(url_for('enseignants'))
        except Exception:
            db.session.rollback()
            flash('Erreur lors de la modification.', 'danger')
    return render_template('teachers/form.html', enseignant=enseignant, action='Modifier')


@app.route('/enseignants/<int:id>/supprimer', methods=['POST'])
def enseignant_supprimer(id):
    enseignant = Enseignant.query.get_or_404(id)
    db.session.delete(enseignant)
    db.session.commit()
    flash('Enseignant supprimé.', 'success')
    return redirect(url_for('enseignants'))


# ── Classes ──────────────────────────────────────────────────────────────────
@app.route('/classes')
def classes():
    items = Classe.query.order_by(Classe.nom).all()
    return render_template('classes/index.html', classes=items)


@app.route('/classes/ajouter', methods=['GET', 'POST'])
def classe_ajouter():
    if request.method == 'POST':
        classe = Classe(
            nom=request.form['nom'],
            niveau=request.form['niveau'],
            annee_scolaire=request.form.get('annee_scolaire', '2025-2026'),
        )
        db.session.add(classe)
        db.session.commit()
        flash('Classe ajoutée avec succès.', 'success')
        return redirect(url_for('classes'))
    return render_template('classes/form.html', classe=None, action='Ajouter')


@app.route('/classes/<int:id>/modifier', methods=['GET', 'POST'])
def classe_modifier(id):
    classe = Classe.query.get_or_404(id)
    if request.method == 'POST':
        classe.nom = request.form['nom']
        classe.niveau = request.form['niveau']
        classe.annee_scolaire = request.form.get('annee_scolaire', '2025-2026')
        db.session.commit()
        flash('Classe modifiée avec succès.', 'success')
        return redirect(url_for('classes'))
    return render_template('classes/form.html', classe=classe, action='Modifier')


@app.route('/classes/<int:id>/supprimer', methods=['POST'])
def classe_supprimer(id):
    classe = Classe.query.get_or_404(id)
    db.session.delete(classe)
    db.session.commit()
    flash('Classe supprimée.', 'success')
    return redirect(url_for('classes'))


# ── Matières ─────────────────────────────────────────────────────────────────
@app.route('/matieres')
def matieres():
    items = Matiere.query.order_by(Matiere.nom).all()
    return render_template('subjects/index.html', matieres=items)


@app.route('/matieres/ajouter', methods=['GET', 'POST'])
def matiere_ajouter():
    enseignants = Enseignant.query.order_by(Enseignant.nom).all()
    if request.method == 'POST':
        matiere = Matiere(
            nom=request.form['nom'],
            coefficient=float(request.form.get('coefficient', 1.0)),
            enseignant_id=request.form.get('enseignant_id') or None,
        )
        db.session.add(matiere)
        db.session.commit()
        flash('Matière ajoutée avec succès.', 'success')
        return redirect(url_for('matieres'))
    return render_template('subjects/form.html', matiere=None, enseignants=enseignants, action='Ajouter')


@app.route('/matieres/<int:id>/modifier', methods=['GET', 'POST'])
def matiere_modifier(id):
    matiere = Matiere.query.get_or_404(id)
    enseignants = Enseignant.query.order_by(Enseignant.nom).all()
    if request.method == 'POST':
        matiere.nom = request.form['nom']
        matiere.coefficient = float(request.form.get('coefficient', 1.0))
        matiere.enseignant_id = request.form.get('enseignant_id') or None
        db.session.commit()
        flash('Matière modifiée avec succès.', 'success')
        return redirect(url_for('matieres'))
    return render_template('subjects/form.html', matiere=matiere, enseignants=enseignants, action='Modifier')


@app.route('/matieres/<int:id>/supprimer', methods=['POST'])
def matiere_supprimer(id):
    matiere = Matiere.query.get_or_404(id)
    db.session.delete(matiere)
    db.session.commit()
    flash('Matière supprimée.', 'success')
    return redirect(url_for('matieres'))


# ── Notes ─────────────────────────────────────────────────────────────────────
@app.route('/notes')
def notes():
    classe_id = request.args.get('classe_id', type=int)
    matiere_id = request.args.get('matiere_id', type=int)
    trimestre = request.args.get('trimestre', type=int)

    query = Note.query.join(Eleve).join(Matiere)
    if classe_id:
        query = query.filter(Eleve.classe_id == classe_id)
    if matiere_id:
        query = query.filter(Note.matiere_id == matiere_id)
    if trimestre:
        query = query.filter(Note.trimestre == trimestre)

    items = query.order_by(Eleve.nom, Matiere.nom).all()
    classes = Classe.query.order_by(Classe.nom).all()
    matieres_list = Matiere.query.order_by(Matiere.nom).all()
    return render_template('grades/index.html',
                           notes=items, classes=classes, matieres=matieres_list,
                           classe_id=classe_id, matiere_id=matiere_id, trimestre=trimestre)


@app.route('/notes/ajouter', methods=['GET', 'POST'])
def note_ajouter():
    eleves = Eleve.query.order_by(Eleve.nom).all()
    matieres_list = Matiere.query.order_by(Matiere.nom).all()
    if request.method == 'POST':
        note = Note(
            eleve_id=int(request.form['eleve_id']),
            matiere_id=int(request.form['matiere_id']),
            valeur=float(request.form['valeur']),
            trimestre=int(request.form.get('trimestre', 1)),
            annee_scolaire=request.form.get('annee_scolaire', '2025-2026'),
        )
        db.session.add(note)
        db.session.commit()
        flash('Note ajoutée avec succès.', 'success')
        return redirect(url_for('notes'))
    return render_template('grades/form.html', note=None, eleves=eleves, matieres=matieres_list, action='Ajouter')


@app.route('/notes/<int:id>/modifier', methods=['GET', 'POST'])
def note_modifier(id):
    note = Note.query.get_or_404(id)
    eleves = Eleve.query.order_by(Eleve.nom).all()
    matieres_list = Matiere.query.order_by(Matiere.nom).all()
    if request.method == 'POST':
        note.eleve_id = int(request.form['eleve_id'])
        note.matiere_id = int(request.form['matiere_id'])
        note.valeur = float(request.form['valeur'])
        note.trimestre = int(request.form.get('trimestre', 1))
        note.annee_scolaire = request.form.get('annee_scolaire', '2025-2026')
        db.session.commit()
        flash('Note modifiée avec succès.', 'success')
        return redirect(url_for('notes'))
    return render_template('grades/form.html', note=note, eleves=eleves, matieres=matieres_list, action='Modifier')


@app.route('/notes/<int:id>/supprimer', methods=['POST'])
def note_supprimer(id):
    note = Note.query.get_or_404(id)
    db.session.delete(note)
    db.session.commit()
    flash('Note supprimée.', 'success')
    return redirect(url_for('notes'))


# ── Bulletin ──────────────────────────────────────────────────────────────────
@app.route('/bulletin/<int:eleve_id>')
def bulletin(eleve_id):
    eleve = Eleve.query.get_or_404(eleve_id)
    trimestre = request.args.get('trimestre', 1, type=int)
    notes = Note.query.filter_by(eleve_id=eleve_id, trimestre=trimestre).all()
    total_points = sum(n.valeur * n.matiere.coefficient for n in notes)
    total_coeff = sum(n.matiere.coefficient for n in notes)
    moyenne = total_points / total_coeff if total_coeff else 0
    return render_template('grades/bulletin.html',
                           eleve=eleve, notes=notes, trimestre=trimestre,
                           moyenne=moyenne)


if __name__ == '__main__':
    debug = os.environ.get('FLASK_DEBUG', '0') == '1'
    app.run(debug=debug)
