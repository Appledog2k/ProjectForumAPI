using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Articles.Migrations
{
    public partial class updaterepoimage1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "103ed533-17f5-4214-8aa6-48454d497196");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "57b7cfdd-f477-44a2-8715-26e7c2a11330");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "53c09174-d091-412e-bc38-094432ff6fbb", "231d2b60-96d5-499a-b24e-e387b5547ce6", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "f766cba4-daae-4ca1-a564-63edf70e7a42", "a2915518-f68f-4ffc-8244-fcf5d8f53264", "User", "USER" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
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
                values: new object[] { "103ed533-17f5-4214-8aa6-48454d497196", "194f30e6-95fd-42f3-869d-2c279b492efc", "User", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "57b7cfdd-f477-44a2-8715-26e7c2a11330", "158c6767-63b5-47aa-ab3a-39638de062ba", "Admin", "ADMIN" });
        }
    }
}
