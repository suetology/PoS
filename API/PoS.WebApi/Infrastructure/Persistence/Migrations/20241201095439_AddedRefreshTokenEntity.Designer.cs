﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PoS.WebApi.Infrastructure.Persistence;

#nullable disable

namespace PoS.WebApi.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    [Migration("20241201095439_AddedRefreshTokenEntity")]
    partial class AddedRefreshTokenEntity
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.10");

            modelBuilder.Entity("PoS.WebApi.Domain.Entities.Business", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Address")
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("VARCHAR(20)");

                    b.HasKey("Id");

                    b.ToTable("Businesses");
                });

            modelBuilder.Entity("PoS.WebApi.Domain.Entities.Customer", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("VARCHAR(20)");

                    b.HasKey("Id");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("PoS.WebApi.Domain.Entities.Discount", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<int>("AmountAvailable")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsPercentage")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("ValidFrom")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("ValidTo")
                        .HasColumnType("TEXT");

                    b.Property<decimal>("Value")
                        .HasColumnType("DECIMAL(5, 2)");

                    b.HasKey("Id");

                    b.ToTable("Discounts");
                });

            modelBuilder.Entity("PoS.WebApi.Domain.Entities.GroupDiscount", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("DiscountId")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("ItemGroupId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("DiscountId");

                    b.HasIndex("ItemGroupId");

                    b.ToTable("GroupDiscounts");
                });

            modelBuilder.Entity("PoS.WebApi.Domain.Entities.Item", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<byte[]>("Image")
                        .IsRequired()
                        .HasColumnType("BLOB");

                    b.Property<Guid?>("ItemGroupId")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<decimal>("Price")
                        .HasColumnType("TEXT");

                    b.Property<int>("Stock")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("ItemGroupId");

                    b.ToTable("Items");
                });

            modelBuilder.Entity("PoS.WebApi.Domain.Entities.ItemDiscount", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("DiscountId")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("ItemId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("DiscountId");

                    b.HasIndex("ItemId");

                    b.ToTable("ItemDiscounts");
                });

            modelBuilder.Entity("PoS.WebApi.Domain.Entities.ItemGroup", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("ItemGroups");
                });

            modelBuilder.Entity("PoS.WebApi.Domain.Entities.ItemTax", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("ItemId")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("TaxId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("ItemId");

                    b.HasIndex("TaxId");

                    b.ToTable("ItemTaxes");
                });

            modelBuilder.Entity("PoS.WebApi.Domain.Entities.Order", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("Closed")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("Created")
                        .HasColumnType("TEXT");

                    b.Property<decimal>("DiscountAmount")
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("DiscountId")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("EmployeeId")
                        .HasColumnType("TEXT");

                    b.Property<decimal>("FinalAmount")
                        .HasColumnType("TEXT");

                    b.Property<decimal>("PaidAmount")
                        .HasColumnType("TEXT");

                    b.Property<decimal>("ServiceChargeAmount")
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("ServiceChargeId")
                        .HasColumnType("TEXT");

                    b.Property<int>("Status")
                        .HasColumnType("INTEGER");

                    b.Property<decimal?>("TipAmount")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("DiscountId");

                    b.HasIndex("EmployeeId");

                    b.HasIndex("ServiceChargeId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("PoS.WebApi.Domain.Entities.OrderItem", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("ItemId")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("OrderId")
                        .HasColumnType("TEXT");

                    b.Property<decimal>("Price")
                        .HasColumnType("TEXT");

                    b.Property<int>("Quantity")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasDefaultValue(0);

                    b.HasKey("Id");

                    b.HasIndex("ItemId");

                    b.HasIndex("OrderId");

                    b.ToTable("OrderItems");
                });

            modelBuilder.Entity("PoS.WebApi.Domain.Entities.OrderService", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("OrderId")
                        .HasColumnType("TEXT");

                    b.Property<decimal>("Price")
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("ReservationId")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("ServiceId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("OrderId");

                    b.HasIndex("ReservationId");

                    b.HasIndex("ServiceId");

                    b.ToTable("OrderServices");
                });

            modelBuilder.Entity("PoS.WebApi.Domain.Entities.Payment", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<decimal>("Amount")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("Date")
                        .HasColumnType("TEXT");

                    b.Property<int>("Method")
                        .HasColumnType("INTEGER");

                    b.Property<Guid>("OrderId")
                        .HasColumnType("TEXT");

                    b.Property<string>("StripeTransactionId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("OrderId");

                    b.ToTable("Payments");
                });

            modelBuilder.Entity("PoS.WebApi.Domain.Entities.RefreshToken", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("AccessToken")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("Expires")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("RefreshTokens");
                });

            modelBuilder.Entity("PoS.WebApi.Domain.Entities.Refund", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<decimal>("Amount")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("Date")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("OrderId")
                        .HasColumnType("TEXT");

                    b.Property<string>("Reason")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("OrderId")
                        .IsUnique();

                    b.ToTable("Refunds");
                });

            modelBuilder.Entity("PoS.WebApi.Domain.Entities.Reservation", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("AppointmentTime")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("CustomerId")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("EmployeeId")
                        .HasColumnType("TEXT");

                    b.Property<bool>("NotificationSent")
                        .HasColumnType("INTEGER");

                    b.Property<Guid>("OrderId")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("ReservationTime")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("ServiceId")
                        .HasColumnType("TEXT");

                    b.Property<int>("Status")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.HasIndex("EmployeeId");

                    b.HasIndex("OrderId")
                        .IsUnique();

                    b.HasIndex("ServiceId");

                    b.ToTable("Reservations");
                });

            modelBuilder.Entity("PoS.WebApi.Domain.Entities.Service", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("Duration")
                        .HasColumnType("INTEGER");

                    b.Property<Guid>("EmployeeId")
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsActive")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasDefaultValue(false);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<decimal>("Price")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("EmployeeId");

                    b.ToTable("Services");
                });

            modelBuilder.Entity("PoS.WebApi.Domain.Entities.ServiceCharge", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsPercentage")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("LastUpdated")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("ReservationId")
                        .HasColumnType("TEXT");

                    b.Property<decimal>("Value")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("ReservationId");

                    b.ToTable("ServiceCharges");
                });

            modelBuilder.Entity("PoS.WebApi.Domain.Entities.Shift", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("Date")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("EmployeeId")
                        .HasColumnType("TEXT");

                    b.Property<TimeOnly>("EndTime")
                        .HasColumnType("TEXT");

                    b.Property<TimeOnly>("StartTime")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("EmployeeId");

                    b.ToTable("Shifts");
                });

            modelBuilder.Entity("PoS.WebApi.Domain.Entities.Tax", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsPercentage")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("LastUpdated")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("Type")
                        .HasColumnType("INTEGER");

                    b.Property<decimal>("Value")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Taxes");
                });

            modelBuilder.Entity("PoS.WebApi.Domain.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("BusinessId")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("DateOfEmployment")
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("LastUpdated")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("VARCHAR(20)");

                    b.Property<int>("Role")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Status")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("BusinessId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("PoS.WebApi.Domain.Entities.GroupDiscount", b =>
                {
                    b.HasOne("PoS.WebApi.Domain.Entities.Discount", "Discount")
                        .WithMany("GroupDiscounts")
                        .HasForeignKey("DiscountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PoS.WebApi.Domain.Entities.ItemGroup", "ItemGroup")
                        .WithMany("GroupDiscounts")
                        .HasForeignKey("ItemGroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Discount");

                    b.Navigation("ItemGroup");
                });

            modelBuilder.Entity("PoS.WebApi.Domain.Entities.Item", b =>
                {
                    b.HasOne("PoS.WebApi.Domain.Entities.ItemGroup", "ItemGroup")
                        .WithMany("Items")
                        .HasForeignKey("ItemGroupId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("ItemGroup");
                });

            modelBuilder.Entity("PoS.WebApi.Domain.Entities.ItemDiscount", b =>
                {
                    b.HasOne("PoS.WebApi.Domain.Entities.Discount", "Discount")
                        .WithMany("ItemDiscounts")
                        .HasForeignKey("DiscountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PoS.WebApi.Domain.Entities.Item", "Item")
                        .WithMany("ItemDiscounts")
                        .HasForeignKey("ItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Discount");

                    b.Navigation("Item");
                });

            modelBuilder.Entity("PoS.WebApi.Domain.Entities.ItemTax", b =>
                {
                    b.HasOne("PoS.WebApi.Domain.Entities.Item", "Item")
                        .WithMany("ItemTaxes")
                        .HasForeignKey("ItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PoS.WebApi.Domain.Entities.Tax", "Tax")
                        .WithMany()
                        .HasForeignKey("TaxId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Item");

                    b.Navigation("Tax");
                });

            modelBuilder.Entity("PoS.WebApi.Domain.Entities.Order", b =>
                {
                    b.HasOne("PoS.WebApi.Domain.Entities.Discount", "Discount")
                        .WithMany()
                        .HasForeignKey("DiscountId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("PoS.WebApi.Domain.Entities.User", "Employee")
                        .WithMany("Orders")
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PoS.WebApi.Domain.Entities.ServiceCharge", "ServiceCharge")
                        .WithMany()
                        .HasForeignKey("ServiceChargeId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("Discount");

                    b.Navigation("Employee");

                    b.Navigation("ServiceCharge");
                });

            modelBuilder.Entity("PoS.WebApi.Domain.Entities.OrderItem", b =>
                {
                    b.HasOne("PoS.WebApi.Domain.Entities.Item", "Item")
                        .WithMany()
                        .HasForeignKey("ItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PoS.WebApi.Domain.Entities.Order", "Order")
                        .WithMany("OrderItems")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Item");

                    b.Navigation("Order");
                });

            modelBuilder.Entity("PoS.WebApi.Domain.Entities.OrderService", b =>
                {
                    b.HasOne("PoS.WebApi.Domain.Entities.Order", "Order")
                        .WithMany("OrderServices")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PoS.WebApi.Domain.Entities.Reservation", null)
                        .WithMany("OrderServices")
                        .HasForeignKey("ReservationId");

                    b.HasOne("PoS.WebApi.Domain.Entities.Service", "Service")
                        .WithMany()
                        .HasForeignKey("ServiceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Order");

                    b.Navigation("Service");
                });

            modelBuilder.Entity("PoS.WebApi.Domain.Entities.Payment", b =>
                {
                    b.HasOne("PoS.WebApi.Domain.Entities.Order", "Order")
                        .WithMany("Payments")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Order");
                });

            modelBuilder.Entity("PoS.WebApi.Domain.Entities.Refund", b =>
                {
                    b.HasOne("PoS.WebApi.Domain.Entities.Order", "Order")
                        .WithOne("Refund")
                        .HasForeignKey("PoS.WebApi.Domain.Entities.Refund", "OrderId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Order");
                });

            modelBuilder.Entity("PoS.WebApi.Domain.Entities.Reservation", b =>
                {
                    b.HasOne("PoS.WebApi.Domain.Entities.Customer", "Customer")
                        .WithMany("Reservations")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PoS.WebApi.Domain.Entities.User", "Employee")
                        .WithMany("Reservations")
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PoS.WebApi.Domain.Entities.Order", "Order")
                        .WithOne("Reservation")
                        .HasForeignKey("PoS.WebApi.Domain.Entities.Reservation", "OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PoS.WebApi.Domain.Entities.Service", "Service")
                        .WithMany()
                        .HasForeignKey("ServiceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Customer");

                    b.Navigation("Employee");

                    b.Navigation("Order");

                    b.Navigation("Service");
                });

            modelBuilder.Entity("PoS.WebApi.Domain.Entities.Service", b =>
                {
                    b.HasOne("PoS.WebApi.Domain.Entities.User", "Employee")
                        .WithMany("Services")
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Employee");
                });

            modelBuilder.Entity("PoS.WebApi.Domain.Entities.ServiceCharge", b =>
                {
                    b.HasOne("PoS.WebApi.Domain.Entities.Reservation", null)
                        .WithMany("ServiceCharges")
                        .HasForeignKey("ReservationId");
                });

            modelBuilder.Entity("PoS.WebApi.Domain.Entities.Shift", b =>
                {
                    b.HasOne("PoS.WebApi.Domain.Entities.User", "Employee")
                        .WithMany("Shifts")
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Employee");
                });

            modelBuilder.Entity("PoS.WebApi.Domain.Entities.User", b =>
                {
                    b.HasOne("PoS.WebApi.Domain.Entities.Business", "Business")
                        .WithMany("Employees")
                        .HasForeignKey("BusinessId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Business");
                });

            modelBuilder.Entity("PoS.WebApi.Domain.Entities.Business", b =>
                {
                    b.Navigation("Employees");
                });

            modelBuilder.Entity("PoS.WebApi.Domain.Entities.Customer", b =>
                {
                    b.Navigation("Reservations");
                });

            modelBuilder.Entity("PoS.WebApi.Domain.Entities.Discount", b =>
                {
                    b.Navigation("GroupDiscounts");

                    b.Navigation("ItemDiscounts");
                });

            modelBuilder.Entity("PoS.WebApi.Domain.Entities.Item", b =>
                {
                    b.Navigation("ItemDiscounts");

                    b.Navigation("ItemTaxes");
                });

            modelBuilder.Entity("PoS.WebApi.Domain.Entities.ItemGroup", b =>
                {
                    b.Navigation("GroupDiscounts");

                    b.Navigation("Items");
                });

            modelBuilder.Entity("PoS.WebApi.Domain.Entities.Order", b =>
                {
                    b.Navigation("OrderItems");

                    b.Navigation("OrderServices");

                    b.Navigation("Payments");

                    b.Navigation("Refund");

                    b.Navigation("Reservation");
                });

            modelBuilder.Entity("PoS.WebApi.Domain.Entities.Reservation", b =>
                {
                    b.Navigation("OrderServices");

                    b.Navigation("ServiceCharges");
                });

            modelBuilder.Entity("PoS.WebApi.Domain.Entities.User", b =>
                {
                    b.Navigation("Orders");

                    b.Navigation("Reservations");

                    b.Navigation("Services");

                    b.Navigation("Shifts");
                });
#pragma warning restore 612, 618
        }
    }
}
