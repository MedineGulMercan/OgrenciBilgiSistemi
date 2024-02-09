using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OgrenciBilgiSistemi.Migrations
{
    /// <inheritdoc />
    public partial class mig : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "order",
                table: "CurrentSemesters");

            migrationBuilder.AddColumn<bool>(
                name: "is_active",
                table: "CurrentSemesters",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "is_active",
                table: "CurrentSemesters");

            migrationBuilder.AddColumn<int>(
                name: "order",
                table: "CurrentSemesters",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
