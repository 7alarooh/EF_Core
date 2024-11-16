using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OutSysCollegeManagement.Migrations
{
    /// <inheritdoc />
    public partial class AddFacultyForeignKeyToFaculty_Phone_v1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Faculty_Phones_Faculties_F_id",
                table: "Faculty_Phones");

            migrationBuilder.RenameColumn(
                name: "F_id",
                table: "Faculty_Phones",
                newName: "Fid");

            migrationBuilder.RenameIndex(
                name: "IX_Faculty_Phones_F_id",
                table: "Faculty_Phones",
                newName: "IX_Faculty_Phones_Fid");

            migrationBuilder.AddForeignKey(
                name: "FK_Faculty_Phones_Faculties_Fid",
                table: "Faculty_Phones",
                column: "Fid",
                principalTable: "Faculties",
                principalColumn: "Fid",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Faculty_Phones_Faculties_Fid",
                table: "Faculty_Phones");

            migrationBuilder.RenameColumn(
                name: "Fid",
                table: "Faculty_Phones",
                newName: "F_id");

            migrationBuilder.RenameIndex(
                name: "IX_Faculty_Phones_Fid",
                table: "Faculty_Phones",
                newName: "IX_Faculty_Phones_F_id");

            migrationBuilder.AddForeignKey(
                name: "FK_Faculty_Phones_Faculties_F_id",
                table: "Faculty_Phones",
                column: "F_id",
                principalTable: "Faculties",
                principalColumn: "Fid",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
