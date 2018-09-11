using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Pogodeo.DataAccess.Migrations
{
    public partial class WWOProvider : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "WWOWeatherId",
                table: "BigCitiesData",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "WWOWeather",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    LastUpdateDate = table.Column<DateTime>(nullable: false),
                    WeatherHourData = table.Column<string>(nullable: true),
                    WeatherDayData = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WWOWeather", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BigCitiesData_WWOWeatherId",
                table: "BigCitiesData",
                column: "WWOWeatherId");

            migrationBuilder.AddForeignKey(
                name: "FK_BigCitiesData_WWOWeather_WWOWeatherId",
                table: "BigCitiesData",
                column: "WWOWeatherId",
                principalTable: "WWOWeather",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BigCitiesData_WWOWeather_WWOWeatherId",
                table: "BigCitiesData");

            migrationBuilder.DropTable(
                name: "WWOWeather");

            migrationBuilder.DropIndex(
                name: "IX_BigCitiesData_WWOWeatherId",
                table: "BigCitiesData");

            migrationBuilder.DropColumn(
                name: "WWOWeatherId",
                table: "BigCitiesData");
        }
    }
}
