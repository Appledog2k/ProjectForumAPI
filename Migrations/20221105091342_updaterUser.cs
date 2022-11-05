using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Articles.Migrations
{
    public partial class updaterUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "53c09174-d091-412e-bc38-094432ff6fbb");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f766cba4-daae-4ca1-a564-63edf70e7a42");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "c685addd-459f-4f2e-966b-d60efa751d85", "ede1b880-b28c-4e6e-b385-ca706f014df4", "User", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "f7576ab4-7404-4fdf-9d5e-dd3c0f35d17a", "5fd9332c-e94e-4f5a-94da-d0e6d4e6a8f4", "Admin", "ADMIN" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c685addd-459f-4f2e-966b-d60efa751d85");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f7576ab4-7404-4fdf-9d5e-dd3c0f35d17a");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "53c09174-d091-412e-bc38-094432ff6fbb", "231d2b60-96d5-499a-b24e-e387b5547ce6", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "f766cba4-daae-4ca1-a564-63edf70e7a42", "a2915518-f68f-4ffc-8244-fcf5d8f53264", "User", "USER" });
        }
    }
}
