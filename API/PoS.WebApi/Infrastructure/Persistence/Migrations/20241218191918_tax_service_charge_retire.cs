using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PoS.WebApi.Migrations
{
    /// <inheritdoc />
    public partial class tax_service_charge_retire : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Retired",
                table: "Taxes",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Retired",
                table: "ServiceCharges",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Retired",
                table: "Taxes");

            migrationBuilder.DropColumn(
                name: "Retired",
                table: "ServiceCharges");
        }
    }
}
