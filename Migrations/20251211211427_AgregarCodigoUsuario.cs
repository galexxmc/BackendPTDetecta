using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BackendPTDetecta.Migrations
{
    /// <inheritdoc />
    public partial class AgregarCodigoUsuario : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "TX_NOMBRES",
                table: "SEG_USUARIOS",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "TX_APELLIDOS",
                table: "SEG_USUARIOS",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<string>(
                name: "CodigoUsuario",
                table: "SEG_USUARIOS",
                type: "character varying(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "TIPO_SEGUROS",
                keyColumn: "NU_ID_TIPO_SEGURO",
                keyValue: 1,
                column: "FE_REG",
                value: new DateTime(2025, 12, 11, 21, 14, 26, 397, DateTimeKind.Utc).AddTicks(1303));

            migrationBuilder.UpdateData(
                table: "TIPO_SEGUROS",
                keyColumn: "NU_ID_TIPO_SEGURO",
                keyValue: 2,
                column: "FE_REG",
                value: new DateTime(2025, 12, 11, 21, 14, 26, 397, DateTimeKind.Utc).AddTicks(1533));

            migrationBuilder.UpdateData(
                table: "TIPO_SEGUROS",
                keyColumn: "NU_ID_TIPO_SEGURO",
                keyValue: 3,
                column: "FE_REG",
                value: new DateTime(2025, 12, 11, 21, 14, 26, 397, DateTimeKind.Utc).AddTicks(1535));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CodigoUsuario",
                table: "SEG_USUARIOS");

            migrationBuilder.AlterColumn<string>(
                name: "TX_NOMBRES",
                table: "SEG_USUARIOS",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "TX_APELLIDOS",
                table: "SEG_USUARIOS",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100);

            migrationBuilder.UpdateData(
                table: "TIPO_SEGUROS",
                keyColumn: "NU_ID_TIPO_SEGURO",
                keyValue: 1,
                column: "FE_REG",
                value: new DateTime(2025, 12, 5, 17, 53, 1, 269, DateTimeKind.Utc).AddTicks(1322));

            migrationBuilder.UpdateData(
                table: "TIPO_SEGUROS",
                keyColumn: "NU_ID_TIPO_SEGURO",
                keyValue: 2,
                column: "FE_REG",
                value: new DateTime(2025, 12, 5, 17, 53, 1, 269, DateTimeKind.Utc).AddTicks(1574));

            migrationBuilder.UpdateData(
                table: "TIPO_SEGUROS",
                keyColumn: "NU_ID_TIPO_SEGURO",
                keyValue: 3,
                column: "FE_REG",
                value: new DateTime(2025, 12, 5, 17, 53, 1, 269, DateTimeKind.Utc).AddTicks(1577));
        }
    }
}
