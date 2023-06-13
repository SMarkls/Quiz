using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Server.Migrations
{
    /// <inheritdoc />
    public partial class EditedModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Questions_States_GameStateId",
                table: "Questions");

            migrationBuilder.DropIndex(
                name: "IX_Questions_GameStateId",
                table: "Questions");

            migrationBuilder.DropColumn(
                name: "GameStateId",
                table: "Questions");

            migrationBuilder.CreateTable(
                name: "GameSessionQuestion",
                columns: table => new
                {
                    AskedQuestionsId = table.Column<long>(type: "INTEGER", nullable: false),
                    SessionsId = table.Column<long>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameSessionQuestion", x => new { x.AskedQuestionsId, x.SessionsId });
                    table.ForeignKey(
                        name: "FK_GameSessionQuestion_Questions_AskedQuestionsId",
                        column: x => x.AskedQuestionsId,
                        principalTable: "Questions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GameSessionQuestion_Sessions_SessionsId",
                        column: x => x.SessionsId,
                        principalTable: "Sessions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GameSessionQuestion_SessionsId",
                table: "GameSessionQuestion",
                column: "SessionsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GameSessionQuestion");

            migrationBuilder.AddColumn<long>(
                name: "GameStateId",
                table: "Questions",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Questions_GameStateId",
                table: "Questions",
                column: "GameStateId");

            migrationBuilder.AddForeignKey(
                name: "FK_Questions_States_GameStateId",
                table: "Questions",
                column: "GameStateId",
                principalTable: "States",
                principalColumn: "Id");
        }
    }
}
