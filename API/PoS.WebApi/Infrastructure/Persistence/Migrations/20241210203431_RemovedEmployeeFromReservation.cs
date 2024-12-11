using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PoS.WebApi.Migrations
{
    /// <inheritdoc />
    public partial class RemovedEmployeeFromReservation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_Users_EmployeeId",
                table: "Reservations");

            migrationBuilder.DropIndex(
                name: "IX_Reservations_EmployeeId",
                table: "Reservations");

            migrationBuilder.DropColumn(
                name: "EmployeeId",
                table: "Reservations");

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "Reservations",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_UserId",
                table: "Reservations",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_Users_UserId",
                table: "Reservations",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_Users_UserId",
                table: "Reservations");

            migrationBuilder.DropIndex(
                name: "IX_Reservations_UserId",
                table: "Reservations");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Reservations");

            migrationBuilder.AddColumn<Guid>(
                name: "EmployeeId",
                table: "Reservations",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_EmployeeId",
                table: "Reservations",
                column: "EmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_Users_EmployeeId",
                table: "Reservations",
                column: "EmployeeId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
