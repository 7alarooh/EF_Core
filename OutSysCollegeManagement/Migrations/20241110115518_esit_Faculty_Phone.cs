using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OutSysCollegeManagement.Migrations
{
    /// <inheritdoc />
    public partial class esit_Faculty_Phone : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Faculty_Phones_Faculties_Fid",
                table: "Faculty_Phones");

            migrationBuilder.DropForeignKey(
                name: "FK_Faculty_Phones_Students_SID",
                table: "Faculty_Phones");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Faculty_Phones",
                table: "Faculty_Phones");

            migrationBuilder.DropIndex(
                name: "IX_Faculty_Phones_Fid",
                table: "Faculty_Phones");

            migrationBuilder.DropColumn(
                name: "SID",
                table: "Faculty_Phones");

            migrationBuilder.RenameTable(
                name: "Faculty_Phones",
                newName: "Faculty_Phone");

            migrationBuilder.RenameColumn(
                name: "Mobile_no",
                table: "Faculty_Phone",
                newName: "Phone_no");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Faculty_Phone",
                table: "Faculty_Phone",
                columns: new[] { "Fid", "Phone_no" });

            migrationBuilder.AddForeignKey(
                name: "FK_Faculty_Phone_Faculties_Fid",
                table: "Faculty_Phone",
                column: "Fid",
                principalTable: "Faculties",
                principalColumn: "Fid",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Faculty_Phone_Faculties_Fid",
                table: "Faculty_Phone");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Faculty_Phone",
                table: "Faculty_Phone");

            migrationBuilder.RenameTable(
                name: "Faculty_Phone",
                newName: "Faculty_Phones");

            migrationBuilder.RenameColumn(
                name: "Phone_no",
                table: "Faculty_Phones",
                newName: "Mobile_no");

            migrationBuilder.AddColumn<int>(
                name: "SID",
                table: "Faculty_Phones",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Faculty_Phones",
                table: "Faculty_Phones",
                columns: new[] { "SID", "Mobile_no", "Fid" });

            migrationBuilder.CreateIndex(
                name: "IX_Faculty_Phones_Fid",
                table: "Faculty_Phones",
                column: "Fid");

            migrationBuilder.AddForeignKey(
                name: "FK_Faculty_Phones_Faculties_Fid",
                table: "Faculty_Phones",
                column: "Fid",
                principalTable: "Faculties",
                principalColumn: "Fid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Faculty_Phones_Students_SID",
                table: "Faculty_Phones",
                column: "SID",
                principalTable: "Students",
                principalColumn: "SID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
