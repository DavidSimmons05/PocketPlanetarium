using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PocketPlanetarium.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedDatabaseGotBareBonesWorking : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Planet",
                columns: table => new
                {
                    planetId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Distance = table.Column<double>(type: "float", nullable: false),
                    Mass = table.Column<double>(type: "float", nullable: false),
                    StellarMagnitude = table.Column<double>(type: "float", nullable: false),
                    DiscoveryDate = table.Column<DateOnly>(type: "date", nullable: false),
                    ImagePath = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Planet", x => x.planetId);
                });

            migrationBuilder.CreateTable(
                name: "Planetarium",
                columns: table => new
                {
                    planetariumId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    userId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Planetarium", x => x.planetariumId);
                    table.ForeignKey(
                        name: "FK_Planetarium_AspNetUsers_userId",
                        column: x => x.userId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Star",
                columns: table => new
                {
                    starId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    distance = table.Column<double>(type: "float", nullable: false),
                    mass = table.Column<double>(type: "float", nullable: false),
                    StellarMagnitude = table.Column<double>(type: "float", nullable: false),
                    DiscoveryDate = table.Column<DateOnly>(type: "date", nullable: false),
                    ImagePath = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Star", x => x.starId);
                });

            migrationBuilder.CreateTable(
                name: "PersonalizedPlanet",
                columns: table => new
                {
                    personalizedPlanetId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    planetId = table.Column<int>(type: "int", nullable: false),
                    planetariumId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonalizedPlanet", x => x.personalizedPlanetId);
                    table.ForeignKey(
                        name: "FK_PersonalizedPlanet_Planet_planetId",
                        column: x => x.planetId,
                        principalTable: "Planet",
                        principalColumn: "planetId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PersonalizedPlanet_Planetarium_planetariumId",
                        column: x => x.planetariumId,
                        principalTable: "Planetarium",
                        principalColumn: "planetariumId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PersonalizedStar",
                columns: table => new
                {
                    personalizedStarId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    starId = table.Column<int>(type: "int", nullable: false),
                    planetariumId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonalizedStar", x => x.personalizedStarId);
                    table.ForeignKey(
                        name: "FK_PersonalizedStar_Planetarium_planetariumId",
                        column: x => x.planetariumId,
                        principalTable: "Planetarium",
                        principalColumn: "planetariumId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PersonalizedStar_Star_starId",
                        column: x => x.starId,
                        principalTable: "Star",
                        principalColumn: "starId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PersonalizedPlanet_planetariumId",
                table: "PersonalizedPlanet",
                column: "planetariumId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonalizedPlanet_planetId",
                table: "PersonalizedPlanet",
                column: "planetId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonalizedStar_planetariumId",
                table: "PersonalizedStar",
                column: "planetariumId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonalizedStar_starId",
                table: "PersonalizedStar",
                column: "starId");

            migrationBuilder.CreateIndex(
                name: "IX_Planetarium_userId",
                table: "Planetarium",
                column: "userId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PersonalizedPlanet");

            migrationBuilder.DropTable(
                name: "PersonalizedStar");

            migrationBuilder.DropTable(
                name: "Planet");

            migrationBuilder.DropTable(
                name: "Planetarium");

            migrationBuilder.DropTable(
                name: "Star");
        }
    }
}
