using Microsoft.EntityFrameworkCore.Migrations;

namespace CBP.Transferencia.API.Migrations
{
    public partial class AlteracaoCampoNomeParaDescricao : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Nome",
                table: "TermoTransferenciaItens",
                newName: "Descricao");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Descricao",
                table: "TermoTransferenciaItens",
                newName: "Nome");
        }
    }
}
