using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Pogodeo.DataAccess.Migrations
{
    public partial class TestMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TestField1",
                table: "Forecasts");

            migrationBuilder.DropColumn(
                name: "TestField2",
                table: "Forecasts");

            migrationBuilder.DropColumn(
                name: "TestField3",
                table: "Forecasts");

            migrationBuilder.DropColumn(
                name: "TestField4",
                table: "Forecasts");

            migrationBuilder.AddColumn<DateTime>(
                name: "EventDate",
                table: "Forecasts",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<float>(
                name: "TempEvening",
                table: "Forecasts",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "TempMorning",
                table: "Forecasts",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "TempNoon",
                table: "Forecasts",
                nullable: false,
                defaultValue: 0f);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EventDate",
                table: "Forecasts");

            migrationBuilder.DropColumn(
                name: "TempEvening",
                table: "Forecasts");

            migrationBuilder.DropColumn(
                name: "TempMorning",
                table: "Forecasts");

            migrationBuilder.DropColumn(
                name: "TempNoon",
                table: "Forecasts");

            migrationBuilder.AddColumn<string>(
                name: "TestField1",
                table: "Forecasts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TestField2",
                table: "Forecasts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TestField3",
                table: "Forecasts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TestField4",
                table: "Forecasts",
                nullable: true);
        }
    }
}
