using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SimuladoIronFit.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AlunoId",
                table: "Personais");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AlunoId",
                table: "Personais",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
