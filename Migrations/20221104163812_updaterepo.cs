using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Articles.Migrations
{
    public partial class updaterepo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "27e635fc-bc80-4acf-a935-1e8a43c7e1bd");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "75f61ee6-994a-4452-9b66-89f5cf4a0321");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "843d9a89-23dd-4425-b8cf-4f7b0b10bda0", "fe96d829-ed02-45d2-ade7-54f59c01e52f", "User", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "c06a9b8c-8978-4c1d-8b0a-cd3f7c0074d3", "de656551-1e08-443e-bb40-e44096deac80", "Admin", "ADMIN" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "843d9a89-23dd-4425-b8cf-4f7b0b10bda0");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c06a9b8c-8978-4c1d-8b0a-cd3f7c0074d3");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "27e635fc-bc80-4acf-a935-1e8a43c7e1bd", "9f56e8f4-413d-4fb6-8996-8cccc7cccdcd", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "75f61ee6-994a-4452-9b66-89f5cf4a0321", "7b266e28-7007-41ab-9263-91a7748db0be", "User", "USER" });
        }
    }
}
