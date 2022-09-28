using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CRUD.Migrations
{
    public partial class MigracaoInicial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "jogador",
                columns: table => new
                {
                    cpf = table.Column<string>(type: "character varying", nullable: false),
                    nome = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    sobrenome = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false),
                    data_nascimento = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    nome_mae = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_cpf_jogador", x => x.cpf);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "jogador");
        }
    }
}
