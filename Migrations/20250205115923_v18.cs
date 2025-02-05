using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace warren_analysis_desk.Migrations
{
    /// <inheritdoc />
    public partial class v18 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_user_slack_messages_slack_messages_SlackMessagesId",
                table: "user_slack_messages");

            migrationBuilder.DropColumn(
                name: "IdSlackMessages",
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

            migrationBuilder.AddForeignKey(
                name: "FK_user_slack_messages_slack_messages_SlackMessagesId",
                table: "user_slack_messages",
                column: "SlackMessagesId",
                principalTable: "slack_messages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_user_slack_messages_slack_messages_SlackMessagesId",
                table: "user_slack_messages");

            migrationBuilder.AlterColumn<int>(
                name: "SlackMessagesId",
                table: "user_slack_messages",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "IdSlackMessages",
                table: "user_slack_messages",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_user_slack_messages_slack_messages_SlackMessagesId",
                table: "user_slack_messages",
                column: "SlackMessagesId",
                principalTable: "slack_messages",
                principalColumn: "Id");
        }
    }
}
