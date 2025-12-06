using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MiTiendaDescuentos.Web.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddEntidadesMEP : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Sede",
                columns: table => new
                {
                    IdSede = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdColegio = table.Column<long>(type: "bigint", nullable: true),
                    Nombre = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Direccion = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Telefono = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Correo = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    InstitucionIdColegio = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sede", x => x.IdSede);
                    table.ForeignKey(
                        name: "FK_Sede_instituciones_InstitucionIdColegio",
                        column: x => x.InstitucionIdColegio,
                        principalTable: "instituciones",
                        principalColumn: "Id_Colegio");
                });

            migrationBuilder.CreateTable(
                name: "Cita",
                columns: table => new
                {
                    IdCita = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FechaCita = table.Column<DateTime>(type: "datetime2", nullable: false),
                    HoraCita = table.Column<TimeSpan>(type: "time", nullable: false),
                    NombreAgenda = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CorreoAgenda = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    TelefonoAgenda = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    CantidadCitas = table.Column<int>(type: "int", nullable: false),
                    IdColegio = table.Column<long>(type: "bigint", nullable: false),
                    IdSede = table.Column<int>(type: "int", nullable: false),
                    Estado = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    InstitucionIdColegio = table.Column<long>(type: "bigint", nullable: true),
                    SedeIdSede = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cita", x => x.IdCita);
                    table.ForeignKey(
                        name: "FK_Cita_Sede_SedeIdSede",
                        column: x => x.SedeIdSede,
                        principalTable: "Sede",
                        principalColumn: "IdSede");
                    table.ForeignKey(
                        name: "FK_Cita_instituciones_InstitucionIdColegio",
                        column: x => x.InstitucionIdColegio,
                        principalTable: "instituciones",
                        principalColumn: "Id_Colegio");
                });

            migrationBuilder.CreateTable(
                name: "Grado",
                columns: table => new
                {
                    IdGrado = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdSede = table.Column<long>(type: "bigint", nullable: true),
                    NombreGrado = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    SedeIdSede = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Grado", x => x.IdGrado);
                    table.ForeignKey(
                        name: "FK_Grado_Sede_SedeIdSede",
                        column: x => x.SedeIdSede,
                        principalTable: "Sede",
                        principalColumn: "IdSede");
                });

            migrationBuilder.CreateTable(
                name: "Cupo",
                columns: table => new
                {
                    IdCupo = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdSede = table.Column<long>(type: "bigint", nullable: true),
                    IdGrado = table.Column<long>(type: "bigint", nullable: true),
                    CuposTotales = table.Column<int>(type: "int", nullable: false),
                    CuposOcupados = table.Column<int>(type: "int", nullable: false),
                    SedeIdSede = table.Column<long>(type: "bigint", nullable: true),
                    GradoIdGrado = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cupo", x => x.IdCupo);
                    table.ForeignKey(
                        name: "FK_Cupo_Grado_GradoIdGrado",
                        column: x => x.GradoIdGrado,
                        principalTable: "Grado",
                        principalColumn: "IdGrado");
                    table.ForeignKey(
                        name: "FK_Cupo_Sede_SedeIdSede",
                        column: x => x.SedeIdSede,
                        principalTable: "Sede",
                        principalColumn: "IdSede");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cita_InstitucionIdColegio",
                table: "Cita",
                column: "InstitucionIdColegio");

            migrationBuilder.CreateIndex(
                name: "IX_Cita_SedeIdSede",
                table: "Cita",
                column: "SedeIdSede");

            migrationBuilder.CreateIndex(
                name: "IX_Cupo_GradoIdGrado",
                table: "Cupo",
                column: "GradoIdGrado");

            migrationBuilder.CreateIndex(
                name: "IX_Cupo_SedeIdSede",
                table: "Cupo",
                column: "SedeIdSede");

            migrationBuilder.CreateIndex(
                name: "IX_Grado_SedeIdSede",
                table: "Grado",
                column: "SedeIdSede");

            migrationBuilder.CreateIndex(
                name: "IX_Sede_InstitucionIdColegio",
                table: "Sede",
                column: "InstitucionIdColegio");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cita");

            migrationBuilder.DropTable(
                name: "Cupo");

            migrationBuilder.DropTable(
                name: "Grado");

            migrationBuilder.DropTable(
                name: "Sede");
        }
    }
}
