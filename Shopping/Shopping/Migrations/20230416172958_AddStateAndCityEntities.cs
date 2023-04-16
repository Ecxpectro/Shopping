using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Shopping.Migrations
{
    /// <inheritdoc />
    public partial class AddStateAndCityEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "States",
                columns: table => new
                {
                    IdState = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StateName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CountryIdCountry = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_States", x => x.IdState);
                    table.ForeignKey(
                        name: "FK_States_Countries_CountryIdCountry",
                        column: x => x.CountryIdCountry,
                        principalTable: "Countries",
                        principalColumn: "IdCountry",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Cities",
                columns: table => new
                {
                    IdCity = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CityName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    StateIdState = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cities", x => x.IdCity);
                    table.ForeignKey(
                        name: "FK_Cities_States_StateIdState",
                        column: x => x.StateIdState,
                        principalTable: "States",
                        principalColumn: "IdState",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cities_CityName_StateIdState",
                table: "Cities",
                columns: new[] { "CityName", "StateIdState" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Cities_StateIdState",
                table: "Cities",
                column: "StateIdState");

            migrationBuilder.CreateIndex(
                name: "IX_States_CountryIdCountry",
                table: "States",
                column: "CountryIdCountry");

            migrationBuilder.CreateIndex(
                name: "IX_States_StateName_CountryIdCountry",
                table: "States",
                columns: new[] { "StateName", "CountryIdCountry" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cities");

            migrationBuilder.DropTable(
                name: "States");
        }
    }
}
