using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiCar.Migrations
{
    /// <inheritdoc />
    public partial class AddRelacionamentoLaudoVeicular : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "VehicleReports",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NumberReport = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Chassi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EngineNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LicensePlate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Make = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Mileage = table.Column<int>(type: "int", nullable: false),
                    Color = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    YearOfManufacture = table.Column<int>(type: "int", nullable: false),
                    EChassiStatus = table.Column<int>(type: "int", nullable: false),
                    CarListingId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VehicleReports", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VehicleReports_CarListings_CarListingId",
                        column: x => x.CarListingId,
                        principalTable: "CarListings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_VehicleReports_CarListingId",
                table: "VehicleReports",
                column: "CarListingId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VehicleReports");
        }
    }
}
