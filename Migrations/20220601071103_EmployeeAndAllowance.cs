using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CRUDWEPAPI_EF.Migrations
{
    public partial class EmployeeAndAllowance : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Employee",
                columns: table => new
                {
                    employeeID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    employeeName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    promotedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    employeeDept = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    employeeEmail = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employee", x => x.employeeID);
                });

            migrationBuilder.CreateTable(
                name: "Allowance",
                columns: table => new
                {
                    employeeAllowancesID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    employeeID = table.Column<int>(type: "int", nullable: false),
                    allowanceType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Allowance", x => x.employeeAllowancesID);
                    table.ForeignKey(
                        name: "FK_Allowance_Employee_employeeID",
                        column: x => x.employeeID,
                        principalTable: "Employee",
                        principalColumn: "employeeID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Allowance_employeeID",
                table: "Allowance",
                column: "employeeID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Allowance");

            migrationBuilder.DropTable(
                name: "Employee");
        }
    }
}
