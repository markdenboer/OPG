using Microsoft.EntityFrameworkCore.Migrations;

namespace OPG.Migrations
{
    public partial class Version01 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "BoxHeight",
                table: "Orders",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "BoxLength",
                table: "Orders",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "BoxWidth",
                table: "Orders",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BoxHeight",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "BoxLength",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "BoxWidth",
                table: "Orders");
        }
    }
}
