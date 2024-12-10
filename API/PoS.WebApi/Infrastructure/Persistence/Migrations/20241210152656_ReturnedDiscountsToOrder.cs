using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PoS.WebApi.Migrations
{
    /// <inheritdoc />
    public partial class ReturnedDiscountsToOrder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "DiscountId",
                table: "Orders",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Orders_DiscountId",
                table: "Orders",
                column: "DiscountId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Discounts_DiscountId",
                table: "Orders",
                column: "DiscountId",
                principalTable: "Discounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Discounts_DiscountId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_DiscountId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "DiscountId",
                table: "Orders");
        }
    }
}
