using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiCar.Migrations
{
    /// <inheritdoc />
    public partial class AddConcessionaria : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DealershipId",
                table: "CarListings",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Dealerships",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Contact = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dealerships", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CarListings_DealershipId",
                table: "CarListings",
                column: "DealershipId");

            migrationBuilder.AddForeignKey(
                name: "FK_CarListings_Dealerships_DealershipId",
                table: "CarListings",
                column: "DealershipId",
                principalTable: "Dealerships",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CarListings_Dealerships_DealershipId",
                table: "CarListings");

            migrationBuilder.DropTable(
                name: "Dealerships");

            migrationBuilder.DropIndex(
                name: "IX_CarListings_DealershipId",
                table: "CarListings");

            migrationBuilder.DropColumn(
                name: "DealershipId",
                table: "CarListings");
        }
    }
}
