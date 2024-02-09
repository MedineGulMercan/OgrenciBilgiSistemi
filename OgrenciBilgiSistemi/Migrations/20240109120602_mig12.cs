using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OgrenciBilgiSistemi.Migrations
{
    /// <inheritdoc />
    public partial class mig12 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "grade",
                table: "CourseLetterScores");

            migrationBuilder.DropColumn(
                name: "score_end",
                table: "CourseLetterScores");

            migrationBuilder.RenameColumn(
                name: "score_start",
                table: "CourseLetterScores",
                newName: "course_grade");

            migrationBuilder.AddColumn<Guid>(
                name: "letter_grade_id",
                table: "CourseLetterScores",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "LetterGrades",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    letter = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    grade = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    order_by = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LetterGrades", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CourseLetterScores_letter_grade_id",
                table: "CourseLetterScores",
                column: "letter_grade_id");

            migrationBuilder.AddForeignKey(
                name: "FK_CourseLetterScores_LetterGrades_letter_grade_id",
                table: "CourseLetterScores",
                column: "letter_grade_id",
                principalTable: "LetterGrades",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CourseLetterScores_LetterGrades_letter_grade_id",
                table: "CourseLetterScores");

            migrationBuilder.DropTable(
                name: "LetterGrades");

            migrationBuilder.DropIndex(
                name: "IX_CourseLetterScores_letter_grade_id",
                table: "CourseLetterScores");

            migrationBuilder.DropColumn(
                name: "letter_grade_id",
                table: "CourseLetterScores");

            migrationBuilder.RenameColumn(
                name: "course_grade",
                table: "CourseLetterScores",
                newName: "score_start");

            migrationBuilder.AddColumn<string>(
                name: "grade",
                table: "CourseLetterScores",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "score_end",
                table: "CourseLetterScores",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
