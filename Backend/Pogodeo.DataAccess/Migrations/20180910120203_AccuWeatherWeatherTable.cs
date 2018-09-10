using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Pogodeo.DataAccess.Migrations
{
    public partial class AccuWeatherWeatherTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AccuWeatherWeather",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    LastUpdateDate = table.Column<DateTime>(nullable: false),
                    WeatherData = table.Column<string>(nullable: true),
                    AssociatedBigCityId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccuWeatherWeather", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AccuWeatherWeather_BigCitiesData_AssociatedBigCityId",
                        column: x => x.AssociatedBigCityId,
                        principalTable: "BigCitiesData",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AccuWeatherWeather_AssociatedBigCityId",
                table: "AccuWeatherWeather",
                column: "AssociatedBigCityId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccuWeatherWeather");
        }
    }
}
