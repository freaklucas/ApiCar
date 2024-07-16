using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiCar.Migrations
{
    /// <inheritdoc />
    public partial class AddPeopleCoverageEnumToInsurancePolicy : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Deductible",
                table: "InsurancePolicy",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "MaxCoverage",
                table: "InsurancePolicy",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "PeopleCoverage",
                table: "InsurancePolicy",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Deductible",
                table: "InsurancePolicy");

            migrationBuilder.DropColumn(
                name: "MaxCoverage",
                table: "InsurancePolicy");

            migrationBuilder.DropColumn(
                name: "PeopleCoverage",
                table: "InsurancePolicy");
        }
    }
}
