using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Library_Management_System.Migrations
{
    /// <inheritdoc />
    public partial class SecondMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "434ad7f5-3290-4ceb-9c98-fac210ad7aec");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "69a8271b-2e2e-4144-ba28-9dc5e9a09677");

            migrationBuilder.RenameColumn(
                name: "Message",
                table: "LibarianMessages",
                newName: "UserMessageContent");

            migrationBuilder.AddColumn<string>(
                name: "LibrarianMessageContent",
                table: "LibarianMessages",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "665289d0-19d7-4556-8757-82a1c05d780e", null, "Libarian", "LIBARIAN" },
                    { "eada6166-4f12-4cca-ad62-bfa5c37db607", null, "User", "USER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "665289d0-19d7-4556-8757-82a1c05d780e");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "eada6166-4f12-4cca-ad62-bfa5c37db607");

            migrationBuilder.DropColumn(
                name: "LibrarianMessageContent",
                table: "LibarianMessages");

            migrationBuilder.RenameColumn(
                name: "UserMessageContent",
                table: "LibarianMessages",
                newName: "Message");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "434ad7f5-3290-4ceb-9c98-fac210ad7aec", null, "User", "USER" },
                    { "69a8271b-2e2e-4144-ba28-9dc5e9a09677", null, "Libarian", "LIBARIAN" }
                });
        }
    }
}
