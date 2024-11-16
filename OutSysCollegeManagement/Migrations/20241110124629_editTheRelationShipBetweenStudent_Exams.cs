using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OutSysCollegeManagement.Migrations
{
    /// <inheritdoc />
    public partial class editTheRelationShipBetweenStudent_Exams : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ExamsStudent",
                columns: table => new
                {
                    ExamsExam_code = table.Column<int>(type: "int", nullable: false),
                    StudentsSID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExamsStudent", x => new { x.ExamsExam_code, x.StudentsSID });
                    table.ForeignKey(
                        name: "FK_ExamsStudent_Exams_ExamsExam_code",
                        column: x => x.ExamsExam_code,
                        principalTable: "Exams",
                        principalColumn: "Exam_code",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ExamsStudent_Students_StudentsSID",
                        column: x => x.StudentsSID,
                        principalTable: "Students",
                        principalColumn: "SID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ExamsStudent_StudentsSID",
                table: "ExamsStudent",
                column: "StudentsSID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExamsStudent");
        }
    }
}
