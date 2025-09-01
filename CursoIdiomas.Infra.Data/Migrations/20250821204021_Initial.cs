using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CursoIdiomas.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TB_ALUNO",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NOME = table.Column<string>(type: "VARCHAR(100)", nullable: false),
                    CPF = table.Column<string>(type: "VARCHAR(11)", nullable: false),
                    EMAIL = table.Column<string>(type: "VARCHAR(100)", nullable: false),
                    DATA_CADASTRO = table.Column<DateTime>(type: "DATE", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_ALUNO", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "TB_TURMA",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NUMERO = table.Column<string>(type: "TEXT", nullable: false),
                    ANO_LETIVO = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_TURMA", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "TB_ALUNO_TURMA",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ALUNO_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TURMA_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DATA_MATRICULA = table.Column<DateTime>(type: "DATE", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_ALUNO_TURMA", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TB_ALUNO_TURMA_TB_ALUNO_ALUNO_ID",
                        column: x => x.ALUNO_ID,
                        principalTable: "TB_ALUNO",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TB_ALUNO_TURMA_TB_TURMA_TURMA_ID",
                        column: x => x.TURMA_ID,
                        principalTable: "TB_TURMA",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_TB_ALUNO_TURMA_ALUNO_ID_TURMA_ID",
                table: "TB_ALUNO_TURMA",
                columns: new[] { "ALUNO_ID", "TURMA_ID" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TB_ALUNO_TURMA_TURMA_ID",
                table: "TB_ALUNO_TURMA",
                column: "TURMA_ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TB_ALUNO_TURMA");

            migrationBuilder.DropTable(
                name: "TB_ALUNO");

            migrationBuilder.DropTable(
                name: "TB_TURMA");
        }
    }
}
