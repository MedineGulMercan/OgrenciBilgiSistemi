using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OgrenciBilgiSistemi.Migrations
{
    /// <inheritdoc />
    public partial class mig3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CourseAssessments_Courses_course_id",
                table: "CourseAssessments");

            migrationBuilder.DropForeignKey(
                name: "FK_CourseAssessments_Teachers_teacher_id",
                table: "CourseAssessments");

            migrationBuilder.DropIndex(
                name: "IX_CourseAssessments_course_id",
                table: "CourseAssessments");

            migrationBuilder.DropColumn(
                name: "course_id",
                table: "CourseAssessments");

            migrationBuilder.RenameColumn(
                name: "teacher_id",
                table: "CourseAssessments",
                newName: "course_selection_id");

            migrationBuilder.RenameIndex(
                name: "IX_CourseAssessments_teacher_id",
                table: "CourseAssessments",
                newName: "IX_CourseAssessments_course_selection_id");

            migrationBuilder.AddForeignKey(
                name: "FK_CourseAssessments_CourseSelections_course_selection_id",
                table: "CourseAssessments",
                column: "course_selection_id",
                principalTable: "CourseSelections",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CourseAssessments_CourseSelections_course_selection_id",
                table: "CourseAssessments");

            migrationBuilder.RenameColumn(
                name: "course_selection_id",
                table: "CourseAssessments",
                newName: "teacher_id");

            migrationBuilder.RenameIndex(
                name: "IX_CourseAssessments_course_selection_id",
                table: "CourseAssessments",
                newName: "IX_CourseAssessments_teacher_id");

            migrationBuilder.AddColumn<Guid>(
                name: "course_id",
                table: "CourseAssessments",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_CourseAssessments_course_id",
                table: "CourseAssessments",
                column: "course_id");

            migrationBuilder.AddForeignKey(
                name: "FK_CourseAssessments_Courses_course_id",
                table: "CourseAssessments",
                column: "course_id",
                principalTable: "Courses",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CourseAssessments_Teachers_teacher_id",
                table: "CourseAssessments",
                column: "teacher_id",
                principalTable: "Teachers",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
