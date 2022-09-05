using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProyectoDAWBackend.Migrations
{
    public partial class ThirdMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TituloRespuesta",
                table: "Respuestas",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "TituloPregunta",
                table: "Preguntas",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TituloRespuesta",
                table: "Respuestas");

            migrationBuilder.DropColumn(
                name: "TituloPregunta",
                table: "Preguntas");
        }
    }
}
