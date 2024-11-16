using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OutSysCollegeManagement.Migrations
{
    /// <inheritdoc />
    public partial class AddFacultyForeignKeyToFaculty_Phone : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Faculty_Phones",
                table: "Faculty_Phones");

            migrationBuilder.AddColumn<int>(
                name: "F_id",
                table: "Faculty_Phones",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Faculty_Phones",
                table: "Faculty_Phones",
                columns: new[] { "SID", "Mobile_no", "F_id" });

            migrationBuilder.CreateIndex(
                name: "IX_Faculty_Phones_F_id",
                table: "Faculty_Phones",
                column: "F_id");

            migrationBuilder.AddForeignKey(
                name: "FK_Faculty_Phones_Faculties_F_id",
                table: "Faculty_Phones",
                column: "F_id",
                principalTable: "Faculties",
                principalColumn: "Fid",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Faculty_Phones_Faculties_F_id",
                table: "Faculty_Phones");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Faculty_Phones",
                table: "Faculty_Phones");

            migrationBuilder.DropIndex(
                name: "IX_Faculty_Phones_F_id",
                table: "Faculty_Phones");

            migrationBuilder.DropColumn(
                name: "F_id",
                table: "Faculty_Phones");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Faculty_Phones",
                table: "Faculty_Phones",
                columns: new[] { "SID", "Mobile_no" });
        }
    }
}
