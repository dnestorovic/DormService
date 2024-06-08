using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace IdentiryServer.Migrations
{
    /// <inheritdoc />
    public partial class AddRolesToDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8083ab2c-0e2e-4492-8c7a-4db1e87a3244");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d5026050-6910-45b3-bc34-e2e9907c8ef3");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "7cc463f0-3b63-4957-8e17-b14cdc48cf8e", null, "Student", "STUDENT" },
                    { "e4533490-df2e-4315-be6f-4eb789877a4d", null, "Administator", "ADMINISTRATOR" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7cc463f0-3b63-4957-8e17-b14cdc48cf8e");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e4533490-df2e-4315-be6f-4eb789877a4d");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "8083ab2c-0e2e-4492-8c7a-4db1e87a3244", null, "Administator", "ADMIONISTRATOR" },
                    { "d5026050-6910-45b3-bc34-e2e9907c8ef3", null, "Student", "STUDENT" }
                });
        }
    }
}
