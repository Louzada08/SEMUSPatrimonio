using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CBP.ResponsavelPatrimonial.API.Migrations
{
    public partial class UnidadeResponsavel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "UnidadeId",
                table: "Responsavel",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Unidade",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Nome = table.Column<string>(type: "varchar(100)", nullable: false),
                    ResponsavelId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Unidade", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Responsavel_UnidadeId",
                table: "Responsavel",
                column: "UnidadeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Responsavel_Unidade_UnidadeId",
                table: "Responsavel",
                column: "UnidadeId",
                principalTable: "Unidade",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Responsavel_Unidade_UnidadeId",
                table: "Responsavel");

            migrationBuilder.DropTable(
                name: "Unidade");

            migrationBuilder.DropIndex(
                name: "IX_Responsavel_UnidadeId",
                table: "Responsavel");

            migrationBuilder.DropColumn(
                name: "UnidadeId",
                table: "Responsavel");
        }
    }
}
