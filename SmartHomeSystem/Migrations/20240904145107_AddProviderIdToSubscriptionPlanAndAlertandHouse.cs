using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SmartHomeSystem.Migrations
{
    /// <inheritdoc />
    public partial class AddProviderIdToSubscriptionPlanAndAlertandHouse : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Alerts_Providers_ProviderId",
                table: "Alerts");

            migrationBuilder.DropForeignKey(
                name: "FK_Devices_Providers_ProviderId",
                table: "Devices");

            migrationBuilder.DropForeignKey(
                name: "FK_EnergyUsages_Providers_ProviderId",
                table: "EnergyUsages");

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: -2137099298);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: -1807378741);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: -1505998498);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: -1013563335);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: -613840018);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: -469015573);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 818981057);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 2049108345);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 2091536934);

            migrationBuilder.AlterColumn<string>(
                name: "ProviderId",
                table: "Houses",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ProviderId",
                table: "EnergyUsages",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ProviderId",
                table: "Devices",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoleClaims",
                columns: new[] { "Id", "ClaimType", "ClaimValue", "RoleId" },
                values: new object[,]
                {
                    { -1609327333, "permission", "delete", "admin" },
                    { -1550551286, "permission", "create", "admin" },
                    { -1342467187, "permission", "update", "admin" },
                    { 350177333, "permission", "read", "guest" },
                    { 403808466, "permission", "create", "provider" },
                    { 966144751, "permission", "read", "admin" },
                    { 1001959955, "permission", "delete", "provider" },
                    { 1199700905, "permission", "read", "provider" },
                    { 1736278602, "permission", "update", "provider" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Alerts_Providers_ProviderId",
                table: "Alerts",
                column: "ProviderId",
                principalTable: "Providers",
                principalColumn: "ProviderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Devices_Providers_ProviderId",
                table: "Devices",
                column: "ProviderId",
                principalTable: "Providers",
                principalColumn: "ProviderId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EnergyUsages_Providers_ProviderId",
                table: "EnergyUsages",
                column: "ProviderId",
                principalTable: "Providers",
                principalColumn: "ProviderId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Alerts_Providers_ProviderId",
                table: "Alerts");

            migrationBuilder.DropForeignKey(
                name: "FK_Devices_Providers_ProviderId",
                table: "Devices");

            migrationBuilder.DropForeignKey(
                name: "FK_EnergyUsages_Providers_ProviderId",
                table: "EnergyUsages");

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: -1609327333);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: -1550551286);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: -1342467187);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 350177333);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 403808466);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 966144751);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 1001959955);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 1199700905);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 1736278602);

            migrationBuilder.AlterColumn<string>(
                name: "ProviderId",
                table: "Houses",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "ProviderId",
                table: "EnergyUsages",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "ProviderId",
                table: "Devices",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.InsertData(
                table: "AspNetRoleClaims",
                columns: new[] { "Id", "ClaimType", "ClaimValue", "RoleId" },
                values: new object[,]
                {
                    { -2137099298, "permission", "read", "guest" },
                    { -1807378741, "permission", "read", "admin" },
                    { -1505998498, "permission", "delete", "admin" },
                    { -1013563335, "permission", "update", "admin" },
                    { -613840018, "permission", "create", "admin" },
                    { -469015573, "permission", "read", "provider" },
                    { 818981057, "permission", "update", "provider" },
                    { 2049108345, "permission", "delete", "provider" },
                    { 2091536934, "permission", "create", "provider" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Alerts_Providers_ProviderId",
                table: "Alerts",
                column: "ProviderId",
                principalTable: "Providers",
                principalColumn: "ProviderId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Devices_Providers_ProviderId",
                table: "Devices",
                column: "ProviderId",
                principalTable: "Providers",
                principalColumn: "ProviderId");

            migrationBuilder.AddForeignKey(
                name: "FK_EnergyUsages_Providers_ProviderId",
                table: "EnergyUsages",
                column: "ProviderId",
                principalTable: "Providers",
                principalColumn: "ProviderId");
        }
    }
}
