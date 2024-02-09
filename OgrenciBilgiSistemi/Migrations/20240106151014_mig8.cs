using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OgrenciBilgiSistemi.Migrations
{
    /// <inheritdoc />
    public partial class mig8 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "assesment_year",
                table: "CourseAssessments");

            migrationBuilder.AddColumn<Guid>(
                name: "current_semesters_id",
                table: "CourseAssessments",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "current_semesters_id",
                table: "CourseAssessments");

            migrationBuilder.AddColumn<string>(
                name: "assesment_year",
                table: "CourseAssessments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
