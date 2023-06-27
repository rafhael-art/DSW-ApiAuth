using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Api_Auth.Migrations
{
    public partial class Iasdf : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CodigoCliente",
                table: "Empleados",
                newName: "CodigoEmpleado");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CodigoEmpleado",
                table: "Empleados",
                newName: "CodigoCliente");
        }
    }
}
