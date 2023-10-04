using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SDCL_ChatTool.Migrations
{
    public partial class Init_Database : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ChatGptResponseDump",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ChatGptResponseId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TotalTokens = table.Column<int>(type: "int", nullable: false),
                    PromptTokens = table.Column<int>(type: "int", nullable: false),
                    CompletionTokens = table.Column<int>(type: "int", nullable: false),
                    ContentJson = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAtChatGptEnd = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ChatLogId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChatGptResponseDump", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ChatLog",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ChatRole = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ChatMessage = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsSummary = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SessionId = table.Column<int>(type: "int", nullable: false),
                    ParticipantInfoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChatLog", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChatGptResponseDump");

            migrationBuilder.DropTable(
                name: "ChatLog");
        }
    }
}
