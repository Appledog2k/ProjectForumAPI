using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Articles.Migrations
{
    public partial class updaterUser1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c685addd-459f-4f2e-966b-d60efa751d85");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f7576ab4-7404-4fdf-9d5e-dd3c0f35d17a");

            migrationBuilder.AddColumn<string>(
                name: "Avatar",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "b679d70e-99be-4aa7-8341-5c5b95053354", "3e862313-1c5c-4b38-a1a6-3b1c369bb905", "User", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "e75a0c8f-7666-4a85-a0cb-5f392326f19c", "a8f6db61-0f98-4978-bc3a-3103e8e5726a", "Admin", "ADMIN" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b679d70e-99be-4aa7-8341-5c5b95053354");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e75a0c8f-7666-4a85-a0cb-5f392326f19c");

            migrationBuilder.DropColumn(
                name: "Avatar",
                table: "AspNetUsers");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "c685addd-459f-4f2e-966b-d60efa751d85", "ede1b880-b28c-4e6e-b385-ca706f014df4", "User", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "f7576ab4-7404-4fdf-9d5e-dd3c0f35d17a", "5fd9332c-e94e-4f5a-94da-d0e6d4e6a8f4", "Admin", "ADMIN" });
        }
    }
}
