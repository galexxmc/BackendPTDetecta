using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BackendPTDetecta.Migrations
{
    /// <inheritdoc />
    public partial class CompletarEsquemaMedico : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HISTORIAL_CLINICO_PACIENTES_NU_ID_PACIENTE",
                table: "HISTORIAL_CLINICO");

            migrationBuilder.DropForeignKey(
                name: "FK_PACIENTES_TIPO_SEGUROS_NU_ID_TIPO_SEGURO",
                table: "PACIENTES");

            migrationBuilder.DropPrimaryKey(
                name: "PK_HISTORIAL_CLINICO",
                table: "HISTORIAL_CLINICO");

            migrationBuilder.RenameTable(
                name: "HISTORIAL_CLINICO",
                newName: "HISTORIAS_CLINICAS");

            migrationBuilder.RenameColumn(
                name: "FE_APER_HIS_CLIN",
                table: "HISTORIAS_CLINICAS",
                newName: "FE_APERTURA");

            migrationBuilder.RenameIndex(
                name: "IX_HISTORIAL_CLINICO_NU_ID_PACIENTE",
                table: "HISTORIAS_CLINICAS",
                newName: "IX_HISTORIAS_CLINICAS_NU_ID_PACIENTE");

            migrationBuilder.AlterColumn<int>(
                name: "NU_ID_TIPO_SEGURO",
                table: "PACIENTES",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<string>(
                name: "NU_DNI_PACIEN",
                table: "PACIENTES",
                type: "character varying(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(8)",
                oldMaxLength: 8);

            migrationBuilder.AddColumn<int>(
                name: "NU_EDAD_PACIEN",
                table: "PACIENTES",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "NU_ID_HIS_CLINICA",
                table: "PACIENTES",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TX_CODIGO_HISTORIA",
                table: "HISTORIAS_CLINICAS",
                type: "character varying(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_HISTORIAS_CLINICAS",
                table: "HISTORIAS_CLINICAS",
                column: "NU_ID_HIS_CLINICA");

            migrationBuilder.UpdateData(
                table: "TIPO_SEGUROS",
                keyColumn: "NU_ID_TIPO_SEGURO",
                keyValue: 1,
                column: "FE_REG",
                value: new DateTime(2025, 11, 26, 20, 34, 53, 440, DateTimeKind.Utc).AddTicks(895));

            migrationBuilder.UpdateData(
                table: "TIPO_SEGUROS",
                keyColumn: "NU_ID_TIPO_SEGURO",
                keyValue: 2,
                column: "FE_REG",
                value: new DateTime(2025, 11, 26, 20, 34, 53, 440, DateTimeKind.Utc).AddTicks(1139));

            migrationBuilder.UpdateData(
                table: "TIPO_SEGUROS",
                keyColumn: "NU_ID_TIPO_SEGURO",
                keyValue: 3,
                column: "FE_REG",
                value: new DateTime(2025, 11, 26, 20, 34, 53, 440, DateTimeKind.Utc).AddTicks(1141));

            migrationBuilder.AddForeignKey(
                name: "FK_HISTORIAS_CLINICAS_PACIENTES_NU_ID_PACIENTE",
                table: "HISTORIAS_CLINICAS",
                column: "NU_ID_PACIENTE",
                principalTable: "PACIENTES",
                principalColumn: "NU_ID_PACIENTE",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PACIENTES_TIPO_SEGUROS_NU_ID_TIPO_SEGURO",
                table: "PACIENTES",
                column: "NU_ID_TIPO_SEGURO",
                principalTable: "TIPO_SEGUROS",
                principalColumn: "NU_ID_TIPO_SEGURO");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HISTORIAS_CLINICAS_PACIENTES_NU_ID_PACIENTE",
                table: "HISTORIAS_CLINICAS");

            migrationBuilder.DropForeignKey(
                name: "FK_PACIENTES_TIPO_SEGUROS_NU_ID_TIPO_SEGURO",
                table: "PACIENTES");

            migrationBuilder.DropPrimaryKey(
                name: "PK_HISTORIAS_CLINICAS",
                table: "HISTORIAS_CLINICAS");

            migrationBuilder.DropColumn(
                name: "NU_EDAD_PACIEN",
                table: "PACIENTES");

            migrationBuilder.DropColumn(
                name: "NU_ID_HIS_CLINICA",
                table: "PACIENTES");

            migrationBuilder.DropColumn(
                name: "TX_CODIGO_HISTORIA",
                table: "HISTORIAS_CLINICAS");

            migrationBuilder.RenameTable(
                name: "HISTORIAS_CLINICAS",
                newName: "HISTORIAL_CLINICO");

            migrationBuilder.RenameColumn(
                name: "FE_APERTURA",
                table: "HISTORIAL_CLINICO",
                newName: "FE_APER_HIS_CLIN");

            migrationBuilder.RenameIndex(
                name: "IX_HISTORIAS_CLINICAS_NU_ID_PACIENTE",
                table: "HISTORIAL_CLINICO",
                newName: "IX_HISTORIAL_CLINICO_NU_ID_PACIENTE");

            migrationBuilder.AlterColumn<int>(
                name: "NU_ID_TIPO_SEGURO",
                table: "PACIENTES",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "NU_DNI_PACIEN",
                table: "PACIENTES",
                type: "character varying(8)",
                maxLength: 8,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(20)",
                oldMaxLength: 20);

            migrationBuilder.AddPrimaryKey(
                name: "PK_HISTORIAL_CLINICO",
                table: "HISTORIAL_CLINICO",
                column: "NU_ID_HIS_CLINICA");

            migrationBuilder.UpdateData(
                table: "TIPO_SEGUROS",
                keyColumn: "NU_ID_TIPO_SEGURO",
                keyValue: 1,
                column: "FE_REG",
                value: new DateTime(2025, 11, 26, 20, 8, 45, 726, DateTimeKind.Utc).AddTicks(7365));

            migrationBuilder.UpdateData(
                table: "TIPO_SEGUROS",
                keyColumn: "NU_ID_TIPO_SEGURO",
                keyValue: 2,
                column: "FE_REG",
                value: new DateTime(2025, 11, 26, 20, 8, 45, 726, DateTimeKind.Utc).AddTicks(7640));

            migrationBuilder.UpdateData(
                table: "TIPO_SEGUROS",
                keyColumn: "NU_ID_TIPO_SEGURO",
                keyValue: 3,
                column: "FE_REG",
                value: new DateTime(2025, 11, 26, 20, 8, 45, 726, DateTimeKind.Utc).AddTicks(7642));

            migrationBuilder.AddForeignKey(
                name: "FK_HISTORIAL_CLINICO_PACIENTES_NU_ID_PACIENTE",
                table: "HISTORIAL_CLINICO",
                column: "NU_ID_PACIENTE",
                principalTable: "PACIENTES",
                principalColumn: "NU_ID_PACIENTE",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PACIENTES_TIPO_SEGUROS_NU_ID_TIPO_SEGURO",
                table: "PACIENTES",
                column: "NU_ID_TIPO_SEGURO",
                principalTable: "TIPO_SEGUROS",
                principalColumn: "NU_ID_TIPO_SEGURO",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
