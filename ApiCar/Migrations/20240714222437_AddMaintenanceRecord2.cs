using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiCar.Migrations
{
    /// <inheritdoc />
    public partial class AddMaintenanceRecord2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MaintenanceRecords_Cars_CarId",
                table: "MaintenanceRecords");

            migrationBuilder.DropIndex(
                name: "IX_MaintenanceRecords_CarId",
                table: "MaintenanceRecords");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_MaintenanceRecords_CarId",
                table: "MaintenanceRecords",
                column: "CarId");

            migrationBuilder.AddForeignKey(
                name: "FK_MaintenanceRecords_Cars_CarId",
                table: "MaintenanceRecords",
                column: "CarId",
                principalTable: "Cars",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
