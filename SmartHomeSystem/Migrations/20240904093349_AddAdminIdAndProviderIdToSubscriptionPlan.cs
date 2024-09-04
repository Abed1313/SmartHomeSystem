using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SmartHomeSystem.Migrations
{
    /// <inheritdoc />
    public partial class AddAdminIdAndProviderIdToSubscriptionPlan : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SubscriptionPlans_Admins_AdminId",
                table: "SubscriptionPlans");

            migrationBuilder.DropForeignKey(
                name: "FK_SubscriptionPlans_Providers_ProviderId",
                table: "SubscriptionPlans");

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: -1930314522);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: -1860207793);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: -1297039453);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: -508425391);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: -458929482);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 257140646);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 1314906135);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 1670653170);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 2070319292);

            migrationBuilder.AlterColumn<string>(
                name: "ProviderId",
                table: "SubscriptionPlans",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "AdminId",
                table: "SubscriptionPlans",
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
                name: "FK_SubscriptionPlans_Admins_AdminId",
                table: "SubscriptionPlans",
                column: "AdminId",
                principalTable: "Admins",
                principalColumn: "AdminId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SubscriptionPlans_Providers_ProviderId",
                table: "SubscriptionPlans",
                column: "ProviderId",
                principalTable: "Providers",
                principalColumn: "ProviderId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SubscriptionPlans_Admins_AdminId",
                table: "SubscriptionPlans");

            migrationBuilder.DropForeignKey(
                name: "FK_SubscriptionPlans_Providers_ProviderId",
                table: "SubscriptionPlans");

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
                table: "SubscriptionPlans",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "AdminId",
                table: "SubscriptionPlans",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.InsertData(
                table: "AspNetRoleClaims",
                columns: new[] { "Id", "ClaimType", "ClaimValue", "RoleId" },
                values: new object[,]
                {
                    { -1930314522, "permission", "delete", "admin" },
                    { -1860207793, "permission", "read", "admin" },
                    { -1297039453, "permission", "create", "provider" },
                    { -508425391, "permission", "read", "provider" },
                    { -458929482, "permission", "read", "guest" },
                    { 257140646, "permission", "update", "provider" },
                    { 1314906135, "permission", "update", "admin" },
                    { 1670653170, "permission", "delete", "provider" },
                    { 2070319292, "permission", "create", "admin" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_SubscriptionPlans_Admins_AdminId",
                table: "SubscriptionPlans",
                column: "AdminId",
                principalTable: "Admins",
                principalColumn: "AdminId");

            migrationBuilder.AddForeignKey(
                name: "FK_SubscriptionPlans_Providers_ProviderId",
                table: "SubscriptionPlans",
                column: "ProviderId",
                principalTable: "Providers",
                principalColumn: "ProviderId");
        }
    }
}
