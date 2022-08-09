using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Articles.Migrations
{
    public partial class newproject : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8196ca81-f7d4-4086-a6c2-d1cd3798a0b4");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "edd72114-6e11-4b1f-8756-3b4fdefcabe2");

            migrationBuilder.UpdateData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Content", "Title" },
                values: new object[] { "Nội dung bài viết 1", "Đây là bài viết 1" });

            migrationBuilder.UpdateData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: 2,
                column: "Title",
                value: "Đây là bài viết 2");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "074b27cf-f69d-40ec-8b9f-c84d91a93a85", "3f0dee7e-1baa-4142-9451-5b36d0021f21", "User", "USER" },
                    { "7f14ebf9-8114-4fc7-acc0-d40b7a27b254", "feb40cb7-3890-4ce4-a480-995ebfd7948a", "Admin", "ADMIN" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "074b27cf-f69d-40ec-8b9f-c84d91a93a85");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7f14ebf9-8114-4fc7-acc0-d40b7a27b254");

            migrationBuilder.UpdateData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Content", "Title" },
                values: new object[] { "Content of article 1", "Article 1" });

            migrationBuilder.UpdateData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: 2,
                column: "Title",
                value: "Article 2");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "8196ca81-f7d4-4086-a6c2-d1cd3798a0b4", "5f8034d6-8d06-46bc-b322-d3551d3dac24", "User", "USER" },
                    { "edd72114-6e11-4b1f-8756-3b4fdefcabe2", "a02eca53-a631-45ec-a8b0-12dca4bc500b", "Admin", "ADMIN" }
                });
        }
    }
}
