using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MiTiendaDescuentos.Web.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddInstitucionesToDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "instituciones",
                columns: table => new
                {
                    Id_Colegio = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    localidad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    direccion_principal = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    correo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    telefono = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    cantidad_sedes = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_instituciones", x => x.Id_Colegio);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "instituciones");
        }
    }
}
