using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebApiUI.Migrations
{
    /// <inheritdoc />
    public partial class AddRolesToDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "81e72194-b2bf-45e9-816b-bfb6406b988c", null, "Admin", "ADMIN" },
                    { "aeab140f-140b-4666-87c7-57af7340a927", null, "Editor", "EDITOR" },
                    { "f6a8c0b3-287a-45e6-98f7-2db4232ce001", null, "User", "USER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "81e72194-b2bf-45e9-816b-bfb6406b988c");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "aeab140f-140b-4666-87c7-57af7340a927");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f6a8c0b3-287a-45e6-98f7-2db4232ce001");
        }
    }
}
