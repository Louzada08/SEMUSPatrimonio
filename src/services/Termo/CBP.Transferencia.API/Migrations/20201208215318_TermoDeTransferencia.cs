using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CBP.Transferencia.API.Migrations
{
    public partial class TermoDeTransferencia : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TermoTransferencia",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ResponsavelCedenteId = table.Column<Guid>(nullable: false),
                    DataEmissao = table.Column<DateTime>(nullable: false),
                    DataRecebimento = table.Column<DateTime>(nullable: false),
                    DataVistoSetorDePatrimonio = table.Column<DateTime>(nullable: false),
                    Motivo = table.Column<string>(type: "varchar(250)", nullable: true),
                    ValorTotal = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TermoTransferencia", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TermoTransferenciaItens",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    PatrimonioId = table.Column<Guid>(nullable: false),
                    NumeroPatrimonio = table.Column<string>(type: "varchar(100)", nullable: true),
                    NumeroPatrimonioCP = table.Column<string>(type: "varchar(100)", nullable: true),
                    Nome = table.Column<string>(type: "varchar(100)", nullable: true),
                    Quantidade = table.Column<int>(nullable: false),
                    Valor = table.Column<decimal>(nullable: false),
                    TermoTransferenciaId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TermoTransferenciaItens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TermoTransferenciaItens_TermoTransferencia_TermoTransferenc~",
                        column: x => x.TermoTransferenciaId,
                        principalTable: "TermoTransferencia",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IDX_Cliente",
                table: "TermoTransferencia",
                column: "ResponsavelCedenteId");

            migrationBuilder.CreateIndex(
                name: "IX_TermoTransferenciaItens_TermoTransferenciaId",
                table: "TermoTransferenciaItens",
                column: "TermoTransferenciaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TermoTransferenciaItens");

            migrationBuilder.DropTable(
                name: "TermoTransferencia");
        }
    }
}
