using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SmartHomeSystem.Migrations
{
    /// <inheritdoc />
    public partial class SeedDataForSomeEntitis : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: -1548591340);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: -172807151);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 596000483);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 674821090);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 1109820908);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 1348354837);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 1528332082);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 1670665213);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 1909978148);

            migrationBuilder.InsertData(
                table: "ActionSeverities",
                columns: new[] { "ActionSeverityId", "Description", "Name" },
                values: new object[,]
                {
                    { 1, "Informational alerts that do not require immediate attention.", "Info" },
                    { 2, "Alerts that require attention but are not critical.", "Warning" },
                    { 3, "Critical alerts that require immediate attention.", "Critical" }
                });

            migrationBuilder.InsertData(
                table: "ActionTypes",
                columns: new[] { "ActionTypeId", "Name", "Parameters" },
                values: new object[,]
                {
                    { 1, "TurnOn", "None" },
                    { 2, "TurnOff", "None" },
                    { 3, "SetTemperature", "75°F" },
                    { 4, "SetBrightness", "50% Brightness" }
                });

            migrationBuilder.InsertData(
                table: "AspNetRoleClaims",
                columns: new[] { "Id", "ClaimType", "ClaimValue", "RoleId" },
                values: new object[,]
                {
                    { -1868490446, "permission", "read", "provider" },
                    { -1542704331, "permission", "update", "provider" },
                    { -1094618176, "permission", "read", "admin" },
                    { -1037113363, "permission", "create", "admin" },
                    { -707345414, "permission", "delete", "admin" },
                    { -135204508, "permission", "update", "admin" },
                    { 929098631, "permission", "delete", "provider" },
                    { 1311995422, "permission", "read", "guest" },
                    { 1715612253, "permission", "create", "provider" }
                });

            migrationBuilder.InsertData(
                table: "DeviceTypes",
                columns: new[] { "DeviceTypeId", "Description", "Name" },
                values: new object[,]
                {
                    { 1, "A smart light bulb that can be controlled remotely.", "Light" },
                    { 2, "A smart thermostat that controls the temperature of the house.", "Thermostat" },
                    { 3, "A camera that provides remote monitoring of the house.", "Security Camera" },
                    { 4, "A smart plug that can be controlled to turn on or off connected devices.", "Smart Plug" }
                });

            migrationBuilder.InsertData(
                table: "RoomTypes",
                columns: new[] { "RoomTypeId", "Description", "Name" },
                values: new object[,]
                {
                    { 1, "A room in a house for general and informal everyday use.", "Living Room" },
                    { 2, "A room where food is prepared and cooked.", "Kitchen" },
                    { 3, "A room used for sleeping.", "Bedroom" },
                    { 4, "A room containing a bathtub or shower, and usually a toilet.", "Bathroom" },
                    { 5, "A room for housing a vehicle or storage.", "Garage" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ActionSeverities",
                keyColumn: "ActionSeverityId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ActionSeverities",
                keyColumn: "ActionSeverityId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "ActionSeverities",
                keyColumn: "ActionSeverityId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "ActionTypes",
                keyColumn: "ActionTypeId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ActionTypes",
                keyColumn: "ActionTypeId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "ActionTypes",
                keyColumn: "ActionTypeId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "ActionTypes",
                keyColumn: "ActionTypeId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: -1868490446);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: -1542704331);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: -1094618176);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: -1037113363);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: -707345414);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: -135204508);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 929098631);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 1311995422);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 1715612253);

            migrationBuilder.DeleteData(
                table: "DeviceTypes",
                keyColumn: "DeviceTypeId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "DeviceTypes",
                keyColumn: "DeviceTypeId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "DeviceTypes",
                keyColumn: "DeviceTypeId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "DeviceTypes",
                keyColumn: "DeviceTypeId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "RoomTypes",
                keyColumn: "RoomTypeId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "RoomTypes",
                keyColumn: "RoomTypeId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "RoomTypes",
                keyColumn: "RoomTypeId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "RoomTypes",
                keyColumn: "RoomTypeId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "RoomTypes",
                keyColumn: "RoomTypeId",
                keyValue: 5);

            migrationBuilder.InsertData(
                table: "AspNetRoleClaims",
                columns: new[] { "Id", "ClaimType", "ClaimValue", "RoleId" },
                values: new object[,]
                {
                    { -1548591340, "permission", "update", "admin" },
                    { -172807151, "permission", "create", "provider" },
                    { 596000483, "permission", "read", "guest" },
                    { 674821090, "permission", "delete", "provider" },
                    { 1109820908, "permission", "update", "provider" },
                    { 1348354837, "permission", "read", "provider" },
                    { 1528332082, "permission", "delete", "admin" },
                    { 1670665213, "permission", "read", "admin" },
                    { 1909978148, "permission", "create", "admin" }
                });
        }
    }
}
