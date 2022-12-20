using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace bolsadetrabajo.Migrations
{
    /// <inheritdoc />
    public partial class stats : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PublicacionGuardada",
                columns: table => new
                {
                    idGuardado = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idTrabajador = table.Column<int>(type: "int", nullable: false),
                    TrabajadoridTrabajador = table.Column<int>(type: "int", nullable: true),
                    idPublicacion = table.Column<int>(type: "int", nullable: false),
                    PublicacionidPublicacion = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PublicacionGuardada", x => x.idGuardado);
                    table.ForeignKey(
                        name: "FK_PublicacionGuardada_Publicacion_PublicacionidPublicacion",
                        column: x => x.PublicacionidPublicacion,
                        principalTable: "Publicacion",
                        principalColumn: "idPublicacion");
                    table.ForeignKey(
                        name: "FK_PublicacionGuardada_Trabajador_TrabajadoridTrabajador",
                        column: x => x.TrabajadoridTrabajador,
                        principalTable: "Trabajador",
                        principalColumn: "idTrabajador");
                });

            migrationBuilder.CreateIndex(
                name: "IX_PublicacionGuardada_PublicacionidPublicacion",
                table: "PublicacionGuardada",
                column: "PublicacionidPublicacion");

            migrationBuilder.CreateIndex(
                name: "IX_PublicacionGuardada_TrabajadoridTrabajador",
                table: "PublicacionGuardada",
                column: "TrabajadoridTrabajador");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PublicacionGuardada");
        }
    }
}
