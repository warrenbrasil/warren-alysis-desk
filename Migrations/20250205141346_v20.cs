using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace warren_analysis_desk.Migrations
{
    /// <inheritdoc />
    public partial class v20 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_user_slack_messages_slack_messages_SlackMessagesId",
                table: "user_slack_messages");

            migrationBuilder.DropColumn(
                name: "Marked",
                table: "slack_messages");

            migrationBuilder.AlterColumn<int>(
                name: "SlackMessagesId",
                table: "user_slack_messages",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<bool>(
                name: "Marked",
                table: "user_slack_messages",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddForeignKey(
                name: "FK_user_slack_messages_slack_messages_SlackMessagesId",
                table: "user_slack_messages",
                column: "SlackMessagesId",
                principalTable: "slack_messages",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_user_slack_messages_slack_messages_SlackMessagesId",
                table: "user_slack_messages");

            migrationBuilder.DropColumn(
                name: "Marked",
                table: "user_slack_messages");

            migrationBuilder.AlterColumn<int>(
                name: "SlackMessagesId",
                table: "user_slack_messages",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Marked",
                table: "slack_messages",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddForeignKey(
                name: "FK_user_slack_messages_slack_messages_SlackMessagesId",
                table: "user_slack_messages",
                column: "SlackMessagesId",
                principalTable: "slack_messages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
