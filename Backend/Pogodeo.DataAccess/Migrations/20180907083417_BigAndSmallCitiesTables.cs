using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Pogodeo.DataAccess.Migrations
{
    public partial class BigAndSmallCitiesTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CityLocalizationKeys");

            migrationBuilder.CreateTable(
                name: "BigCitiesData",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CityName = table.Column<string>(nullable: true),
                    Latitude = table.Column<string>(nullable: true),
                    Longitude = table.Column<string>(nullable: true),
                    AccuWeatherLocalizationKey = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BigCitiesData", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SmallCitiesData",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CityName = table.Column<string>(nullable: true),
                    Latitude = table.Column<string>(nullable: true),
                    Longitude = table.Column<string>(nullable: true),
                    AssociatedBigCityId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SmallCitiesData", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SmallCitiesData_BigCitiesData_AssociatedBigCityId",
                        column: x => x.AssociatedBigCityId,
                        principalTable: "BigCitiesData",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SmallCitiesData_AssociatedBigCityId",
                table: "SmallCitiesData",
                column: "AssociatedBigCityId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SmallCitiesData");

            migrationBuilder.DropTable(
                name: "BigCitiesData");

            migrationBuilder.CreateTable(
                name: "CityLocalizationKeys",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AccuWeatherLocalizationKey = table.Column<string>(nullable: true),
                    CityName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CityLocalizationKeys", x => x.Id);
                });
        }
    }
}
