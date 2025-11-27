using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BackendPTDetecta.Migrations
{
    /// <inheritdoc />
    public partial class ColumnaCalculadaCodigo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TIPO_SEGUROS",
                columns: table => new
                {
                    NU_ID_TIPO_SEGURO = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TX_NOM_SEG = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    NU_RUC_EMPRESA = table.Column<string>(type: "character varying(11)", maxLength: 11, nullable: false),
                    TX_TIP_COBER = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    TX_CO_PAGO = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    TX_ID_USU_REG = table.Column<string>(type: "text", nullable: false),
                    FE_REG = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    TX_ID_USU_MOD = table.Column<string>(type: "text", nullable: true),
                    FE_MOD = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    TX_ID_USU_ELI = table.Column<string>(type: "text", nullable: true),
                    FE_ELI = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    TX_MOTIVO_ELI = table.Column<string>(type: "text", nullable: true),
                    NU_ESTADO_REGISTRO = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TIPO_SEGUROS", x => x.NU_ID_TIPO_SEGURO);
                });

            migrationBuilder.CreateTable(
                name: "PACIENTES",
                columns: table => new
                {
                    NU_ID_PACIENTE = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TX_CODIGO_PACIENTE = table.Column<string>(type: "text", nullable: false, computedColumnSql: "'P' || LPAD(\"NU_ID_PACIENTE\"::TEXT, 5, '0')", stored: true),
                    NU_DNI_PACIEN = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    TX_NOM_PACIEN = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    TX_APE_PACIEN = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    TX_SEXO_PACIEN = table.Column<string>(type: "character varying(1)", maxLength: 1, nullable: false),
                    TX_FE_NAC_PACIEN = table.Column<DateTime>(type: "date", nullable: false),
                    NU_EDAD_PACIEN = table.Column<int>(type: "integer", nullable: false),
                    TX_DIR_PACIEN = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    NU_TEL_PACIEN = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    TX_EMAIL_PACIEN = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    NU_ID_HIS_CLINICA = table.Column<int>(type: "integer", nullable: true),
                    NU_ID_TIPO_SEGURO = table.Column<int>(type: "integer", nullable: true),
                    TX_ID_USU_REG = table.Column<string>(type: "text", nullable: false),
                    FE_REG = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    TX_ID_USU_MOD = table.Column<string>(type: "text", nullable: true),
                    FE_MOD = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    TX_ID_USU_ELI = table.Column<string>(type: "text", nullable: true),
                    FE_ELI = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    TX_MOTIVO_ELI = table.Column<string>(type: "text", nullable: true),
                    NU_ESTADO_REGISTRO = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PACIENTES", x => x.NU_ID_PACIENTE);
                    table.ForeignKey(
                        name: "FK_PACIENTES_TIPO_SEGUROS_NU_ID_TIPO_SEGURO",
                        column: x => x.NU_ID_TIPO_SEGURO,
                        principalTable: "TIPO_SEGUROS",
                        principalColumn: "NU_ID_TIPO_SEGURO");
                });

            migrationBuilder.CreateTable(
                name: "HISTORIAS_CLINICAS",
                columns: table => new
                {
                    NU_ID_HIS_CLINICA = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TX_CODIGO_HISTORIA = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    FE_APERTURA = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    TX_GRUP_SANG = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    TX_ALER_PRIN = table.Column<string>(type: "text", nullable: false),
                    TX_ENFER_CRONI = table.Column<string>(type: "text", nullable: false),
                    TX_ANTE_HEREDI = table.Column<string>(type: "text", nullable: false),
                    TX_ESTADO_PACIEN = table.Column<string>(type: "text", nullable: false),
                    NU_ID_PACIENTE = table.Column<int>(type: "integer", nullable: false),
                    TX_ID_USU_REG = table.Column<string>(type: "text", nullable: false),
                    FE_REG = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    TX_ID_USU_MOD = table.Column<string>(type: "text", nullable: true),
                    FE_MOD = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    TX_ID_USU_ELI = table.Column<string>(type: "text", nullable: true),
                    FE_ELI = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    TX_MOTIVO_ELI = table.Column<string>(type: "text", nullable: true),
                    NU_ESTADO_REGISTRO = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HISTORIAS_CLINICAS", x => x.NU_ID_HIS_CLINICA);
                    table.ForeignKey(
                        name: "FK_HISTORIAS_CLINICAS_PACIENTES_NU_ID_PACIENTE",
                        column: x => x.NU_ID_PACIENTE,
                        principalTable: "PACIENTES",
                        principalColumn: "NU_ID_PACIENTE",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "TIPO_SEGUROS",
                columns: new[] { "NU_ID_TIPO_SEGURO", "TX_CO_PAGO", "NU_ESTADO_REGISTRO", "FE_ELI", "FE_MOD", "FE_REG", "TX_MOTIVO_ELI", "TX_NOM_SEG", "NU_RUC_EMPRESA", "TX_TIP_COBER", "TX_ID_USU_ELI", "TX_ID_USU_MOD", "TX_ID_USU_REG" },
                values: new object[,]
                {
                    { 1, "0%", 1, null, null, new DateTime(2025, 11, 27, 20, 6, 47, 586, DateTimeKind.Utc).AddTicks(165), null, "SIS", "20100000001", "Integral", null, null, "SYSTEM" },
                    { 2, "0%", 1, null, null, new DateTime(2025, 11, 27, 20, 6, 47, 586, DateTimeKind.Utc).AddTicks(418), null, "EsSalud", "20500000002", "Laboral", null, null, "SYSTEM" },
                    { 3, "20%", 1, null, null, new DateTime(2025, 11, 27, 20, 6, 47, 586, DateTimeKind.Utc).AddTicks(420), null, "EPS Pacifico", "20600000003", "Privada", null, null, "SYSTEM" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_HISTORIAS_CLINICAS_NU_ID_PACIENTE",
                table: "HISTORIAS_CLINICAS",
                column: "NU_ID_PACIENTE",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PACIENTES_NU_DNI_PACIEN",
                table: "PACIENTES",
                column: "NU_DNI_PACIEN",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PACIENTES_NU_ID_TIPO_SEGURO",
                table: "PACIENTES",
                column: "NU_ID_TIPO_SEGURO");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HISTORIAS_CLINICAS");

            migrationBuilder.DropTable(
                name: "PACIENTES");

            migrationBuilder.DropTable(
                name: "TIPO_SEGUROS");
        }
    }
}
