using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Articles.Migrations
{
    public partial class addDefaultRole : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "8196ca81-f7d4-4086-a6c2-d1cd3798a0b4", "5f8034d6-8d06-46bc-b322-d3551d3dac24", "User", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "edd72114-6e11-4b1f-8756-3b4fdefcabe2", "a02eca53-a631-45ec-a8b0-12dca4bc500b", "Admin", "ADMIN" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8196ca81-f7d4-4086-a6c2-d1cd3798a0b4");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "edd72114-6e11-4b1f-8756-3b4fdefcabe2");
        }
    }
}
