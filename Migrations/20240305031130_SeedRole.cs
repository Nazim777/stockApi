using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace stockApi.Migrations
{
    public partial class SeedRole : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "7623c8be-c8f8-4f6c-8389-0aebea7a6ac8", "8552bbeb-3fc6-41f8-86c9-30d96a4e5f11", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "870db178-9079-4bf9-8a33-9d3d30623527", "9ee1ed2d-9d53-4164-a264-db414eb93999", "User", "USER" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7623c8be-c8f8-4f6c-8389-0aebea7a6ac8");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "870db178-9079-4bf9-8a33-9d3d30623527");
        }
    }
}
