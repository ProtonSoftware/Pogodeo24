using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Pogodeo.DataAccess.Migrations
{
    public partial class WeatherTablesComplete : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AccuWeatherWeather_BigCitiesData_AssociatedBigCityId",
                table: "AccuWeatherWeather");

            migrationBuilder.DropIndex(
                name: "IX_AccuWeatherWeather_AssociatedBigCityId",
                table: "AccuWeatherWeather");

            migrationBuilder.DropColumn(
                name: "AssociatedBigCityId",
                table: "AccuWeatherWeather");

            migrationBuilder.RenameColumn(
                name: "WeatherData",
                table: "AccuWeatherWeather",
                newName: "WeatherHourData");

            migrationBuilder.AddColumn<int>(
                name: "AccuWeatherWeatherId",
                table: "BigCitiesData",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AerisWeatherWeatherId",
                table: "BigCitiesData",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WeatherDayData",
                table: "AccuWeatherWeather",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "AerisWeatherWeather",
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
                    table.PrimaryKey("PK_AerisWeatherWeather", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BigCitiesData_AccuWeatherWeatherId",
                table: "BigCitiesData",
                column: "AccuWeatherWeatherId");

            migrationBuilder.CreateIndex(
                name: "IX_BigCitiesData_AerisWeatherWeatherId",
                table: "BigCitiesData",
                column: "AerisWeatherWeatherId");

            migrationBuilder.AddForeignKey(
                name: "FK_BigCitiesData_AccuWeatherWeather_AccuWeatherWeatherId",
                table: "BigCitiesData",
                column: "AccuWeatherWeatherId",
                principalTable: "AccuWeatherWeather",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BigCitiesData_AerisWeatherWeather_AerisWeatherWeatherId",
                table: "BigCitiesData",
                column: "AerisWeatherWeatherId",
                principalTable: "AerisWeatherWeather",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BigCitiesData_AccuWeatherWeather_AccuWeatherWeatherId",
                table: "BigCitiesData");

            migrationBuilder.DropForeignKey(
                name: "FK_BigCitiesData_AerisWeatherWeather_AerisWeatherWeatherId",
                table: "BigCitiesData");

            migrationBuilder.DropTable(
                name: "AerisWeatherWeather");

            migrationBuilder.DropIndex(
                name: "IX_BigCitiesData_AccuWeatherWeatherId",
                table: "BigCitiesData");

            migrationBuilder.DropIndex(
                name: "IX_BigCitiesData_AerisWeatherWeatherId",
                table: "BigCitiesData");

            migrationBuilder.DropColumn(
                name: "AccuWeatherWeatherId",
                table: "BigCitiesData");

            migrationBuilder.DropColumn(
                name: "AerisWeatherWeatherId",
                table: "BigCitiesData");

            migrationBuilder.DropColumn(
                name: "WeatherDayData",
                table: "AccuWeatherWeather");

            migrationBuilder.RenameColumn(
                name: "WeatherHourData",
                table: "AccuWeatherWeather",
                newName: "WeatherData");

            migrationBuilder.AddColumn<int>(
                name: "AssociatedBigCityId",
                table: "AccuWeatherWeather",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AccuWeatherWeather_AssociatedBigCityId",
                table: "AccuWeatherWeather",
                column: "AssociatedBigCityId");

            migrationBuilder.AddForeignKey(
                name: "FK_AccuWeatherWeather_BigCitiesData_AssociatedBigCityId",
                table: "AccuWeatherWeather",
                column: "AssociatedBigCityId",
                principalTable: "BigCitiesData",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
