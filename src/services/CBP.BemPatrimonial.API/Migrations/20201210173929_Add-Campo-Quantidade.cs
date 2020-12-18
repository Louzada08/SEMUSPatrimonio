using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CBP.BemPatrimonial.API.Migrations
{
    public partial class AddCampoQuantidade : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.AddColumn<int>(
                name: "Quantidade",
                table: "BemPatrimonial",
                nullable: false,
                defaultValue: 1);

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pessoa_Local_LocaisId",
                table: "Pessoa");

            migrationBuilder.DropColumn(
                name: "Quantidade",
                table: "BemPatrimonial");

            migrationBuilder.AlterColumn<Guid>(
                name: "LocaisId",
                table: "Pessoa",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.AddColumn<int>(
                name: "LocalId",
                table: "Pessoa",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Pessoa_Local_LocaisId",
                table: "Pessoa",
                column: "LocaisId",
                principalTable: "Local",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
