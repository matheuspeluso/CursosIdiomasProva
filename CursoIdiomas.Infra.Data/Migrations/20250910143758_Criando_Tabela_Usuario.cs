using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CursoIdiomas.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class Criando_Tabela_Usuario : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "NUMERO",
                table: "TB_TURMA",
                type: "VARCHAR(10)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<string>(
                name: "ANO_LETIVO",
                table: "TB_TURMA",
                type: "VARCHAR(10)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.CreateTable(
                name: "TB_USUARIO",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NOME = table.Column<string>(type: "VARCHAR(100)", nullable: false),
                    EMAIL = table.Column<string>(type: "VARCHAR(100)", nullable: false),
                    SENHA = table.Column<string>(type: "VARCHAR(100)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_USUARIO", x => x.ID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TB_TURMA_NUMERO",
                table: "TB_TURMA",
                column: "NUMERO",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TB_USUARIO_EMAIL",
                table: "TB_USUARIO",
                column: "EMAIL",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TB_USUARIO");

            migrationBuilder.DropIndex(
                name: "IX_TB_TURMA_NUMERO",
                table: "TB_TURMA");

            migrationBuilder.AlterColumn<string>(
                name: "NUMERO",
                table: "TB_TURMA",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "VARCHAR(10)");

            migrationBuilder.AlterColumn<string>(
                name: "ANO_LETIVO",
                table: "TB_TURMA",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "VARCHAR(10)");
        }
    }
}
