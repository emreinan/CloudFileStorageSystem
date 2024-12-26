using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace FileMetadataAPI.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Mig4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_FileShares",
                table: "FileShares");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "FileShares",
                type: "integer",
                nullable: false,
                defaultValue: 0)
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_FileShares",
                table: "FileShares",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_FileShares_FileId",
                table: "FileShares",
                column: "FileId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_FileShares",
                table: "FileShares");

            migrationBuilder.DropIndex(
                name: "IX_FileShares_FileId",
                table: "FileShares");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "FileShares");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FileShares",
                table: "FileShares",
                columns: new[] { "FileId", "UserId" });
        }
    }
}
