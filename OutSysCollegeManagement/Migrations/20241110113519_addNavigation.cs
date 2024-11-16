using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OutSysCollegeManagement.Migrations
{
    /// <inheritdoc />
    public partial class addNavigation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FacultyFid",
                table: "Courses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Courses_FacultyFid",
                table: "Courses",
                column: "FacultyFid");

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_Faculties_FacultyFid",
                table: "Courses",
                column: "FacultyFid",
                principalTable: "Faculties",
                principalColumn: "Fid",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Courses_Faculties_FacultyFid",
                table: "Courses");

            migrationBuilder.DropIndex(
                name: "IX_Courses_FacultyFid",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "FacultyFid",
                table: "Courses");
        }
    }
}
