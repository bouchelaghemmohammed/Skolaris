using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GestionScolaire.API.Migrations
{
    /// <inheritdoc />
    public partial class AddAssignFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IdEnseignant",
                table: "CoursOfferts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "IdGroupe",
                table: "CoursOfferts",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IdEnseignant",
                table: "CoursOfferts");

            migrationBuilder.DropColumn(
                name: "IdGroupe",
                table: "CoursOfferts");
        }
    }
}
