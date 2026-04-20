using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GestionScolaire.API.Migrations
{
    /// <inheritdoc />
    public partial class AddCours : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cours",
                columns: table => new
                {
                    IdCours = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdProgramme = table.Column<int>(type: "int", nullable: false),
                    ProgrammeIdProgramme = table.Column<int>(type: "int", nullable: true),
                    IdNiveau = table.Column<int>(type: "int", nullable: false),
                    Nom = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Credit = table.Column<int>(type: "int", nullable: false),
                    Duree = table.Column<int>(type: "int", nullable: false),
                    Actif = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cours", x => x.IdCours);
                    table.ForeignKey(
                        name: "FK_Cours_Programmes_ProgrammeIdProgramme",
                        column: x => x.ProgrammeIdProgramme,
                        principalTable: "Programmes",
                        principalColumn: "IdProgramme");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cours_ProgrammeIdProgramme",
                table: "Cours",
                column: "ProgrammeIdProgramme");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cours");
        }
    }
}
