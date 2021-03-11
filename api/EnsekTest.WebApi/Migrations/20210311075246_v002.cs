using Microsoft.EntityFrameworkCore.Migrations;

namespace EnsekTest.WebApi.Migrations
{
    public partial class v002 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_MeterReadings",
                table: "MeterReadings");

            migrationBuilder.AddColumn<int>(
                name: "MeterReadingId",
                table: "MeterReadings",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0)
                .Annotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_MeterReadings",
                table: "MeterReadings",
                column: "MeterReadingId");

            migrationBuilder.CreateIndex(
                name: "IX_MeterReadings_AccountId_MeterReadingDateTime",
                table: "MeterReadings",
                columns: new[] { "AccountId", "MeterReadingDateTime" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_MeterReadings",
                table: "MeterReadings");

            migrationBuilder.DropIndex(
                name: "IX_MeterReadings_AccountId_MeterReadingDateTime",
                table: "MeterReadings");

            migrationBuilder.DropColumn(
                name: "MeterReadingId",
                table: "MeterReadings");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MeterReadings",
                table: "MeterReadings",
                columns: new[] { "AccountId", "MeterReadingDateTime" });
        }
    }
}
