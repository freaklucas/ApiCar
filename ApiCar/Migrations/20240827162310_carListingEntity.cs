using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiCar.Migrations
{
    /// <inheritdoc />
    public partial class carListingEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CarListing_Cars_CarId",
                table: "CarListing");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CarListing",
                table: "CarListing");

            migrationBuilder.RenameTable(
                name: "CarListing",
                newName: "CarListings");

            migrationBuilder.RenameIndex(
                name: "IX_CarListing_CarId",
                table: "CarListings",
                newName: "IX_CarListings_CarId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CarListings",
                table: "CarListings",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CarListings_Cars_CarId",
                table: "CarListings",
                column: "CarId",
                principalTable: "Cars",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CarListings_Cars_CarId",
                table: "CarListings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CarListings",
                table: "CarListings");

            migrationBuilder.RenameTable(
                name: "CarListings",
                newName: "CarListing");

            migrationBuilder.RenameIndex(
                name: "IX_CarListings_CarId",
                table: "CarListing",
                newName: "IX_CarListing_CarId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CarListing",
                table: "CarListing",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CarListing_Cars_CarId",
                table: "CarListing",
                column: "CarId",
                principalTable: "Cars",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
