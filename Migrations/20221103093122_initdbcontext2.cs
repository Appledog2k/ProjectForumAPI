using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Articles.Migrations
{
    public partial class initdbcontext2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0839b3c2-7c9d-4cae-8cd8-780ac5506727");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8905781e-1a8b-4774-a655-6587ae950cd3");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "7f8044ac-a99e-44b2-9911-c122892aeb29", "f6b49ab3-e571-4511-ad50-147342908a59", "User", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "e4f9437e-31d8-4fab-8aa3-0b2d38e8faec", "fbffbc18-f52f-44a7-b24e-2cb5cecddb1a", "Admin", "ADMIN" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7f8044ac-a99e-44b2-9911-c122892aeb29");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e4f9437e-31d8-4fab-8aa3-0b2d38e8faec");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "0839b3c2-7c9d-4cae-8cd8-780ac5506727", "905b52b1-2dc7-4aab-8b45-7c4a76e95a8a", "User", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "8905781e-1a8b-4774-a655-6587ae950cd3", "030a35c3-42c1-4e60-8631-d8bedbc29acd", "Admin", "ADMIN" });
        }
    }
}
