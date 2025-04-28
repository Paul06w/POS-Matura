using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace _250317_3PLF_Anmeldesystem.Migrations
{
    /// <inheritdoc />
    public partial class id : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Registrations_Departments_DepartmentId",
                table: "Registrations");

            migrationBuilder.AlterColumn<int>(
                name: "DepartmentId",
                table: "Registrations",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddForeignKey(
                name: "FK_Registrations_Departments_DepartmentId",
                table: "Registrations",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Registrations_Departments_DepartmentId",
                table: "Registrations");

            migrationBuilder.AlterColumn<int>(
                name: "DepartmentId",
                table: "Registrations",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Registrations_Departments_DepartmentId",
                table: "Registrations",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
