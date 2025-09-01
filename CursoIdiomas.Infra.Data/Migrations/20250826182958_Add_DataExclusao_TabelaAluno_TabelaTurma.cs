using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CursoIdiomas.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class Add_DataExclusao_TabelaAluno_TabelaTurma : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_TB_ALUNO_CPF",
                table: "TB_ALUNO");

            migrationBuilder.DropIndex(
                name: "IX_TB_ALUNO_EMAIL",
                table: "TB_ALUNO");

            migrationBuilder.AddColumn<DateTime>(
                name: "DATA_EXCLUSAO",
                table: "TB_TURMA",
                type: "DATE",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DATA_EXCLUSAO",
                table: "TB_ALUNO",
                type: "DATE",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TB_ALUNO_CPF_EMAIL",
                table: "TB_ALUNO",
                columns: new[] { "CPF", "EMAIL" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_TB_ALUNO_CPF_EMAIL",
                table: "TB_ALUNO");

            migrationBuilder.DropColumn(
                name: "DATA_EXCLUSAO",
                table: "TB_TURMA");

            migrationBuilder.DropColumn(
                name: "DATA_EXCLUSAO",
                table: "TB_ALUNO");

            migrationBuilder.CreateIndex(
                name: "IX_TB_ALUNO_CPF",
                table: "TB_ALUNO",
                column: "CPF",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TB_ALUNO_EMAIL",
                table: "TB_ALUNO",
                column: "EMAIL",
                unique: true);
        }
    }
}
