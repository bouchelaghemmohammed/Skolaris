using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GestionScolaire.API.Migrations
{
    /// <inheritdoc />
    public partial class AddCoursOffert : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CoursOfferts",
                columns: table => new
                {
                    IdCoursOffert = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdCours = table.Column<int>(type: "int", nullable: false),
                    CoursIdCours = table.Column<int>(type: "int", nullable: true),
                    IdSession = table.Column<int>(type: "int", nullable: false),
                    ModeEnseignement = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Actif = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CoursOfferts", x => x.IdCoursOffert);
                    table.ForeignKey(
                        name: "FK_CoursOfferts_Cours_CoursIdCours",
                        column: x => x.CoursIdCours,
                        principalTable: "Cours",
                        principalColumn: "IdCours");
                });

            migrationBuilder.CreateIndex(
                name: "IX_CoursOfferts_CoursIdCours",
                table: "CoursOfferts",
                column: "CoursIdCours");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CoursOfferts");
        }
    }
}
