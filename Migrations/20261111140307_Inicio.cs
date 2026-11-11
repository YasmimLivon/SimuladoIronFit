using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SimuladoIronFit.Migrations
{
    /// <inheritdoc />
    public partial class Inicio : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TreinoId",
                table: "Alunos",
                newName: "PersonalId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PersonalId",
                table: "Alunos",
                newName: "TreinoId");
        }
    }
}
