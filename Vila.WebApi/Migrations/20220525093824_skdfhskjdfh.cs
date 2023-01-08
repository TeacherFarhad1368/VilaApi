using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Vila.WebApi.Migrations
{
    public partial class skdfhskjdfh : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Dayprice",
                table: "Vilas");

            migrationBuilder.DropColumn(
                name: "SellPrice",
                table: "Vilas");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "Dayprice",
                table: "Vilas",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "SellPrice",
                table: "Vilas",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }
    }
}
