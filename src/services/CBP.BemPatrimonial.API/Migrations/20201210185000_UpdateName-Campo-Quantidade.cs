using Microsoft.EntityFrameworkCore.Migrations;

namespace CBP.BemPatrimonial.API.Migrations
{
    public partial class UpdateNameCampoQuantidade : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Quantidade",
                table: "BemPatrimonial");

            migrationBuilder.AddColumn<int>(
                name: "QuantidadeEstoque",
                table: "BemPatrimonial",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "QuantidadeEstoque",
                table: "BemPatrimonial");

            migrationBuilder.AddColumn<int>(
                name: "Quantidade",
                table: "BemPatrimonial",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
