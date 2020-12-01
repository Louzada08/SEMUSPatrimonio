using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CBP.BemPatrimonial.API.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EstadoConservacao",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Nome = table.Column<string>(type: "char(10)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EstadoConservacao", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Local",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Nome = table.Column<string>(type: "varchar(100)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Local", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Pessoa",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Matricula = table.Column<int>(nullable: false),
                    PrimeiroNome = table.Column<string>(type: "varchar(80)", nullable: true),
                    SegundoNome = table.Column<string>(type: "varchar(80)", nullable: true),
                    Cargo = table.Column<string>(type: "varchar(80)", nullable: true),
                    Login = table.Column<string>(type: "varchar(100)", nullable: true),
                    Senha = table.Column<string>(type: "varchar(50)", nullable: true),
                    LocaisId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pessoa", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pessoa_Local_LocaisId",
                        column: x => x.LocaisId,
                        principalTable: "Local",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BemPatrimonial",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CodigoPatrimonio = table.Column<int>(nullable: false),
                    CodigoPatrimonioCP = table.Column<int>(nullable: true),
                    Descricao = table.Column<string>(type: "character varying(250)", nullable: false),
                    EstadoConservacaoId = table.Column<Guid>(nullable: false),
                    NumeroNotaFiscal = table.Column<int>(nullable: true),
                    LocalId = table.Column<Guid>(nullable: false),
                    LocalComplemento = table.Column<string>(type: "character varying(30)", nullable: true),
                    DataEntrada = table.Column<DateTime>(type: "date", nullable: false),
                    DataTransferencia = table.Column<DateTime>(type: "date", nullable: true),
                    DataDoacao = table.Column<DateTime>(type: "date", nullable: true),
                    DataEmprestimo = table.Column<DateTime>(type: "date", nullable: true),
                    DataRetornoEmprestimo = table.Column<DateTime>(type: "date", nullable: true),
                    DataBaixa = table.Column<DateTime>(type: "date", nullable: true),
                    ValorBem = table.Column<decimal>(type: "decimal(9,2)", nullable: false, defaultValue: 0m),
                    PessoaResponsavelId = table.Column<Guid>(nullable: true),
                    NumeroProcessoBaixa = table.Column<int>(nullable: false),
                    CodigoDaBaixa = table.Column<int>(nullable: false),
                    Ativo = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BemPatrimonial", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BemPatrimonial_EstadoConservacao_EstadoConservacaoId",
                        column: x => x.EstadoConservacaoId,
                        principalTable: "EstadoConservacao",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BemPatrimonial_Local_LocalId",
                        column: x => x.LocalId,
                        principalTable: "Local",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BemPatrimonial_Pessoa_PessoaResponsavelId",
                        column: x => x.PessoaResponsavelId,
                        principalTable: "Pessoa",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "idx_bempatrimonial_patrimonio",
                table: "BemPatrimonial",
                column: "CodigoPatrimonio",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BemPatrimonial_EstadoConservacaoId",
                table: "BemPatrimonial",
                column: "EstadoConservacaoId");

            migrationBuilder.CreateIndex(
                name: "IX_BemPatrimonial_LocalId",
                table: "BemPatrimonial",
                column: "LocalId");

            migrationBuilder.CreateIndex(
                name: "idx_bempatrimonial_numeronotafiscal",
                table: "BemPatrimonial",
                column: "NumeroNotaFiscal");

            migrationBuilder.CreateIndex(
                name: "IX_BemPatrimonial_PessoaResponsavelId",
                table: "BemPatrimonial",
                column: "PessoaResponsavelId");

            migrationBuilder.CreateIndex(
                name: "IX_Pessoa_LocaisId",
                table: "Pessoa",
                column: "LocaisId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BemPatrimonial");

            migrationBuilder.DropTable(
                name: "EstadoConservacao");

            migrationBuilder.DropTable(
                name: "Pessoa");

            migrationBuilder.DropTable(
                name: "Local");
        }
    }
}
