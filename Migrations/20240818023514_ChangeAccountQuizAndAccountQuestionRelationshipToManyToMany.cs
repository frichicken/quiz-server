using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace server.Migrations
{
    /// <inheritdoc />
    public partial class ChangeAccountQuizAndAccountQuestionRelationshipToManyToMany : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "IsSaved",
                table: "Quizzes");

            migrationBuilder.CreateTable(
                name: "QuestionAccount",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    IsStarred = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    AccountId = table.Column<int>(type: "int", nullable: true),
                    QuestionId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionAccount", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QuestionAccount_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_QuestionAccount_Questions_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "Questions",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "QuizAccount",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    LastAccess = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    IsSaved = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    OwnerId = table.Column<int>(type: "int", nullable: false),
                    AccountId = table.Column<int>(type: "int", nullable: true),
                    QuizId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuizAccount", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QuizAccount_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_QuizAccount_Quizzes_QuizId",
                        column: x => x.QuizId,
                        principalTable: "Quizzes",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionAccount_AccountId",
                table: "QuestionAccount",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionAccount_QuestionId",
                table: "QuestionAccount",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_QuizAccount_AccountId",
                table: "QuizAccount",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_QuizAccount_QuizId",
                table: "QuizAccount",
                column: "QuizId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "QuestionAccount");

            migrationBuilder.DropTable(
                name: "QuizAccount");

            migrationBuilder.AddColumn<int>(
                name: "AccountId",
                table: "Quizzes",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsSaved",
                table: "Quizzes",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_Quizzes_AccountId",
                table: "Quizzes",
                column: "AccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_Quizzes_Accounts_AccountId",
                table: "Quizzes",
                column: "AccountId",
                principalTable: "Accounts",
                principalColumn: "Id");
        }
    }
}
