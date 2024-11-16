using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OutSysCollegeManagement.Migrations
{
    /// <inheritdoc />
    public partial class AddHostelForeignKeyToStudent_v1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Students_Hostels_hostel_id",
                table: "Students");

            migrationBuilder.RenameColumn(
                name: "hostel_id",
                table: "Students",
                newName: "Hostel_id");

            migrationBuilder.RenameIndex(
                name: "IX_Students_hostel_id",
                table: "Students",
                newName: "IX_Students_Hostel_id");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Hostels_Hostel_id",
                table: "Students",
                column: "Hostel_id",
                principalTable: "Hostels",
                principalColumn: "Hostel_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Students_Hostels_Hostel_id",
                table: "Students");

            migrationBuilder.RenameColumn(
                name: "Hostel_id",
                table: "Students",
                newName: "hostel_id");

            migrationBuilder.RenameIndex(
                name: "IX_Students_Hostel_id",
                table: "Students",
                newName: "IX_Students_hostel_id");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Hostels_hostel_id",
                table: "Students",
                column: "hostel_id",
                principalTable: "Hostels",
                principalColumn: "Hostel_id");
        }
    }
}
