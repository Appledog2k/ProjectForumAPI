using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Articles.Migrations
{
    public partial class userrole : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f5b151f3-08ad-49df-ba56-2909af9a04b5");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fdebcc40-a761-4a01-9866-38fac20c0fe3");

            migrationBuilder.AddColumn<bool>(
                name: "isAdmin",
                table: "AspNetUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Articles",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "9fbb7755-fe70-4cb4-adbf-04e68cd29909", "d828ae27-016f-4019-85b4-c9058914fd2d", "User", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "d421c972-4b2a-4495-8e39-2c41d534a24e", "98b8b822-0d2d-4d7e-ac9c-252ebfbfa4d9", "Admin", "ADMIN" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9fbb7755-fe70-4cb4-adbf-04e68cd29909");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d421c972-4b2a-4495-8e39-2c41d534a24e");

            migrationBuilder.DropColumn(
                name: "isAdmin",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Articles");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "f5b151f3-08ad-49df-ba56-2909af9a04b5", "09c413b9-3226-4652-84c8-f27ba58e2c5d", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "fdebcc40-a761-4a01-9866-38fac20c0fe3", "aa38c7fe-b769-404b-a89b-6420ed4713d3", "User", "USER" });
        }
    }
}
