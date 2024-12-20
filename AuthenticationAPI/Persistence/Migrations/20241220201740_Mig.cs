using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AuthenticationAPI.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Mig : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "TEXT", maxLength: 150, nullable: false),
                    PasswordHash = table.Column<byte[]>(type: "BLOB", nullable: false),
                    PasswordSalt = table.Column<byte[]>(type: "BLOB", nullable: false),
                    Role = table.Column<int>(type: "INTEGER", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RefreshTokens",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserId = table.Column<int>(type: "INTEGER", nullable: false),
                    Token = table.Column<string>(type: "TEXT", nullable: false),
                    ExpiryDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    RevokedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    CreatedaAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefreshTokens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RefreshTokens_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "Name", "PasswordHash", "PasswordSalt", "Role" },
                values: new object[] { 1, "admin@admin.com", "Admin", new byte[] { 120, 44, 74, 2, 21, 231, 179, 21, 157, 246, 206, 184, 42, 212, 125, 89, 168, 174, 77, 186, 87, 47, 247, 206, 147, 207, 2, 136, 250, 85, 211, 131, 181, 243, 164, 41, 233, 227, 108, 174, 217, 201, 141, 158, 70, 216, 170, 39, 106, 242, 134, 253, 241, 205, 12, 200, 187, 29, 145, 127, 244, 68, 178, 189 }, new byte[] { 12, 131, 51, 131, 120, 95, 244, 185, 159, 95, 164, 255, 100, 141, 232, 6, 186, 109, 116, 189, 14, 155, 237, 183, 98, 15, 93, 228, 241, 205, 229, 121, 80, 145, 126, 237, 170, 205, 167, 112, 69, 219, 116, 168, 227, 240, 188, 229, 219, 122, 26, 106, 25, 83, 141, 166, 180, 198, 94, 29, 16, 42, 51, 90, 19, 241, 122, 183, 48, 42, 254, 36, 58, 119, 113, 2, 18, 25, 111, 220, 235, 224, 35, 63, 213, 1, 35, 172, 6, 224, 194, 137, 196, 180, 8, 1, 119, 119, 83, 124, 139, 35, 250, 254, 101, 52, 186, 242, 190, 241, 241, 159, 86, 189, 7, 197, 80, 90, 192, 89, 8, 145, 26, 62, 99, 175, 162, 89 }, 1 });

            migrationBuilder.CreateIndex(
                name: "IX_RefreshTokens_Token",
                table: "RefreshTokens",
                column: "Token",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RefreshTokens_UserId",
                table: "RefreshTokens",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RefreshTokens");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
