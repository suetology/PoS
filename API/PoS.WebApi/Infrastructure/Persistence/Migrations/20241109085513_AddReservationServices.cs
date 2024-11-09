using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PoS.WebApi.Migrations
{
    /// <inheritdoc />
    public partial class AddReservationServices : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ReservationId",
                table: "ServiceCharges",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ServiceId",
                table: "Reservations",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "ReservationId",
                table: "OrderServices",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ServiceCharges_ReservationId",
                table: "ServiceCharges",
                column: "ReservationId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_ServiceId",
                table: "Reservations",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderServices_ReservationId",
                table: "OrderServices",
                column: "ReservationId");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropIndex(
                name: "IX_ServiceCharges_ReservationId",
                table: "ServiceCharges");

            migrationBuilder.DropIndex(
                name: "IX_Reservations_ServiceId",
                table: "Reservations");

            migrationBuilder.DropIndex(
                name: "IX_OrderServices_ReservationId",
                table: "OrderServices");

            migrationBuilder.DropColumn(
                name: "ReservationId",
                table: "ServiceCharges");

            migrationBuilder.DropColumn(
                name: "ServiceId",
                table: "Reservations");

            migrationBuilder.DropColumn(
                name: "ReservationId",
                table: "OrderServices");
        }
    }
}
