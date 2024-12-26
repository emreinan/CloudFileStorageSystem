using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AuthenticationAPI.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Mig1 : Migration
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
                values: new object[,]
                {
                    { 1, "admin@admin.com", "Admin", new byte[] { 126, 42, 52, 32, 67, 40, 151, 113, 191, 249, 249, 4, 228, 159, 214, 175, 92, 47, 191, 22, 70, 243, 153, 19, 242, 31, 57, 116, 10, 130, 174, 104, 127, 23, 205, 199, 193, 210, 142, 120, 139, 83, 41, 126, 85, 139, 45, 189, 128, 60, 158, 24, 42, 29, 180, 106, 77, 228, 31, 165, 129, 7, 210, 191 }, new byte[] { 136, 29, 207, 181, 220, 101, 216, 15, 193, 178, 245, 19, 199, 57, 248, 158, 228, 180, 203, 213, 196, 21, 239, 111, 195, 66, 19, 169, 216, 113, 88, 217, 147, 65, 22, 176, 38, 160, 196, 109, 23, 164, 197, 90, 162, 164, 21, 32, 203, 238, 243, 232, 36, 78, 148, 28, 99, 101, 178, 132, 6, 113, 106, 81, 155, 95, 62, 217, 8, 9, 79, 214, 218, 12, 51, 147, 174, 113, 127, 250, 21, 5, 222, 181, 24, 134, 91, 130, 51, 140, 120, 43, 57, 215, 33, 64, 105, 162, 173, 112, 177, 215, 9, 1, 3, 235, 240, 4, 117, 2, 21, 212, 0, 142, 86, 44, 33, 134, 23, 205, 82, 37, 82, 52, 194, 141, 99, 39 }, 1 },
                    { 2, "user@user.com", "User", new byte[] { 126, 42, 52, 32, 67, 40, 151, 113, 191, 249, 249, 4, 228, 159, 214, 175, 92, 47, 191, 22, 70, 243, 153, 19, 242, 31, 57, 116, 10, 130, 174, 104, 127, 23, 205, 199, 193, 210, 142, 120, 139, 83, 41, 126, 85, 139, 45, 189, 128, 60, 158, 24, 42, 29, 180, 106, 77, 228, 31, 165, 129, 7, 210, 191 }, new byte[] { 136, 29, 207, 181, 220, 101, 216, 15, 193, 178, 245, 19, 199, 57, 248, 158, 228, 180, 203, 213, 196, 21, 239, 111, 195, 66, 19, 169, 216, 113, 88, 217, 147, 65, 22, 176, 38, 160, 196, 109, 23, 164, 197, 90, 162, 164, 21, 32, 203, 238, 243, 232, 36, 78, 148, 28, 99, 101, 178, 132, 6, 113, 106, 81, 155, 95, 62, 217, 8, 9, 79, 214, 218, 12, 51, 147, 174, 113, 127, 250, 21, 5, 222, 181, 24, 134, 91, 130, 51, 140, 120, 43, 57, 215, 33, 64, 105, 162, 173, 112, 177, 215, 9, 1, 3, 235, 240, 4, 117, 2, 21, 212, 0, 142, 86, 44, 33, 134, 23, 205, 82, 37, 82, 52, 194, 141, 99, 39 }, 2 }
                });

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
