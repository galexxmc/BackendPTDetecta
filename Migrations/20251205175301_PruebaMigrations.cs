using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BackendPTDetecta.Migrations
{
    /// <inheritdoc />
    public partial class PruebaMigrations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
    }
}
