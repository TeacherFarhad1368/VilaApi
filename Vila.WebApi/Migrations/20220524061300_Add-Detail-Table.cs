using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Vila.WebApi.Migrations
{
    public partial class AddDetailTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Detail",
                columns: table => new
                {
                    DetailId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VilaId = table.Column<int>(type: "int", nullable: false),
                    What = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Value = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Detail", x => x.DetailId);
                    table.ForeignKey(
                        name: "FK_Detail_Vilas_VilaId",
                        column: x => x.VilaId,
                        principalTable: "Vilas",
                        principalColumn: "VilaId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Detail_VilaId",
                table: "Detail",
                column: "VilaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Detail");
        }
    }
}
