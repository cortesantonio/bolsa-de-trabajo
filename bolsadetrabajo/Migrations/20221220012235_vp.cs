using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace bolsadetrabajo.Migrations
{
    /// <inheritdoc />
    public partial class vp : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "VisitasPublicacion",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TrabajadorId = table.Column<int>(type: "int", nullable: false),
                    PublicacionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VisitasPublicacion", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VisitasPublicacion_Publicacion_PublicacionId",
                        column: x => x.PublicacionId,
                        principalTable: "Publicacion",
                        principalColumn: "idPublicacion",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VisitasPublicacion_Trabajador_TrabajadorId",
                        column: x => x.TrabajadorId,
                        principalTable: "Trabajador",
                        principalColumn: "idTrabajador",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_VisitasPublicacion_PublicacionId",
                table: "VisitasPublicacion",
                column: "PublicacionId");

            migrationBuilder.CreateIndex(
                name: "IX_VisitasPublicacion_TrabajadorId",
                table: "VisitasPublicacion",
                column: "TrabajadorId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VisitasPublicacion");
        }
    }
}
