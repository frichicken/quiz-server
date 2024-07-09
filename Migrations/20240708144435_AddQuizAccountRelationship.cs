using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace server.Migrations
{
    /// <inheritdoc />
    public partial class AddQuizAccountRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AccountId",
                table: "Quizzes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Quizzes_AccountId",
                table: "Quizzes",
                column: "AccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_Quizzes_Accounts_AccountId",
                table: "Quizzes",
                column: "AccountId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Quizzes_Accounts_AccountId",
                table: "Quizzes");

            migrationBuilder.DropIndex(
                name: "IX_Quizzes_AccountId",
                table: "Quizzes");

            migrationBuilder.DropColumn(
                name: "AccountId",
                table: "Quizzes");
        }
    }
}
