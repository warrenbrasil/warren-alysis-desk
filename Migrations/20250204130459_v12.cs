using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace warren_analysis_desk.Migrations
{
    /// <inheritdoc />
    public partial class v12 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_slack_messages_News_NewsId1",
                table: "slack_messages");

            migrationBuilder.DropIndex(
                name: "IX_slack_messages_NewsId1",
                table: "slack_messages");

            migrationBuilder.DropColumn(
                name: "NewsId1",
                table: "slack_messages");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "NewsId1",
                table: "slack_messages",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_slack_messages_NewsId1",
                table: "slack_messages",
                column: "NewsId1");

            migrationBuilder.AddForeignKey(
                name: "FK_slack_messages_News_NewsId1",
                table: "slack_messages",
                column: "NewsId1",
                principalTable: "News",
                principalColumn: "Id");
        }
    }
}
