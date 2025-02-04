using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace warren_analysis_desk.Migrations
{
    /// <inheritdoc />
    public partial class v15 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_slack_messages_News_NewsId",
                table: "slack_messages");

            migrationBuilder.RenameColumn(
                name: "NewsId",
                table: "slack_messages",
                newName: "IdNews");

            migrationBuilder.RenameIndex(
                name: "IX_slack_messages_NewsId",
                table: "slack_messages",
                newName: "IX_slack_messages_IdNews");

            migrationBuilder.AddForeignKey(
                name: "FK_slack_messages_News_IdNews",
                table: "slack_messages",
                column: "IdNews",
                principalTable: "News",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_slack_messages_News_IdNews",
                table: "slack_messages");

            migrationBuilder.RenameColumn(
                name: "IdNews",
                table: "slack_messages",
                newName: "NewsId");

            migrationBuilder.RenameIndex(
                name: "IX_slack_messages_IdNews",
                table: "slack_messages",
                newName: "IX_slack_messages_NewsId");

            migrationBuilder.AddForeignKey(
                name: "FK_slack_messages_News_NewsId",
                table: "slack_messages",
                column: "NewsId",
                principalTable: "News",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
