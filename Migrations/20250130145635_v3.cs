using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace warren_analysis_desk.Migrations
{
    /// <inheritdoc />
    public partial class v3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "NewsHtml",
                table: "News",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "ShortUrl",
                table: "News",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NewsHtml",
                table: "News");

            migrationBuilder.DropColumn(
                name: "ShortUrl",
                table: "News");
        }
    }
}
