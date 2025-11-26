using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BackendPTDetecta.Migrations
{
    /// <inheritdoc />
    public partial class Inicial2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "TIPOS_SEGURO",
                keyColumn: "NU_ID_TIPO_SEGURO",
                keyValue: 1,
                column: "FE_REG",
                value: new DateTime(2025, 11, 26, 17, 48, 16, 47, DateTimeKind.Utc).AddTicks(3944));

            migrationBuilder.UpdateData(
                table: "TIPOS_SEGURO",
                keyColumn: "NU_ID_TIPO_SEGURO",
                keyValue: 2,
                column: "FE_REG",
                value: new DateTime(2025, 11, 26, 17, 48, 16, 47, DateTimeKind.Utc).AddTicks(4212));

            migrationBuilder.UpdateData(
                table: "TIPOS_SEGURO",
                keyColumn: "NU_ID_TIPO_SEGURO",
                keyValue: 3,
                column: "FE_REG",
                value: new DateTime(2025, 11, 26, 17, 48, 16, 47, DateTimeKind.Utc).AddTicks(4214));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "TIPOS_SEGURO",
                keyColumn: "NU_ID_TIPO_SEGURO",
                keyValue: 1,
                column: "FE_REG",
                value: new DateTime(2025, 11, 26, 17, 46, 3, 388, DateTimeKind.Utc).AddTicks(7600));

            migrationBuilder.UpdateData(
                table: "TIPOS_SEGURO",
                keyColumn: "NU_ID_TIPO_SEGURO",
                keyValue: 2,
                column: "FE_REG",
                value: new DateTime(2025, 11, 26, 17, 46, 3, 388, DateTimeKind.Utc).AddTicks(7870));

            migrationBuilder.UpdateData(
                table: "TIPOS_SEGURO",
                keyColumn: "NU_ID_TIPO_SEGURO",
                keyValue: 3,
                column: "FE_REG",
                value: new DateTime(2025, 11, 26, 17, 46, 3, 388, DateTimeKind.Utc).AddTicks(7871));
        }
    }
}
