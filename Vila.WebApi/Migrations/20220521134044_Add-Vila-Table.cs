using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Vila.WebApi.Migrations
{
    public partial class AddVilaTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Vilas",
                columns: table => new
                {
                    VilaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    State = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    City = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    address = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Mobile = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: false),
                    BuildDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vilas", x => x.VilaId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Vilas");
        }
    }
}
