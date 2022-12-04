using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DemoApplication.Migrations
{
    public partial class Subnavbar : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
               name: "SubNavbars",
               columns: table => new
               {
                   Id = table.Column<int>(type: "int", nullable: false)
                       .Annotation("SqlServer:Identity", "1, 1"),
                   Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                   Order = table.Column<int>(type: "int", nullable: false),
                   Url = table.Column<string>(type: "nvarchar(max)", nullable: false),
                   NavbarId = table.Column<int>(type: "int", nullable: false)
               },
               constraints: table =>
               {
                   table.PrimaryKey("PK_SubNavbars", x => x.Id);
                   table.ForeignKey(
                       name: "FK_SubNavbars_Navbars_NavbarId",
                       column: x => x.NavbarId,
                       principalTable: "Navbars",
                       principalColumn: "Id",
                       onDelete: ReferentialAction.Cascade);
               });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
               name: "SubNavbars");

        }
    }
}
