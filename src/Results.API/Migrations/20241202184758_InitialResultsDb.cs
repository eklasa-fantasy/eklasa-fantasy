using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Results.API.Migrations
{
    /// <inheritdoc />
    public partial class InitialResultsDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Subs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Results",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MatchId = table.Column<int>(type: "int", nullable: false),
                    Time = table.Column<DateTime>(type: "datetime2", nullable: false),
                    HomeTeamName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AwayTeamName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HomeTeamId = table.Column<int>(type: "int", nullable: false),
                    AwayTeamId = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Round = table.Column<int>(type: "int", nullable: false),
                    HomeTeamBadge = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AwayTeamBadge = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HomeTeamScore = table.Column<int>(type: "int", nullable: false),
                    AwayTeamScore = table.Column<int>(type: "int", nullable: false),
                    isMatchLive = table.Column<bool>(type: "bit", nullable: false),
                    SubstitutionsId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Results", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Results_Subs_SubstitutionsId",
                        column: x => x.SubstitutionsId,
                        principalTable: "Subs",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "SubsAway",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Time = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Substitution = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SubsId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubsAway", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubsAway_Subs_SubsId",
                        column: x => x.SubsId,
                        principalTable: "Subs",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "SubsHome",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Time = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Substitution = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SubsId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubsHome", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubsHome_Subs_SubsId",
                        column: x => x.SubsId,
                        principalTable: "Subs",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Cards",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TimeReceived = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HomeFault = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AwayFault = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CardType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ResultId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cards", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cards_Results_ResultId",
                        column: x => x.ResultId,
                        principalTable: "Results",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Goalscorers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TimeScored = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HomeScorer = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AwayScorer = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HomeAssist = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AwayAssist = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Score = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ResultId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Goalscorers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Goalscorers_Results_ResultId",
                        column: x => x.ResultId,
                        principalTable: "Results",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cards_ResultId",
                table: "Cards",
                column: "ResultId");

            migrationBuilder.CreateIndex(
                name: "IX_Goalscorers_ResultId",
                table: "Goalscorers",
                column: "ResultId");

            migrationBuilder.CreateIndex(
                name: "IX_Results_SubstitutionsId",
                table: "Results",
                column: "SubstitutionsId");

            migrationBuilder.CreateIndex(
                name: "IX_SubsAway_SubsId",
                table: "SubsAway",
                column: "SubsId");

            migrationBuilder.CreateIndex(
                name: "IX_SubsHome_SubsId",
                table: "SubsHome",
                column: "SubsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cards");

            migrationBuilder.DropTable(
                name: "Goalscorers");

            migrationBuilder.DropTable(
                name: "SubsAway");

            migrationBuilder.DropTable(
                name: "SubsHome");

            migrationBuilder.DropTable(
                name: "Results");

            migrationBuilder.DropTable(
                name: "Subs");
        }
    }
}
