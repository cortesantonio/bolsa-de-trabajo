using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace bolsadetrabajo.Migrations
{
    /// <inheritdoc />
    public partial class tblstats : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "NumeroContactoPublicacions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TrabajadorId = table.Column<int>(type: "int", nullable: false),
                    PublicacionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NumeroContactoPublicacions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NumeroContactoPublicacions_Publicacion_PublicacionId",
                        column: x => x.PublicacionId,
                        principalTable: "Publicacion",
                        principalColumn: "idPublicacion",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NumeroContactoPublicacions_Trabajador_TrabajadorId",
                        column: x => x.TrabajadorId,
                        principalTable: "Trabajador",
                        principalColumn: "idTrabajador",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PublicacionGuardada",
                columns: table => new
                {
                    idGuardado = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TrabajadorId = table.Column<int>(type: "int", nullable: false),
                    PublicacionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PublicacionGuardada", x => x.idGuardado);
                    table.ForeignKey(
                        name: "FK_PublicacionGuardada_Publicacion_PublicacionId",
                        column: x => x.PublicacionId,
                        principalTable: "Publicacion",
                        principalColumn: "idPublicacion",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PublicacionGuardada_Trabajador_TrabajadorId",
                        column: x => x.TrabajadorId,
                        principalTable: "Trabajador",
                        principalColumn: "idTrabajador",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VisitasPublicacion",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TrabajadorId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TrabajadoridTrabajador = table.Column<int>(type: "int", nullable: true),
                    PublicacionId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PublicacionidPublicacion = table.Column<int>(type: "int", nullable: true)
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
                name: "IX_NumeroContactoPublicacions_PublicacionId",
                table: "NumeroContactoPublicacions",
                column: "PublicacionId");

            migrationBuilder.CreateIndex(
                name: "IX_NumeroContactoPublicacions_TrabajadorId",
                table: "NumeroContactoPublicacions",
                column: "TrabajadorId");

            migrationBuilder.CreateIndex(
                name: "IX_PublicacionGuardada_PublicacionId",
                table: "PublicacionGuardada",
                column: "PublicacionId");

            migrationBuilder.CreateIndex(
                name: "IX_PublicacionGuardada_TrabajadorId",
                table: "PublicacionGuardada",
                column: "TrabajadorId");

            migrationBuilder.CreateIndex(
                name: "IX_VisitasPublicacion_PublicacionidPublicacion",
                table: "VisitasPublicacion",
                column: "PublicacionidPublicacion");

            migrationBuilder.CreateIndex(
                name: "IX_VisitasPublicacion_TrabajadoridTrabajador",
                table: "VisitasPublicacion",
                column: "TrabajadoridTrabajador");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NumeroContactoPublicacions");

            migrationBuilder.DropTable(
                name: "PublicacionGuardada");

            migrationBuilder.DropTable(
                name: "VisitasPublicacion");
        }
    }
}
