using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OgrenciBilgiSistemi.Migrations
{
    /// <inheritdoc />
    public partial class mig11 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CourseLetterScores_Semesters_semester_id",
                table: "CourseLetterScores");

            migrationBuilder.DropIndex(
                name: "IX_CourseLetterScores_semester_id",
                table: "CourseLetterScores");

            migrationBuilder.DropColumn(
                name: "semester_id",
                table: "CourseLetterScores");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "semester_id",
                table: "CourseLetterScores",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_CourseLetterScores_semester_id",
                table: "CourseLetterScores",
                column: "semester_id");

            migrationBuilder.AddForeignKey(
                name: "FK_CourseLetterScores_Semesters_semester_id",
                table: "CourseLetterScores",
                column: "semester_id",
                principalTable: "Semesters",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
