using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BackendPTDetecta.Migrations
{
    /// <inheritdoc />
    public partial class RenombrarTablasIdentity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                table: "AspNetRoleClaims");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                table: "AspNetUserClaims");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                table: "AspNetUserLogins");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                table: "AspNetUserRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                table: "AspNetUserRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                table: "AspNetUserTokens");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AspNetUserTokens",
                table: "AspNetUserTokens");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AspNetUsers",
                table: "AspNetUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AspNetUserRoles",
                table: "AspNetUserRoles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AspNetUserLogins",
                table: "AspNetUserLogins");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AspNetUserClaims",
                table: "AspNetUserClaims");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AspNetRoles",
                table: "AspNetRoles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AspNetRoleClaims",
                table: "AspNetRoleClaims");

            migrationBuilder.RenameTable(
                name: "AspNetUserTokens",
                newName: "SEG_USUARIO_TOKENS");

            migrationBuilder.RenameTable(
                name: "AspNetUsers",
                newName: "SEG_USUARIOS");

            migrationBuilder.RenameTable(
                name: "AspNetUserRoles",
                newName: "SEG_USUARIO_ROLES");

            migrationBuilder.RenameTable(
                name: "AspNetUserLogins",
                newName: "SEG_USUARIO_LOGINS");

            migrationBuilder.RenameTable(
                name: "AspNetUserClaims",
                newName: "SEG_USUARIO_CLAIMS");

            migrationBuilder.RenameTable(
                name: "AspNetRoles",
                newName: "SEG_ROLES");

            migrationBuilder.RenameTable(
                name: "AspNetRoleClaims",
                newName: "SEG_ROL_CLAIMS");

            migrationBuilder.RenameColumn(
                name: "Value",
                table: "SEG_USUARIO_TOKENS",
                newName: "TX_VALOR_TOKEN");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "SEG_USUARIO_TOKENS",
                newName: "TX_NOMBRE_TOKEN");

            migrationBuilder.RenameColumn(
                name: "LoginProvider",
                table: "SEG_USUARIO_TOKENS",
                newName: "TX_PROVEEDOR");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "SEG_USUARIO_TOKENS",
                newName: "TX_ID_USUARIO");

            migrationBuilder.RenameColumn(
                name: "UserName",
                table: "SEG_USUARIOS",
                newName: "TX_USERNAME");

            migrationBuilder.RenameColumn(
                name: "TwoFactorEnabled",
                table: "SEG_USUARIOS",
                newName: "BL_2FA_ENABLED");

            migrationBuilder.RenameColumn(
                name: "SecurityStamp",
                table: "SEG_USUARIOS",
                newName: "TX_SECURITY_STAMP");

            migrationBuilder.RenameColumn(
                name: "PhoneNumberConfirmed",
                table: "SEG_USUARIOS",
                newName: "BL_TELEFONO_CONFIRMADO");

            migrationBuilder.RenameColumn(
                name: "PhoneNumber",
                table: "SEG_USUARIOS",
                newName: "TX_TELEFONO");

            migrationBuilder.RenameColumn(
                name: "PasswordHash",
                table: "SEG_USUARIOS",
                newName: "TX_PASSWORD_HASH");

            migrationBuilder.RenameColumn(
                name: "NormalizedUserName",
                table: "SEG_USUARIOS",
                newName: "TX_USERNAME_NORM");

            migrationBuilder.RenameColumn(
                name: "NormalizedEmail",
                table: "SEG_USUARIOS",
                newName: "TX_EMAIL_NORM");

            migrationBuilder.RenameColumn(
                name: "LockoutEnd",
                table: "SEG_USUARIOS",
                newName: "FE_FIN_BLOQUEO");

            migrationBuilder.RenameColumn(
                name: "LockoutEnabled",
                table: "SEG_USUARIOS",
                newName: "BL_BLOQUEO_ENABLED");

            migrationBuilder.RenameColumn(
                name: "EmailConfirmed",
                table: "SEG_USUARIOS",
                newName: "BL_EMAIL_CONFIRMADO");

            migrationBuilder.RenameColumn(
                name: "Email",
                table: "SEG_USUARIOS",
                newName: "TX_EMAIL");

            migrationBuilder.RenameColumn(
                name: "ConcurrencyStamp",
                table: "SEG_USUARIOS",
                newName: "TX_CONCURRENCY_STAMP");

            migrationBuilder.RenameColumn(
                name: "AccessFailedCount",
                table: "SEG_USUARIOS",
                newName: "NU_INTENTOS_FALLIDOS");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "SEG_USUARIOS",
                newName: "TX_ID_USUARIO");

            migrationBuilder.RenameColumn(
                name: "RoleId",
                table: "SEG_USUARIO_ROLES",
                newName: "TX_ID_ROL");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "SEG_USUARIO_ROLES",
                newName: "TX_ID_USUARIO");

            migrationBuilder.RenameIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "SEG_USUARIO_ROLES",
                newName: "IX_SEG_USUARIO_ROLES_TX_ID_ROL");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "SEG_USUARIO_LOGINS",
                newName: "TX_ID_USUARIO");

            migrationBuilder.RenameColumn(
                name: "ProviderDisplayName",
                table: "SEG_USUARIO_LOGINS",
                newName: "TX_NOMBRE_PROVEEDOR");

            migrationBuilder.RenameColumn(
                name: "ProviderKey",
                table: "SEG_USUARIO_LOGINS",
                newName: "TX_LLAVE_PROVEEDOR");

            migrationBuilder.RenameColumn(
                name: "LoginProvider",
                table: "SEG_USUARIO_LOGINS",
                newName: "TX_PROVEEDOR");

            migrationBuilder.RenameIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "SEG_USUARIO_LOGINS",
                newName: "IX_SEG_USUARIO_LOGINS_TX_ID_USUARIO");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "SEG_USUARIO_CLAIMS",
                newName: "TX_ID_USUARIO");

            migrationBuilder.RenameColumn(
                name: "ClaimValue",
                table: "SEG_USUARIO_CLAIMS",
                newName: "TX_VALOR_CLAIM");

            migrationBuilder.RenameColumn(
                name: "ClaimType",
                table: "SEG_USUARIO_CLAIMS",
                newName: "TX_TIPO_CLAIM");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "SEG_USUARIO_CLAIMS",
                newName: "NU_ID_CLAIM");

            migrationBuilder.RenameIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "SEG_USUARIO_CLAIMS",
                newName: "IX_SEG_USUARIO_CLAIMS_TX_ID_USUARIO");

            migrationBuilder.RenameColumn(
                name: "NormalizedName",
                table: "SEG_ROLES",
                newName: "TX_NOMBRE_ROL_NORM");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "SEG_ROLES",
                newName: "TX_NOMBRE_ROL");

            migrationBuilder.RenameColumn(
                name: "ConcurrencyStamp",
                table: "SEG_ROLES",
                newName: "TX_CONCURRENCY_STAMP");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "SEG_ROLES",
                newName: "TX_ID_ROL");

            migrationBuilder.RenameColumn(
                name: "RoleId",
                table: "SEG_ROL_CLAIMS",
                newName: "TX_ID_ROL");

            migrationBuilder.RenameColumn(
                name: "ClaimValue",
                table: "SEG_ROL_CLAIMS",
                newName: "TX_VALOR_CLAIM");

            migrationBuilder.RenameColumn(
                name: "ClaimType",
                table: "SEG_ROL_CLAIMS",
                newName: "TX_TIPO_CLAIM");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "SEG_ROL_CLAIMS",
                newName: "NU_ID_ROL_CLAIM");

            migrationBuilder.RenameIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "SEG_ROL_CLAIMS",
                newName: "IX_SEG_ROL_CLAIMS_TX_ID_ROL");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SEG_USUARIO_TOKENS",
                table: "SEG_USUARIO_TOKENS",
                columns: new[] { "TX_ID_USUARIO", "TX_PROVEEDOR", "TX_NOMBRE_TOKEN" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_SEG_USUARIOS",
                table: "SEG_USUARIOS",
                column: "TX_ID_USUARIO");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SEG_USUARIO_ROLES",
                table: "SEG_USUARIO_ROLES",
                columns: new[] { "TX_ID_USUARIO", "TX_ID_ROL" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_SEG_USUARIO_LOGINS",
                table: "SEG_USUARIO_LOGINS",
                columns: new[] { "TX_PROVEEDOR", "TX_LLAVE_PROVEEDOR" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_SEG_USUARIO_CLAIMS",
                table: "SEG_USUARIO_CLAIMS",
                column: "NU_ID_CLAIM");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SEG_ROLES",
                table: "SEG_ROLES",
                column: "TX_ID_ROL");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SEG_ROL_CLAIMS",
                table: "SEG_ROL_CLAIMS",
                column: "NU_ID_ROL_CLAIM");

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

            migrationBuilder.AddForeignKey(
                name: "FK_SEG_ROL_CLAIMS_SEG_ROLES_TX_ID_ROL",
                table: "SEG_ROL_CLAIMS",
                column: "TX_ID_ROL",
                principalTable: "SEG_ROLES",
                principalColumn: "TX_ID_ROL",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SEG_USUARIO_CLAIMS_SEG_USUARIOS_TX_ID_USUARIO",
                table: "SEG_USUARIO_CLAIMS",
                column: "TX_ID_USUARIO",
                principalTable: "SEG_USUARIOS",
                principalColumn: "TX_ID_USUARIO",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SEG_USUARIO_LOGINS_SEG_USUARIOS_TX_ID_USUARIO",
                table: "SEG_USUARIO_LOGINS",
                column: "TX_ID_USUARIO",
                principalTable: "SEG_USUARIOS",
                principalColumn: "TX_ID_USUARIO",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SEG_USUARIO_ROLES_SEG_ROLES_TX_ID_ROL",
                table: "SEG_USUARIO_ROLES",
                column: "TX_ID_ROL",
                principalTable: "SEG_ROLES",
                principalColumn: "TX_ID_ROL",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SEG_USUARIO_ROLES_SEG_USUARIOS_TX_ID_USUARIO",
                table: "SEG_USUARIO_ROLES",
                column: "TX_ID_USUARIO",
                principalTable: "SEG_USUARIOS",
                principalColumn: "TX_ID_USUARIO",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SEG_USUARIO_TOKENS_SEG_USUARIOS_TX_ID_USUARIO",
                table: "SEG_USUARIO_TOKENS",
                column: "TX_ID_USUARIO",
                principalTable: "SEG_USUARIOS",
                principalColumn: "TX_ID_USUARIO",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SEG_ROL_CLAIMS_SEG_ROLES_TX_ID_ROL",
                table: "SEG_ROL_CLAIMS");

            migrationBuilder.DropForeignKey(
                name: "FK_SEG_USUARIO_CLAIMS_SEG_USUARIOS_TX_ID_USUARIO",
                table: "SEG_USUARIO_CLAIMS");

            migrationBuilder.DropForeignKey(
                name: "FK_SEG_USUARIO_LOGINS_SEG_USUARIOS_TX_ID_USUARIO",
                table: "SEG_USUARIO_LOGINS");

            migrationBuilder.DropForeignKey(
                name: "FK_SEG_USUARIO_ROLES_SEG_ROLES_TX_ID_ROL",
                table: "SEG_USUARIO_ROLES");

            migrationBuilder.DropForeignKey(
                name: "FK_SEG_USUARIO_ROLES_SEG_USUARIOS_TX_ID_USUARIO",
                table: "SEG_USUARIO_ROLES");

            migrationBuilder.DropForeignKey(
                name: "FK_SEG_USUARIO_TOKENS_SEG_USUARIOS_TX_ID_USUARIO",
                table: "SEG_USUARIO_TOKENS");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SEG_USUARIOS",
                table: "SEG_USUARIOS");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SEG_USUARIO_TOKENS",
                table: "SEG_USUARIO_TOKENS");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SEG_USUARIO_ROLES",
                table: "SEG_USUARIO_ROLES");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SEG_USUARIO_LOGINS",
                table: "SEG_USUARIO_LOGINS");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SEG_USUARIO_CLAIMS",
                table: "SEG_USUARIO_CLAIMS");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SEG_ROLES",
                table: "SEG_ROLES");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SEG_ROL_CLAIMS",
                table: "SEG_ROL_CLAIMS");

            migrationBuilder.RenameTable(
                name: "SEG_USUARIOS",
                newName: "AspNetUsers");

            migrationBuilder.RenameTable(
                name: "SEG_USUARIO_TOKENS",
                newName: "AspNetUserTokens");

            migrationBuilder.RenameTable(
                name: "SEG_USUARIO_ROLES",
                newName: "AspNetUserRoles");

            migrationBuilder.RenameTable(
                name: "SEG_USUARIO_LOGINS",
                newName: "AspNetUserLogins");

            migrationBuilder.RenameTable(
                name: "SEG_USUARIO_CLAIMS",
                newName: "AspNetUserClaims");

            migrationBuilder.RenameTable(
                name: "SEG_ROLES",
                newName: "AspNetRoles");

            migrationBuilder.RenameTable(
                name: "SEG_ROL_CLAIMS",
                newName: "AspNetRoleClaims");

            migrationBuilder.RenameColumn(
                name: "TX_USERNAME_NORM",
                table: "AspNetUsers",
                newName: "NormalizedUserName");

            migrationBuilder.RenameColumn(
                name: "TX_USERNAME",
                table: "AspNetUsers",
                newName: "UserName");

            migrationBuilder.RenameColumn(
                name: "TX_TELEFONO",
                table: "AspNetUsers",
                newName: "PhoneNumber");

            migrationBuilder.RenameColumn(
                name: "TX_SECURITY_STAMP",
                table: "AspNetUsers",
                newName: "SecurityStamp");

            migrationBuilder.RenameColumn(
                name: "TX_PASSWORD_HASH",
                table: "AspNetUsers",
                newName: "PasswordHash");

            migrationBuilder.RenameColumn(
                name: "TX_EMAIL_NORM",
                table: "AspNetUsers",
                newName: "NormalizedEmail");

            migrationBuilder.RenameColumn(
                name: "TX_EMAIL",
                table: "AspNetUsers",
                newName: "Email");

            migrationBuilder.RenameColumn(
                name: "TX_CONCURRENCY_STAMP",
                table: "AspNetUsers",
                newName: "ConcurrencyStamp");

            migrationBuilder.RenameColumn(
                name: "NU_INTENTOS_FALLIDOS",
                table: "AspNetUsers",
                newName: "AccessFailedCount");

            migrationBuilder.RenameColumn(
                name: "FE_FIN_BLOQUEO",
                table: "AspNetUsers",
                newName: "LockoutEnd");

            migrationBuilder.RenameColumn(
                name: "BL_TELEFONO_CONFIRMADO",
                table: "AspNetUsers",
                newName: "PhoneNumberConfirmed");

            migrationBuilder.RenameColumn(
                name: "BL_EMAIL_CONFIRMADO",
                table: "AspNetUsers",
                newName: "EmailConfirmed");

            migrationBuilder.RenameColumn(
                name: "BL_BLOQUEO_ENABLED",
                table: "AspNetUsers",
                newName: "LockoutEnabled");

            migrationBuilder.RenameColumn(
                name: "BL_2FA_ENABLED",
                table: "AspNetUsers",
                newName: "TwoFactorEnabled");

            migrationBuilder.RenameColumn(
                name: "TX_ID_USUARIO",
                table: "AspNetUsers",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "TX_VALOR_TOKEN",
                table: "AspNetUserTokens",
                newName: "Value");

            migrationBuilder.RenameColumn(
                name: "TX_NOMBRE_TOKEN",
                table: "AspNetUserTokens",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "TX_PROVEEDOR",
                table: "AspNetUserTokens",
                newName: "LoginProvider");

            migrationBuilder.RenameColumn(
                name: "TX_ID_USUARIO",
                table: "AspNetUserTokens",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "TX_ID_ROL",
                table: "AspNetUserRoles",
                newName: "RoleId");

            migrationBuilder.RenameColumn(
                name: "TX_ID_USUARIO",
                table: "AspNetUserRoles",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_SEG_USUARIO_ROLES_TX_ID_ROL",
                table: "AspNetUserRoles",
                newName: "IX_AspNetUserRoles_RoleId");

            migrationBuilder.RenameColumn(
                name: "TX_NOMBRE_PROVEEDOR",
                table: "AspNetUserLogins",
                newName: "ProviderDisplayName");

            migrationBuilder.RenameColumn(
                name: "TX_ID_USUARIO",
                table: "AspNetUserLogins",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "TX_LLAVE_PROVEEDOR",
                table: "AspNetUserLogins",
                newName: "ProviderKey");

            migrationBuilder.RenameColumn(
                name: "TX_PROVEEDOR",
                table: "AspNetUserLogins",
                newName: "LoginProvider");

            migrationBuilder.RenameIndex(
                name: "IX_SEG_USUARIO_LOGINS_TX_ID_USUARIO",
                table: "AspNetUserLogins",
                newName: "IX_AspNetUserLogins_UserId");

            migrationBuilder.RenameColumn(
                name: "TX_VALOR_CLAIM",
                table: "AspNetUserClaims",
                newName: "ClaimValue");

            migrationBuilder.RenameColumn(
                name: "TX_TIPO_CLAIM",
                table: "AspNetUserClaims",
                newName: "ClaimType");

            migrationBuilder.RenameColumn(
                name: "TX_ID_USUARIO",
                table: "AspNetUserClaims",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "NU_ID_CLAIM",
                table: "AspNetUserClaims",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_SEG_USUARIO_CLAIMS_TX_ID_USUARIO",
                table: "AspNetUserClaims",
                newName: "IX_AspNetUserClaims_UserId");

            migrationBuilder.RenameColumn(
                name: "TX_NOMBRE_ROL_NORM",
                table: "AspNetRoles",
                newName: "NormalizedName");

            migrationBuilder.RenameColumn(
                name: "TX_NOMBRE_ROL",
                table: "AspNetRoles",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "TX_CONCURRENCY_STAMP",
                table: "AspNetRoles",
                newName: "ConcurrencyStamp");

            migrationBuilder.RenameColumn(
                name: "TX_ID_ROL",
                table: "AspNetRoles",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "TX_VALOR_CLAIM",
                table: "AspNetRoleClaims",
                newName: "ClaimValue");

            migrationBuilder.RenameColumn(
                name: "TX_TIPO_CLAIM",
                table: "AspNetRoleClaims",
                newName: "ClaimType");

            migrationBuilder.RenameColumn(
                name: "TX_ID_ROL",
                table: "AspNetRoleClaims",
                newName: "RoleId");

            migrationBuilder.RenameColumn(
                name: "NU_ID_ROL_CLAIM",
                table: "AspNetRoleClaims",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_SEG_ROL_CLAIMS_TX_ID_ROL",
                table: "AspNetRoleClaims",
                newName: "IX_AspNetRoleClaims_RoleId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AspNetUsers",
                table: "AspNetUsers",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AspNetUserTokens",
                table: "AspNetUserTokens",
                columns: new[] { "UserId", "LoginProvider", "Name" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_AspNetUserRoles",
                table: "AspNetUserRoles",
                columns: new[] { "UserId", "RoleId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_AspNetUserLogins",
                table: "AspNetUserLogins",
                columns: new[] { "LoginProvider", "ProviderKey" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_AspNetUserClaims",
                table: "AspNetUserClaims",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AspNetRoles",
                table: "AspNetRoles",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AspNetRoleClaims",
                table: "AspNetRoleClaims",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "TIPO_SEGUROS",
                keyColumn: "NU_ID_TIPO_SEGURO",
                keyValue: 1,
                column: "FE_REG",
                value: new DateTime(2025, 12, 3, 16, 32, 59, 689, DateTimeKind.Utc).AddTicks(4955));

            migrationBuilder.UpdateData(
                table: "TIPO_SEGUROS",
                keyColumn: "NU_ID_TIPO_SEGURO",
                keyValue: 2,
                column: "FE_REG",
                value: new DateTime(2025, 12, 3, 16, 32, 59, 689, DateTimeKind.Utc).AddTicks(5172));

            migrationBuilder.UpdateData(
                table: "TIPO_SEGUROS",
                keyColumn: "NU_ID_TIPO_SEGURO",
                keyValue: 3,
                column: "FE_REG",
                value: new DateTime(2025, 12, 3, 16, 32, 59, 689, DateTimeKind.Utc).AddTicks(5175));

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId",
                principalTable: "AspNetRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                table: "AspNetUserClaims",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                table: "AspNetUserLogins",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId",
                principalTable: "AspNetRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                table: "AspNetUserRoles",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                table: "AspNetUserTokens",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
