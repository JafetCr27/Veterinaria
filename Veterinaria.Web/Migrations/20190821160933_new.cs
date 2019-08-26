using Microsoft.EntityFrameworkCore.Migrations;

namespace Veterinaria.Web.Migrations
{
    public partial class @new : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CellPhone",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "FixedPhone",
                table: "AspNetUsers");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CellPhone",
                table: "AspNetUsers",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FixedPhone",
                table: "AspNetUsers",
                maxLength: 20,
                nullable: false,
                defaultValue: "");
        }
    }
}
