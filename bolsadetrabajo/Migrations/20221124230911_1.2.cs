using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace bolsadetrabajo.Migrations
{
    /// <inheritdoc />
    public partial class _12 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Publicacion_Empresa_EmpresaId",
                table: "Publicacion");

            migrationBuilder.AlterColumn<string>(
                name: "Fecha",
                table: "Publicacion",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "EmpresaId",
                table: "Publicacion",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Publicacion_Empresa_EmpresaId",
                table: "Publicacion",
                column: "EmpresaId",
                principalTable: "Empresa",
                principalColumn: "idEmpresa");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Publicacion_Empresa_EmpresaId",
                table: "Publicacion");

            migrationBuilder.AlterColumn<string>(
                name: "Fecha",
                table: "Publicacion",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "EmpresaId",
                table: "Publicacion",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Publicacion_Empresa_EmpresaId",
                table: "Publicacion",
                column: "EmpresaId",
                principalTable: "Empresa",
                principalColumn: "idEmpresa",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
