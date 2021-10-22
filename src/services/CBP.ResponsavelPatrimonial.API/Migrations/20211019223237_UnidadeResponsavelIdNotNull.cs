using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CBP.ResponsavelPatrimonial.API.Migrations
{
    public partial class UnidadeResponsavelIdNotNull : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ResponsavelId",
                table: "Unidade");

            migrationBuilder.AlterColumn<Guid>(
                name: "UnidadeId",
                table: "Responsavel",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ResponsavelId",
                table: "Unidade",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("b3c019c1-c456-4851-9f3e-263c4dd1efd3"));

            migrationBuilder.AlterColumn<Guid>(
                name: "UnidadeId",
                table: "Responsavel",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid));
        }
    }
}
