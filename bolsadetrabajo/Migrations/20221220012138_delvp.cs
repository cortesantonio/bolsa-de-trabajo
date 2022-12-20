using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace bolsadetrabajo.Migrations
{
    /// <inheritdoc />
    public partial class delvp : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VisitasPublicacion");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "VisitasPublicacion",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PublicacionidPublicacion = table.Column<int>(type: "int", nullable: true),
                    TrabajadoridTrabajador = table.Column<int>(type: "int", nullable: true),
                    PublicacionId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TrabajadorId = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VisitasPublicacion", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VisitasPublicacion_Publicacion_PublicacionidPublicacion",
                        column: x => x.PublicacionidPublicacion,
                        principalTable: "Publicacion",
                        principalColumn: "idPublicacion");
                    table.ForeignKey(
                        name: "FK_VisitasPublicacion_Trabajador_TrabajadoridTrabajador",
                        column: x => x.TrabajadoridTrabajador,
                        principalTable: "Trabajador",
                        principalColumn: "idTrabajador");
                });

            migrationBuilder.CreateIndex(
                name: "IX_VisitasPublicacion_PublicacionidPublicacion",
                table: "VisitasPublicacion",
                column: "PublicacionidPublicacion");

            migrationBuilder.CreateIndex(
                name: "IX_VisitasPublicacion_TrabajadoridTrabajador",
                table: "VisitasPublicacion",
                column: "TrabajadoridTrabajador");
        }
    }
}
