using Microsoft.EntityFrameworkCore.Migrations;

namespace CBP.ResponsavelPatrimonial.API.Migrations
{
    public partial class CampoFuncaoIncluido : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Funcao",
                table: "Responsavel",
                type: "varchar(25)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Funcao",
                table: "Responsavel");
        }
    }
}
