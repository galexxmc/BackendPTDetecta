using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BackendPTDetecta.Migrations
{
    /// <inheritdoc />
    public partial class Inicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "HISTORIAS_CLINICAS",
                columns: table => new
                {
                    NU_ID_HIS_CLINICA = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TX_CODIGO_HISTORIA = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    FE_APERTURA = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    TX_ID_USU_REG = table.Column<string>(type: "text", nullable: false),
                    FE_REG = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    TX_ID_USU_MOD = table.Column<string>(type: "text", nullable: true),
                    FE_MOD = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    TX_ID_USU_ELI = table.Column<string>(type: "text", nullable: true),
                    FE_ELI = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    TX_MOTIVO_ELI = table.Column<string>(type: "text", nullable: true),
                    NU_ESTADO_REGISTRO = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HISTORIAS_CLINICAS", x => x.NU_ID_HIS_CLINICA);
                });

            migrationBuilder.CreateTable(
                name: "TIPOS_SEGURO",
                columns: table => new
                {
                    NU_ID_TIPO_SEGURO = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TX_NOMBRE_SEGURO = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    TX_DESCRIPCION = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    TX_ID_USU_REG = table.Column<string>(type: "text", nullable: false),
                    FE_REG = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    TX_ID_USU_MOD = table.Column<string>(type: "text", nullable: true),
                    FE_MOD = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    TX_ID_USU_ELI = table.Column<string>(type: "text", nullable: true),
                    FE_ELI = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    TX_MOTIVO_ELI = table.Column<string>(type: "text", nullable: true),
                    NU_ESTADO_REGISTRO = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TIPOS_SEGURO", x => x.NU_ID_TIPO_SEGURO);
                });

            migrationBuilder.CreateTable(
                name: "PACIENTES",
                columns: table => new
                {
                    NU_ID_PACIENTE = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    NU_DNI_PACIEN = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    TX_NOM_PACIEN = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    TX_APE_PACIEN = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    TX_SEXO_PACIEN = table.Column<string>(type: "character varying(1)", maxLength: 1, nullable: false),
                    TX_FE_NAC_PACIEN = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    NU_EDAD_PACIEN = table.Column<int>(type: "integer", nullable: false),
                    NU_ID_HIS_CLINICA = table.Column<int>(type: "integer", nullable: true),
                    NU_ID_TIPO_SEGURO = table.Column<int>(type: "integer", nullable: true),
                    TX_ID_USU_REG = table.Column<string>(type: "text", nullable: false),
                    FE_REG = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    TX_ID_USU_MOD = table.Column<string>(type: "text", nullable: true),
                    FE_MOD = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    TX_ID_USU_ELI = table.Column<string>(type: "text", nullable: true),
                    FE_ELI = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    TX_MOTIVO_ELI = table.Column<string>(type: "text", nullable: true),
                    NU_ESTADO_REGISTRO = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PACIENTES", x => x.NU_ID_PACIENTE);
                    table.ForeignKey(
                        name: "FK_PACIENTES_HISTORIAS_CLINICAS_NU_ID_HIS_CLINICA",
                        column: x => x.NU_ID_HIS_CLINICA,
                        principalTable: "HISTORIAS_CLINICAS",
                        principalColumn: "NU_ID_HIS_CLINICA");
                    table.ForeignKey(
                        name: "FK_PACIENTES_TIPOS_SEGURO_NU_ID_TIPO_SEGURO",
                        column: x => x.NU_ID_TIPO_SEGURO,
                        principalTable: "TIPOS_SEGURO",
                        principalColumn: "NU_ID_TIPO_SEGURO");
                });

            migrationBuilder.InsertData(
                table: "TIPOS_SEGURO",
                columns: new[] { "NU_ID_TIPO_SEGURO", "TX_DESCRIPCION", "NU_ESTADO_REGISTRO", "FE_ELI", "FE_MOD", "FE_REG", "TX_MOTIVO_ELI", "TX_NOMBRE_SEGURO", "TX_ID_USU_ELI", "TX_ID_USU_MOD", "TX_ID_USU_REG" },
                values: new object[,]
                {
                    { 1, null, 1, null, null, new DateTime(2025, 11, 26, 17, 46, 3, 388, DateTimeKind.Utc).AddTicks(7600), null, "SIS", null, null, "SYSTEM" },
                    { 2, null, 1, null, null, new DateTime(2025, 11, 26, 17, 46, 3, 388, DateTimeKind.Utc).AddTicks(7870), null, "EsSalud", null, null, "SYSTEM" },
                    { 3, null, 1, null, null, new DateTime(2025, 11, 26, 17, 46, 3, 388, DateTimeKind.Utc).AddTicks(7871), null, "Privado", null, null, "SYSTEM" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_PACIENTES_NU_ID_HIS_CLINICA",
                table: "PACIENTES",
                column: "NU_ID_HIS_CLINICA");

            migrationBuilder.CreateIndex(
                name: "IX_PACIENTES_NU_ID_TIPO_SEGURO",
                table: "PACIENTES",
                column: "NU_ID_TIPO_SEGURO");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PACIENTES");

            migrationBuilder.DropTable(
                name: "HISTORIAS_CLINICAS");

            migrationBuilder.DropTable(
                name: "TIPOS_SEGURO");
        }
    }
}
