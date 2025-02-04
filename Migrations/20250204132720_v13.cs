using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace warren_analysis_desk.Migrations
{
    /// <inheritdoc />
    public partial class v13 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "NewsId",
                table: "slack_messages",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<bool>(
                name: "Marked",
                table: "slack_messages",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_slack_messages_NewsId",
                table: "slack_messages",
                column: "NewsId");

            migrationBuilder.AddForeignKey(
                name: "FK_slack_messages_News_NewsId",
                table: "slack_messages",
                column: "NewsId",
                principalTable: "News",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_slack_messages_News_NewsId",
                table: "slack_messages");

            migrationBuilder.DropIndex(
                name: "IX_slack_messages_NewsId",
                table: "slack_messages");

            migrationBuilder.DropColumn(
                name: "Marked",
                table: "slack_messages");

            migrationBuilder.AlterColumn<string>(
                name: "NewsId",
                table: "slack_messages",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("MySql:CharSet", "utf8mb4");
        }
    }
}
