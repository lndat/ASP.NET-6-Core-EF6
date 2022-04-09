using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace csm6.Migrations
{
    public partial class AddData2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Players",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Age", "Aim", "Experience", "PlayerName" },
                values: new object[] { 25, 8, 8, "CNN" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Players",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Age", "Aim", "Experience", "PlayerName" },
                values: new object[] { 18, 5, 5, "r@di0" });
        }
    }
}
