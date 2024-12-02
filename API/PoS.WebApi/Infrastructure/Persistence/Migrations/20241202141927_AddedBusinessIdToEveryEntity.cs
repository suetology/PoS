using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PoS.WebApi.Migrations
{
    /// <inheritdoc />
    public partial class AddedBusinessIdToEveryEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "BusinessId",
                table: "Taxes",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "BusinessId",
                table: "Shifts",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "BusinessId",
                table: "Services",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "BusinessId",
                table: "ServiceCharges",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "BusinessId",
                table: "Reservations",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "BusinessId",
                table: "Refunds",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "BusinessId",
                table: "Payments",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "BusinessId",
                table: "Orders",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "BusinessId",
                table: "OrderItems",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "BusinessId",
                table: "Items",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "BusinessId",
                table: "ItemGroups",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "BusinessId",
                table: "Discounts",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "BusinessId",
                table: "Customers",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Taxes_BusinessId",
                table: "Taxes",
                column: "BusinessId");

            migrationBuilder.CreateIndex(
                name: "IX_Shifts_BusinessId",
                table: "Shifts",
                column: "BusinessId");

            migrationBuilder.CreateIndex(
                name: "IX_Services_BusinessId",
                table: "Services",
                column: "BusinessId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceCharges_BusinessId",
                table: "ServiceCharges",
                column: "BusinessId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_BusinessId",
                table: "Reservations",
                column: "BusinessId");

            migrationBuilder.CreateIndex(
                name: "IX_Refunds_BusinessId",
                table: "Refunds",
                column: "BusinessId");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_BusinessId",
                table: "Payments",
                column: "BusinessId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_BusinessId",
                table: "Orders",
                column: "BusinessId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_BusinessId",
                table: "OrderItems",
                column: "BusinessId");

            migrationBuilder.CreateIndex(
                name: "IX_Items_BusinessId",
                table: "Items",
                column: "BusinessId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemGroups_BusinessId",
                table: "ItemGroups",
                column: "BusinessId");

            migrationBuilder.CreateIndex(
                name: "IX_Discounts_BusinessId",
                table: "Discounts",
                column: "BusinessId");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_BusinessId",
                table: "Customers",
                column: "BusinessId");

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_Businesses_BusinessId",
                table: "Customers",
                column: "BusinessId",
                principalTable: "Businesses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Discounts_Businesses_BusinessId",
                table: "Discounts",
                column: "BusinessId",
                principalTable: "Businesses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ItemGroups_Businesses_BusinessId",
                table: "ItemGroups",
                column: "BusinessId",
                principalTable: "Businesses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Items_Businesses_BusinessId",
                table: "Items",
                column: "BusinessId",
                principalTable: "Businesses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItems_Businesses_BusinessId",
                table: "OrderItems",
                column: "BusinessId",
                principalTable: "Businesses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Businesses_BusinessId",
                table: "Orders",
                column: "BusinessId",
                principalTable: "Businesses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_Businesses_BusinessId",
                table: "Payments",
                column: "BusinessId",
                principalTable: "Businesses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Refunds_Businesses_BusinessId",
                table: "Refunds",
                column: "BusinessId",
                principalTable: "Businesses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_Businesses_BusinessId",
                table: "Reservations",
                column: "BusinessId",
                principalTable: "Businesses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceCharges_Businesses_BusinessId",
                table: "ServiceCharges",
                column: "BusinessId",
                principalTable: "Businesses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Services_Businesses_BusinessId",
                table: "Services",
                column: "BusinessId",
                principalTable: "Businesses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Shifts_Businesses_BusinessId",
                table: "Shifts",
                column: "BusinessId",
                principalTable: "Businesses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Taxes_Businesses_BusinessId",
                table: "Taxes",
                column: "BusinessId",
                principalTable: "Businesses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Customers_Businesses_BusinessId",
                table: "Customers");

            migrationBuilder.DropForeignKey(
                name: "FK_Discounts_Businesses_BusinessId",
                table: "Discounts");

            migrationBuilder.DropForeignKey(
                name: "FK_ItemGroups_Businesses_BusinessId",
                table: "ItemGroups");

            migrationBuilder.DropForeignKey(
                name: "FK_Items_Businesses_BusinessId",
                table: "Items");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderItems_Businesses_BusinessId",
                table: "OrderItems");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Businesses_BusinessId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Payments_Businesses_BusinessId",
                table: "Payments");

            migrationBuilder.DropForeignKey(
                name: "FK_Refunds_Businesses_BusinessId",
                table: "Refunds");

            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_Businesses_BusinessId",
                table: "Reservations");

            migrationBuilder.DropForeignKey(
                name: "FK_ServiceCharges_Businesses_BusinessId",
                table: "ServiceCharges");

            migrationBuilder.DropForeignKey(
                name: "FK_Services_Businesses_BusinessId",
                table: "Services");

            migrationBuilder.DropForeignKey(
                name: "FK_Shifts_Businesses_BusinessId",
                table: "Shifts");

            migrationBuilder.DropForeignKey(
                name: "FK_Taxes_Businesses_BusinessId",
                table: "Taxes");

            migrationBuilder.DropIndex(
                name: "IX_Taxes_BusinessId",
                table: "Taxes");

            migrationBuilder.DropIndex(
                name: "IX_Shifts_BusinessId",
                table: "Shifts");

            migrationBuilder.DropIndex(
                name: "IX_Services_BusinessId",
                table: "Services");

            migrationBuilder.DropIndex(
                name: "IX_ServiceCharges_BusinessId",
                table: "ServiceCharges");

            migrationBuilder.DropIndex(
                name: "IX_Reservations_BusinessId",
                table: "Reservations");

            migrationBuilder.DropIndex(
                name: "IX_Refunds_BusinessId",
                table: "Refunds");

            migrationBuilder.DropIndex(
                name: "IX_Payments_BusinessId",
                table: "Payments");

            migrationBuilder.DropIndex(
                name: "IX_Orders_BusinessId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_OrderItems_BusinessId",
                table: "OrderItems");

            migrationBuilder.DropIndex(
                name: "IX_Items_BusinessId",
                table: "Items");

            migrationBuilder.DropIndex(
                name: "IX_ItemGroups_BusinessId",
                table: "ItemGroups");

            migrationBuilder.DropIndex(
                name: "IX_Discounts_BusinessId",
                table: "Discounts");

            migrationBuilder.DropIndex(
                name: "IX_Customers_BusinessId",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "BusinessId",
                table: "Taxes");

            migrationBuilder.DropColumn(
                name: "BusinessId",
                table: "Shifts");

            migrationBuilder.DropColumn(
                name: "BusinessId",
                table: "Services");

            migrationBuilder.DropColumn(
                name: "BusinessId",
                table: "ServiceCharges");

            migrationBuilder.DropColumn(
                name: "BusinessId",
                table: "Reservations");

            migrationBuilder.DropColumn(
                name: "BusinessId",
                table: "Refunds");

            migrationBuilder.DropColumn(
                name: "BusinessId",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "BusinessId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "BusinessId",
                table: "OrderItems");

            migrationBuilder.DropColumn(
                name: "BusinessId",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "BusinessId",
                table: "ItemGroups");

            migrationBuilder.DropColumn(
                name: "BusinessId",
                table: "Discounts");

            migrationBuilder.DropColumn(
                name: "BusinessId",
                table: "Customers");
        }
    }
}
