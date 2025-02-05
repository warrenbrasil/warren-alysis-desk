using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace warren_analysis_desk.Migrations
{
    /// <inheritdoc />
    public partial class v16 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "user_slack_messages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    SlackUserName = table.Column<int>(type: "int", nullable: false),
                    IdSlackMessages = table.Column<int>(type: "int", nullable: false),
                    SlackMessagesId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_slack_messages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_user_slack_messages_slack_messages_SlackMessagesId",
                        column: x => x.SlackMessagesId,
                        principalTable: "slack_messages",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_user_slack_messages_SlackMessagesId",
                table: "user_slack_messages",
                column: "SlackMessagesId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "user_slack_messages");
        }
    }
}
