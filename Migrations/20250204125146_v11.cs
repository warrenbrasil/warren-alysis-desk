using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace warren_analysis_desk.Migrations
{
    /// <inheritdoc />
    public partial class v11 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SlackMessages_News_IdNews",
                table: "SlackMessages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SlackMessages",
                table: "SlackMessages");

            migrationBuilder.DropIndex(
                name: "IX_SlackMessages_IdNews",
                table: "SlackMessages");

            migrationBuilder.DropColumn(
                name: "IdNews",
                table: "SlackMessages");

            migrationBuilder.RenameTable(
                name: "SlackMessages",
                newName: "slack_messages");

            migrationBuilder.AddColumn<string>(
                name: "NewsId",
                table: "slack_messages",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<int>(
                name: "NewsId1",
                table: "slack_messages",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_slack_messages",
                table: "slack_messages",
                column: "Id");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_slack_messages_News_NewsId1",
                table: "slack_messages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_slack_messages",
                table: "slack_messages");

            migrationBuilder.DropIndex(
                name: "IX_slack_messages_NewsId1",
                table: "slack_messages");

            migrationBuilder.DropColumn(
                name: "NewsId",
                table: "slack_messages");

            migrationBuilder.DropColumn(
                name: "NewsId1",
                table: "slack_messages");

            migrationBuilder.RenameTable(
                name: "slack_messages",
                newName: "SlackMessages");

            migrationBuilder.AddColumn<int>(
                name: "IdNews",
                table: "SlackMessages",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_SlackMessages",
                table: "SlackMessages",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_SlackMessages_IdNews",
                table: "SlackMessages",
                column: "IdNews");

            migrationBuilder.AddForeignKey(
                name: "FK_SlackMessages_News_IdNews",
                table: "SlackMessages",
                column: "IdNews",
                principalTable: "News",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
