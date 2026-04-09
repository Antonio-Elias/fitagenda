using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FitAgenda.Data.Migrations
{
    /// <inheritdoc />
    public partial class CriacaoInicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "alunos",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    nome = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    tipo_plano = table.Column<int>(type: "integer", nullable: false),
                    ativo = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_alunos", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "tipos_aula",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    nome = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    ativo = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tipos_aula", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "aulas",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    codigo_tipo_aula = table.Column<Guid>(type: "uuid", nullable: false),
                    data_hora = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    capacidade_maxima = table.Column<int>(type: "integer", nullable: false),
                    ativa = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_aulas", x => x.id);
                    table.ForeignKey(
                        name: "FK_aulas_tipos_aula_codigo_tipo_aula",
                        column: x => x.codigo_tipo_aula,
                        principalTable: "tipos_aula",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "agendamentos",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    codigo_aluno = table.Column<Guid>(type: "uuid", nullable: false),
                    codigo_aula = table.Column<Guid>(type: "uuid", nullable: false),
                    data_agendamento = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ativo = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_agendamentos", x => x.id);
                    table.ForeignKey(
                        name: "FK_agendamentos_alunos_codigo_aluno",
                        column: x => x.codigo_aluno,
                        principalTable: "alunos",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_agendamentos_aulas_codigo_aula",
                        column: x => x.codigo_aula,
                        principalTable: "aulas",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_agendamentos_codigo_aluno_codigo_aula",
                table: "agendamentos",
                columns: new[] { "codigo_aluno", "codigo_aula" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_agendamentos_codigo_aula",
                table: "agendamentos",
                column: "codigo_aula");

            migrationBuilder.CreateIndex(
                name: "IX_aulas_codigo_tipo_aula",
                table: "aulas",
                column: "codigo_tipo_aula");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "agendamentos");

            migrationBuilder.DropTable(
                name: "alunos");

            migrationBuilder.DropTable(
                name: "aulas");

            migrationBuilder.DropTable(
                name: "tipos_aula");
        }
    }
}
