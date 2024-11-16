using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OutSysCollegeManagement.Migrations
{
    /// <inheritdoc />
    public partial class AddHostelForeignKeyToStudent : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "hostel_id",
                table: "Students",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Students_hostel_id",
                table: "Students",
                column: "hostel_id");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Hostels_hostel_id",
                table: "Students",
                column: "hostel_id",
                principalTable: "Hostels",
                principalColumn: "Hostel_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Students_Hostels_hostel_id",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_Students_hostel_id",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "hostel_id",
                table: "Students");
        }
    }
}
