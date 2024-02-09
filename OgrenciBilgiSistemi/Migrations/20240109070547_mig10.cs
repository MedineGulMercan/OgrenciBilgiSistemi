using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OgrenciBilgiSistemi.Migrations
{
    /// <inheritdoc />
    public partial class mig10 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CourseSelections_RoleSemesterCoursess_semester_courses_id",
                table: "CourseSelections");

            migrationBuilder.DropForeignKey(
                name: "FK_RoleSemesterCoursess_Classes_class_id",
                table: "RoleSemesterCoursess");

            migrationBuilder.DropForeignKey(
                name: "FK_RoleSemesterCoursess_Courses_course_id",
                table: "RoleSemesterCoursess");

            migrationBuilder.DropForeignKey(
                name: "FK_RoleSemesterCoursess_Departments_department_id",
                table: "RoleSemesterCoursess");

            migrationBuilder.DropForeignKey(
                name: "FK_RoleSemesterCoursess_Semesters_semester_id",
                table: "RoleSemesterCoursess");

            migrationBuilder.DropForeignKey(
                name: "FK_RoleSemesterCoursess_Teachers_teacher_id",
                table: "RoleSemesterCoursess");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RoleSemesterCoursess",
                table: "RoleSemesterCoursess");

            migrationBuilder.RenameTable(
                name: "RoleSemesterCoursess",
                newName: "SemesterCoursess");

            migrationBuilder.RenameIndex(
                name: "IX_RoleSemesterCoursess_teacher_id",
                table: "SemesterCoursess",
                newName: "IX_SemesterCoursess_teacher_id");

            migrationBuilder.RenameIndex(
                name: "IX_RoleSemesterCoursess_semester_id",
                table: "SemesterCoursess",
                newName: "IX_SemesterCoursess_semester_id");

            migrationBuilder.RenameIndex(
                name: "IX_RoleSemesterCoursess_department_id",
                table: "SemesterCoursess",
                newName: "IX_SemesterCoursess_department_id");

            migrationBuilder.RenameIndex(
                name: "IX_RoleSemesterCoursess_course_id",
                table: "SemesterCoursess",
                newName: "IX_SemesterCoursess_course_id");

            migrationBuilder.RenameIndex(
                name: "IX_RoleSemesterCoursess_class_id",
                table: "SemesterCoursess",
                newName: "IX_SemesterCoursess_class_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SemesterCoursess",
                table: "SemesterCoursess",
                column: "id");

            migrationBuilder.CreateTable(
                name: "CourseLetterScores",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    course_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    semester_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    score_start = table.Column<int>(type: "int", nullable: false),
                    score_end = table.Column<int>(type: "int", nullable: false),
                    grade = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseLetterScores", x => x.id);
                    table.ForeignKey(
                        name: "FK_CourseLetterScores_Courses_course_id",
                        column: x => x.course_id,
                        principalTable: "Courses",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CourseLetterScores_Semesters_semester_id",
                        column: x => x.semester_id,
                        principalTable: "Semesters",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CourseLetterScores_course_id",
                table: "CourseLetterScores",
                column: "course_id");

            migrationBuilder.CreateIndex(
                name: "IX_CourseLetterScores_semester_id",
                table: "CourseLetterScores",
                column: "semester_id");

            migrationBuilder.AddForeignKey(
                name: "FK_CourseSelections_SemesterCoursess_semester_courses_id",
                table: "CourseSelections",
                column: "semester_courses_id",
                principalTable: "SemesterCoursess",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SemesterCoursess_Classes_class_id",
                table: "SemesterCoursess",
                column: "class_id",
                principalTable: "Classes",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SemesterCoursess_Courses_course_id",
                table: "SemesterCoursess",
                column: "course_id",
                principalTable: "Courses",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SemesterCoursess_Departments_department_id",
                table: "SemesterCoursess",
                column: "department_id",
                principalTable: "Departments",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SemesterCoursess_Semesters_semester_id",
                table: "SemesterCoursess",
                column: "semester_id",
                principalTable: "Semesters",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SemesterCoursess_Teachers_teacher_id",
                table: "SemesterCoursess",
                column: "teacher_id",
                principalTable: "Teachers",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CourseSelections_SemesterCoursess_semester_courses_id",
                table: "CourseSelections");

            migrationBuilder.DropForeignKey(
                name: "FK_SemesterCoursess_Classes_class_id",
                table: "SemesterCoursess");

            migrationBuilder.DropForeignKey(
                name: "FK_SemesterCoursess_Courses_course_id",
                table: "SemesterCoursess");

            migrationBuilder.DropForeignKey(
                name: "FK_SemesterCoursess_Departments_department_id",
                table: "SemesterCoursess");

            migrationBuilder.DropForeignKey(
                name: "FK_SemesterCoursess_Semesters_semester_id",
                table: "SemesterCoursess");

            migrationBuilder.DropForeignKey(
                name: "FK_SemesterCoursess_Teachers_teacher_id",
                table: "SemesterCoursess");

            migrationBuilder.DropTable(
                name: "CourseLetterScores");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SemesterCoursess",
                table: "SemesterCoursess");

            migrationBuilder.RenameTable(
                name: "SemesterCoursess",
                newName: "RoleSemesterCoursess");

            migrationBuilder.RenameIndex(
                name: "IX_SemesterCoursess_teacher_id",
                table: "RoleSemesterCoursess",
                newName: "IX_RoleSemesterCoursess_teacher_id");

            migrationBuilder.RenameIndex(
                name: "IX_SemesterCoursess_semester_id",
                table: "RoleSemesterCoursess",
                newName: "IX_RoleSemesterCoursess_semester_id");

            migrationBuilder.RenameIndex(
                name: "IX_SemesterCoursess_department_id",
                table: "RoleSemesterCoursess",
                newName: "IX_RoleSemesterCoursess_department_id");

            migrationBuilder.RenameIndex(
                name: "IX_SemesterCoursess_course_id",
                table: "RoleSemesterCoursess",
                newName: "IX_RoleSemesterCoursess_course_id");

            migrationBuilder.RenameIndex(
                name: "IX_SemesterCoursess_class_id",
                table: "RoleSemesterCoursess",
                newName: "IX_RoleSemesterCoursess_class_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RoleSemesterCoursess",
                table: "RoleSemesterCoursess",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_CourseSelections_RoleSemesterCoursess_semester_courses_id",
                table: "CourseSelections",
                column: "semester_courses_id",
                principalTable: "RoleSemesterCoursess",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RoleSemesterCoursess_Classes_class_id",
                table: "RoleSemesterCoursess",
                column: "class_id",
                principalTable: "Classes",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RoleSemesterCoursess_Courses_course_id",
                table: "RoleSemesterCoursess",
                column: "course_id",
                principalTable: "Courses",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RoleSemesterCoursess_Departments_department_id",
                table: "RoleSemesterCoursess",
                column: "department_id",
                principalTable: "Departments",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RoleSemesterCoursess_Semesters_semester_id",
                table: "RoleSemesterCoursess",
                column: "semester_id",
                principalTable: "Semesters",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RoleSemesterCoursess_Teachers_teacher_id",
                table: "RoleSemesterCoursess",
                column: "teacher_id",
                principalTable: "Teachers",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
