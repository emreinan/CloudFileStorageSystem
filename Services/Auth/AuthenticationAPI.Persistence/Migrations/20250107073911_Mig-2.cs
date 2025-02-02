﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AuthenticationAPI.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Mig2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 36, 148, 133, 233, 214, 153, 237, 14, 49, 191, 45, 157, 151, 180, 18, 82, 100, 255, 117, 30, 227, 151, 118, 139, 141, 78, 23, 70, 110, 254, 137, 116, 188, 12, 251, 68, 136, 205, 62, 184, 14, 238, 70, 203, 50, 65, 128, 194, 223, 68, 236, 49, 139, 147, 142, 28, 229, 182, 5, 68, 141, 63, 185, 19 }, new byte[] { 176, 238, 208, 121, 239, 111, 44, 13, 91, 71, 120, 239, 111, 140, 186, 38, 151, 39, 200, 140, 23, 180, 132, 188, 150, 245, 121, 40, 93, 253, 228, 94, 185, 223, 251, 222, 166, 51, 3, 231, 86, 45, 9, 82, 65, 160, 160, 16, 114, 34, 227, 221, 143, 156, 169, 251, 239, 68, 186, 164, 38, 30, 94, 26, 185, 238, 161, 54, 71, 186, 219, 251, 76, 117, 226, 4, 76, 64, 165, 186, 177, 246, 146, 202, 51, 18, 171, 67, 130, 136, 90, 221, 227, 203, 245, 44, 26, 240, 4, 153, 84, 31, 104, 118, 214, 219, 112, 70, 89, 124, 91, 43, 76, 50, 39, 11, 111, 43, 4, 202, 181, 38, 129, 126, 22, 17, 113, 19 } });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Email", "Name", "PasswordHash", "PasswordSalt" },
                values: new object[] { "user1@user.com", "User1", new byte[] { 36, 148, 133, 233, 214, 153, 237, 14, 49, 191, 45, 157, 151, 180, 18, 82, 100, 255, 117, 30, 227, 151, 118, 139, 141, 78, 23, 70, 110, 254, 137, 116, 188, 12, 251, 68, 136, 205, 62, 184, 14, 238, 70, 203, 50, 65, 128, 194, 223, 68, 236, 49, 139, 147, 142, 28, 229, 182, 5, 68, 141, 63, 185, 19 }, new byte[] { 176, 238, 208, 121, 239, 111, 44, 13, 91, 71, 120, 239, 111, 140, 186, 38, 151, 39, 200, 140, 23, 180, 132, 188, 150, 245, 121, 40, 93, 253, 228, 94, 185, 223, 251, 222, 166, 51, 3, 231, 86, 45, 9, 82, 65, 160, 160, 16, 114, 34, 227, 221, 143, 156, 169, 251, 239, 68, 186, 164, 38, 30, 94, 26, 185, 238, 161, 54, 71, 186, 219, 251, 76, 117, 226, 4, 76, 64, 165, 186, 177, 246, 146, 202, 51, 18, 171, 67, 130, 136, 90, 221, 227, 203, 245, 44, 26, 240, 4, 153, 84, 31, 104, 118, 214, 219, 112, 70, 89, 124, 91, 43, 76, 50, 39, 11, 111, 43, 4, 202, 181, 38, 129, 126, 22, 17, 113, 19 } });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 126, 42, 52, 32, 67, 40, 151, 113, 191, 249, 249, 4, 228, 159, 214, 175, 92, 47, 191, 22, 70, 243, 153, 19, 242, 31, 57, 116, 10, 130, 174, 104, 127, 23, 205, 199, 193, 210, 142, 120, 139, 83, 41, 126, 85, 139, 45, 189, 128, 60, 158, 24, 42, 29, 180, 106, 77, 228, 31, 165, 129, 7, 210, 191 }, new byte[] { 136, 29, 207, 181, 220, 101, 216, 15, 193, 178, 245, 19, 199, 57, 248, 158, 228, 180, 203, 213, 196, 21, 239, 111, 195, 66, 19, 169, 216, 113, 88, 217, 147, 65, 22, 176, 38, 160, 196, 109, 23, 164, 197, 90, 162, 164, 21, 32, 203, 238, 243, 232, 36, 78, 148, 28, 99, 101, 178, 132, 6, 113, 106, 81, 155, 95, 62, 217, 8, 9, 79, 214, 218, 12, 51, 147, 174, 113, 127, 250, 21, 5, 222, 181, 24, 134, 91, 130, 51, 140, 120, 43, 57, 215, 33, 64, 105, 162, 173, 112, 177, 215, 9, 1, 3, 235, 240, 4, 117, 2, 21, 212, 0, 142, 86, 44, 33, 134, 23, 205, 82, 37, 82, 52, 194, 141, 99, 39 } });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Email", "Name", "PasswordHash", "PasswordSalt" },
                values: new object[] { "user@user.com", "User", new byte[] { 126, 42, 52, 32, 67, 40, 151, 113, 191, 249, 249, 4, 228, 159, 214, 175, 92, 47, 191, 22, 70, 243, 153, 19, 242, 31, 57, 116, 10, 130, 174, 104, 127, 23, 205, 199, 193, 210, 142, 120, 139, 83, 41, 126, 85, 139, 45, 189, 128, 60, 158, 24, 42, 29, 180, 106, 77, 228, 31, 165, 129, 7, 210, 191 }, new byte[] { 136, 29, 207, 181, 220, 101, 216, 15, 193, 178, 245, 19, 199, 57, 248, 158, 228, 180, 203, 213, 196, 21, 239, 111, 195, 66, 19, 169, 216, 113, 88, 217, 147, 65, 22, 176, 38, 160, 196, 109, 23, 164, 197, 90, 162, 164, 21, 32, 203, 238, 243, 232, 36, 78, 148, 28, 99, 101, 178, 132, 6, 113, 106, 81, 155, 95, 62, 217, 8, 9, 79, 214, 218, 12, 51, 147, 174, 113, 127, 250, 21, 5, 222, 181, 24, 134, 91, 130, 51, 140, 120, 43, 57, 215, 33, 64, 105, 162, 173, 112, 177, 215, 9, 1, 3, 235, 240, 4, 117, 2, 21, 212, 0, 142, 86, 44, 33, 134, 23, 205, 82, 37, 82, 52, 194, 141, 99, 39 } });
        }
    }
}
