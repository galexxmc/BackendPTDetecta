using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BackendPTDetecta.Migrations
{
    /// <inheritdoc />
    public partial class AgregarNombresUsuario : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TX_APELLIDOS",
                table: "SEG_USUARIOS",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "TX_NOMBRES",
                table: "SEG_USUARIOS",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "TIPO_SEGUROS",
                keyColumn: "NU_ID_TIPO_SEGURO",
                keyValue: 1,
                column: "FE_REG",
                value: new DateTime(2025, 12, 3, 17, 13, 44, 732, DateTimeKind.Utc).AddTicks(6915));

            migrationBuilder.UpdateData(
                table: "TIPO_SEGUROS",
                keyColumn: "NU_ID_TIPO_SEGURO",
                keyValue: 2,
                column: "FE_REG",
                value: new DateTime(2025, 12, 3, 17, 13, 44, 732, DateTimeKind.Utc).AddTicks(7150));

            migrationBuilder.UpdateData(
                table: "TIPO_SEGUROS",
                keyColumn: "NU_ID_TIPO_SEGURO",
                keyValue: 3,
                column: "FE_REG",
                value: new DateTime(2025, 12, 3, 17, 13, 44, 732, DateTimeKind.Utc).AddTicks(7152));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TX_APELLIDOS",
                table: "SEG_USUARIOS");

            migrationBuilder.DropColumn(
                name: "TX_NOMBRES",
                table: "SEG_USUARIOS");

            migrationBuilder.UpdateData(
                table: "TIPO_SEGUROS",
                keyColumn: "NU_ID_TIPO_SEGURO",
                keyValue: 1,
                column: "FE_REG",
                value: new DateTime(2025, 12, 3, 17, 1, 33, 567, DateTimeKind.Utc).AddTicks(2865));

            migrationBuilder.UpdateData(
                table: "TIPO_SEGUROS",
                keyColumn: "NU_ID_TIPO_SEGURO",
                keyValue: 2,
                column: "FE_REG",
                value: new DateTime(2025, 12, 3, 17, 1, 33, 567, DateTimeKind.Utc).AddTicks(3105));

            migrationBuilder.UpdateData(
                table: "TIPO_SEGUROS",
                keyColumn: "NU_ID_TIPO_SEGURO",
                keyValue: 3,
                column: "FE_REG",
                value: new DateTime(2025, 12, 3, 17, 1, 33, 567, DateTimeKind.Utc).AddTicks(3107));
        }
    }
}
