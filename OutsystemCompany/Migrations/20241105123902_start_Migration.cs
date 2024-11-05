using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OutsystemCompany.Migrations
{
    /// <inheritdoc />
    public partial class start_Migration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Departments",
                columns: table => new
                {
                    Dnumber = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Dname = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    MgrSsn = table.Column<int>(type: "int", nullable: false),
                    MgrStartDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departments", x => x.Dnumber);
                });

            migrationBuilder.CreateTable(
                name: "DeptLocations",
                columns: table => new
                {
                    Dnumber = table.Column<int>(type: "int", nullable: false),
                    Dlocation = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeptLocations", x => new { x.Dnumber, x.Dlocation });
                    table.ForeignKey(
                        name: "FK_DeptLocations_Departments_Dnumber",
                        column: x => x.Dnumber,
                        principalTable: "Departments",
                        principalColumn: "Dnumber",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
      name: "Employees",
      columns: table => new
      {
          Ssn = table.Column<int>(type: "int", nullable: false),  // Remove Identity to make it non-auto-incrementing
          Fname = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
          Minit = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
          Lname = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
          Bdate = table.Column<DateTime>(type: "datetime2", nullable: false),
          Address = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
          Sex = table.Column<string>(type: "nvarchar(6)", maxLength: 6, nullable: false),
          Salary = table.Column<double>(type: "float", nullable: false),
          Super_Ssn = table.Column<int>(type: "int", nullable: true),  // Allow nullable supervisor SSN
          Dno = table.Column<int>(type: "int", nullable: true)  // Allow nullable department number
      },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Ssn);  // Primary key on Ssn
                    table.ForeignKey(
                        name: "FK_Employees_Departments_Dno",
                        column: x => x.Dno,
                        principalTable: "Departments",
                        principalColumn: "Dnumber",
                        onDelete: ReferentialAction.Restrict  // Use Restrict to prevent deletion in Departments if Employees reference it
                    );
                    table.ForeignKey(
                        name: "FK_Employees_Employees_Super_Ssn",
                        column: x => x.Super_Ssn,
                        principalTable: "Employees",
                        principalColumn: "Ssn",
                        onDelete: ReferentialAction.Restrict  // Restrict to handle nullable supervisor
                    );
                });

            migrationBuilder.CreateTable(
                name: "Projects",
                columns: table => new
                {
                    Pnumber = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Pname = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Plocation = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Dnum = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projects", x => x.Pnumber);
                    table.ForeignKey(
                        name: "FK_Projects_Departments_Dnum",
                        column: x => x.Dnum,
                        principalTable: "Departments",
                        principalColumn: "Dnumber",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Dependents",
                columns: table => new
                {
                    Essn = table.Column<int>(type: "int", nullable: false),
                    DependentName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Sex = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Bdate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Relationship = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dependents", x => new { x.Essn, x.DependentName });
                    table.ForeignKey(
                        name: "FK_Dependents_Employees_Essn",
                        column: x => x.Essn,
                        principalTable: "Employees",
                        principalColumn: "Ssn",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WorksOn",
                columns: table => new
                {
                    Essn = table.Column<int>(type: "int", nullable: false),
                    Pno = table.Column<int>(type: "int", nullable: false),
                    Hours = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorksOn", x => new { x.Essn, x.Pno });
                    table.ForeignKey(
                        name: "FK_WorksOn_Employees_Essn",
                        column: x => x.Essn,
                        principalTable: "Employees",
                        principalColumn: "Ssn",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WorksOn_Projects_Pno",
                        column: x => x.Pno,
                        principalTable: "Projects",
                        principalColumn: "Pnumber",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Departments_MgrSsn",
                table: "Departments",
                column: "MgrSsn");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_Dno",
                table: "Employees",
                column: "Dno");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_Super_Ssn",
                table: "Employees",
                column: "Super_Ssn");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_Dnum",
                table: "Projects",
                column: "Dnum");

            migrationBuilder.CreateIndex(
                name: "IX_WorksOn_Pno",
                table: "WorksOn",
                column: "Pno");

            migrationBuilder.AddForeignKey(
                name: "FK_Departments_Employees_MgrSsn",
                table: "Departments",
                column: "MgrSsn",
                principalTable: "Employees",
                principalColumn: "Ssn",
                
                onDelete: ReferentialAction.NoAction
                );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Departments_Employees_MgrSsn",
                table: "Departments");

            migrationBuilder.DropTable(
                name: "Dependents");

            migrationBuilder.DropTable(
                name: "DeptLocations");

            migrationBuilder.DropTable(
                name: "WorksOn");

            migrationBuilder.DropTable(
                name: "Projects");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "Departments");
        }
    }
}
