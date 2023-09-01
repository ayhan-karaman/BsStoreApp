using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebApiUI.Migrations
{
    /// <inheritdoc />
    public partial class CreateRelationBetweenBookAndCategoryOneToMany : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "38de5385-632c-4db3-a512-7d201aea232f");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "849805f2-a2fd-496c-9846-da87f986ac7b");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d81daf33-8f2b-4a77-b5f1-cc31bdf451c5");

            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "Books",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "0c55e719-f809-483a-b638-ab404d246d03", null, "Admin", "ADMIN" },
                    { "899b53d7-43e8-4bd1-9761-55658003e3dd", null, "User", "USER" },
                    { "bd4d16ed-f0e8-4ec6-9d5f-38044799f903", null, "Editor", "EDITOR" }
                });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 1,
                column: "CategoryId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 2,
                column: "CategoryId",
                value: 2);

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 3,
                column: "CategoryId",
                value: 1);

            migrationBuilder.CreateIndex(
                name: "IX_Books_CategoryId",
                table: "Books",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Categories_CategoryId",
                table: "Books",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_Categories_CategoryId",
                table: "Books");

            migrationBuilder.DropIndex(
                name: "IX_Books_CategoryId",
                table: "Books");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0c55e719-f809-483a-b638-ab404d246d03");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "899b53d7-43e8-4bd1-9761-55658003e3dd");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "bd4d16ed-f0e8-4ec6-9d5f-38044799f903");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "Books");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "38de5385-632c-4db3-a512-7d201aea232f", null, "User", "USER" },
                    { "849805f2-a2fd-496c-9846-da87f986ac7b", null, "Editor", "EDITOR" },
                    { "d81daf33-8f2b-4a77-b5f1-cc31bdf451c5", null, "Admin", "ADMIN" }
                });
        }
    }
}
