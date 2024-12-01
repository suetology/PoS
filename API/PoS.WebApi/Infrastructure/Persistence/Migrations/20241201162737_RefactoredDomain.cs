using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PoS.WebApi.Migrations
{
    /// <inheritdoc />
    public partial class RefactoredDomain : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderServices_Reservations_ReservationId",
                table: "OrderServices");

            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_Services_ServiceId",
                table: "Reservations");

            migrationBuilder.DropForeignKey(
                name: "FK_ServiceCharges_Reservations_ReservationId",
                table: "ServiceCharges");

            migrationBuilder.DropTable(
                name: "GroupDiscounts");

            migrationBuilder.DropTable(
                name: "ItemDiscounts");

            migrationBuilder.DropTable(
                name: "ItemTaxes");

            migrationBuilder.CreateTable(
                name: "DiscountItem",
                columns: table => new
                {
                    DiscountsId = table.Column<Guid>(type: "TEXT", nullable: false),
                    ItemsId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiscountItem", x => new { x.DiscountsId, x.ItemsId });
                    table.ForeignKey(
                        name: "FK_DiscountItem_Discounts_DiscountsId",
                        column: x => x.DiscountsId,
                        principalTable: "Discounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DiscountItem_Items_ItemsId",
                        column: x => x.ItemsId,
                        principalTable: "Items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DiscountItemGroup",
                columns: table => new
                {
                    DiscountsId = table.Column<Guid>(type: "TEXT", nullable: false),
                    ItemGroupsId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiscountItemGroup", x => new { x.DiscountsId, x.ItemGroupsId });
                    table.ForeignKey(
                        name: "FK_DiscountItemGroup_Discounts_DiscountsId",
                        column: x => x.DiscountsId,
                        principalTable: "Discounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DiscountItemGroup_ItemGroups_ItemGroupsId",
                        column: x => x.ItemGroupsId,
                        principalTable: "ItemGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ItemTax",
                columns: table => new
                {
                    ItemId = table.Column<Guid>(type: "TEXT", nullable: false),
                    TaxesId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemTax", x => new { x.ItemId, x.TaxesId });
                    table.ForeignKey(
                        name: "FK_ItemTax_Items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ItemTax_Taxes_TaxesId",
                        column: x => x.TaxesId,
                        principalTable: "Taxes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DiscountItem_ItemsId",
                table: "DiscountItem",
                column: "ItemsId");

            migrationBuilder.CreateIndex(
                name: "IX_DiscountItemGroup_ItemGroupsId",
                table: "DiscountItemGroup",
                column: "ItemGroupsId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemTax_TaxesId",
                table: "ItemTax",
                column: "TaxesId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DiscountItem");

            migrationBuilder.DropTable(
                name: "DiscountItemGroup");

            migrationBuilder.DropTable(
                name: "ItemTax");
            
            migrationBuilder.CreateTable(
                name: "GroupDiscounts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    DiscountId = table.Column<Guid>(type: "TEXT", nullable: false),
                    ItemGroupId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupDiscounts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GroupDiscounts_Discounts_DiscountId",
                        column: x => x.DiscountId,
                        principalTable: "Discounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GroupDiscounts_ItemGroups_ItemGroupId",
                        column: x => x.ItemGroupId,
                        principalTable: "ItemGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ItemDiscounts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    DiscountId = table.Column<Guid>(type: "TEXT", nullable: false),
                    ItemId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemDiscounts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ItemDiscounts_Discounts_DiscountId",
                        column: x => x.DiscountId,
                        principalTable: "Discounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ItemDiscounts_Items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ItemTaxes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    ItemId = table.Column<Guid>(type: "TEXT", nullable: false),
                    TaxId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemTaxes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ItemTaxes_Items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ItemTaxes_Taxes_TaxId",
                        column: x => x.TaxId,
                        principalTable: "Taxes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GroupDiscounts_DiscountId",
                table: "GroupDiscounts",
                column: "DiscountId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupDiscounts_ItemGroupId",
                table: "GroupDiscounts",
                column: "ItemGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemDiscounts_DiscountId",
                table: "ItemDiscounts",
                column: "DiscountId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemDiscounts_ItemId",
                table: "ItemDiscounts",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemTaxes_ItemId",
                table: "ItemTaxes",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemTaxes_TaxId",
                table: "ItemTaxes",
                column: "TaxId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderServices_Reservations_ReservationId",
                table: "OrderServices",
                column: "ReservationId",
                principalTable: "Reservations",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_Services_ServiceId",
                table: "Reservations",
                column: "ServiceId",
                principalTable: "Services",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceCharges_Reservations_ReservationId",
                table: "ServiceCharges",
                column: "ReservationId",
                principalTable: "Reservations",
                principalColumn: "Id");
        }
    }
}
