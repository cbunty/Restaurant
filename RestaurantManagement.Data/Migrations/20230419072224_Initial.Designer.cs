﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RestaurantManagement.Data.Contexts;

#nullable disable

namespace RestaurantManagement.Data.Migrations
{
    [DbContext(typeof(RestaurantDbContext))]
    [Migration("20230419072224_Initial")]
    partial class Initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("RestaurantManagement.Domain.DBModel.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("CreatedBy")
                        .HasColumnType("VARCHAR(200)");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasMaxLength(2000)
                        .HasColumnType("nvarchar(2000)");

                    b.Property<string>("ModifiedBy")
                        .HasColumnType("VARCHAR(200)");

                    b.Property<DateTime>("ModifiedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(500)");

                    b.Property<byte?>("StatusId")
                        .HasColumnType("tinyint");

                    b.HasKey("Id");

                    b.HasIndex("StatusId");

                    b.ToTable("Categories");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CreatedBy = "System",
                            CreatedOn = new DateTime(2023, 4, 19, 7, 22, 24, 14, DateTimeKind.Utc).AddTicks(710),
                            Description = "This is dummy category created by default by system for demo purporse only.",
                            ModifiedBy = "System",
                            ModifiedOn = new DateTime(2023, 4, 19, 7, 22, 24, 14, DateTimeKind.Utc).AddTicks(718),
                            Name = "Demo Category",
                            StatusId = (byte)1
                        });
                });

            modelBuilder.Entity("RestaurantManagement.Domain.DBModel.Menu", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("VARCHAR(200)");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasMaxLength(2000)
                        .HasColumnType("nvarchar(2000)");

                    b.Property<string>("ModifiedBy")
                        .HasColumnType("VARCHAR(200)");

                    b.Property<DateTime>("ModifiedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(500)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<int>("RestaurantId")
                        .HasColumnType("int");

                    b.Property<byte?>("StatusId")
                        .HasColumnType("tinyint");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.HasIndex("RestaurantId");

                    b.HasIndex("StatusId");

                    b.ToTable("Menus");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CategoryId = 1,
                            CreatedBy = "System",
                            CreatedOn = new DateTime(2023, 4, 19, 7, 22, 24, 14, DateTimeKind.Utc).AddTicks(751),
                            Description = "This is dummy Ist menu created by default by system for demo purporse only.",
                            ModifiedBy = "System",
                            ModifiedOn = new DateTime(2023, 4, 19, 7, 22, 24, 14, DateTimeKind.Utc).AddTicks(750),
                            Name = "Demo Ist Menu",
                            Price = 10m,
                            Quantity = 10,
                            RestaurantId = 1,
                            StatusId = (byte)1
                        },
                        new
                        {
                            Id = 2,
                            CategoryId = 1,
                            CreatedBy = "System",
                            CreatedOn = new DateTime(2023, 4, 19, 7, 22, 24, 14, DateTimeKind.Utc).AddTicks(753),
                            Description = "This is dummy IInd menu created by default by system for demo purporse only.",
                            ModifiedBy = "System",
                            ModifiedOn = new DateTime(2023, 4, 19, 7, 22, 24, 14, DateTimeKind.Utc).AddTicks(753),
                            Name = "Demo IInd Menu",
                            Price = 20m,
                            Quantity = 10,
                            RestaurantId = 1,
                            StatusId = (byte)1
                        });
                });

            modelBuilder.Entity("RestaurantManagement.Domain.DBModel.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("CreatedBy")
                        .HasColumnType("VARCHAR(200)");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<bool>("HasPaid")
                        .HasColumnType("bit");

                    b.Property<string>("ModifiedBy")
                        .HasColumnType("VARCHAR(200)");

                    b.Property<DateTime>("ModifiedOn")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("OrderDateTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("OrderNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte>("OrderStatusId")
                        .HasColumnType("tinyint");

                    b.Property<decimal>("TotalPrice")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("OrderStatusId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("RestaurantManagement.Domain.DBModel.OrderDetail", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("CreatedBy")
                        .HasColumnType("VARCHAR(200)");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<int>("MenuId")
                        .HasColumnType("int");

                    b.Property<string>("ModifiedBy")
                        .HasColumnType("VARCHAR(200)");

                    b.Property<DateTime>("ModifiedOn")
                        .HasColumnType("datetime2");

                    b.Property<int>("OrderId")
                        .HasColumnType("int");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<decimal>("UnitPrice")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("MenuId");

                    b.HasIndex("OrderId");

                    b.ToTable("OrderDetails");
                });

            modelBuilder.Entity("RestaurantManagement.Domain.DBModel.OrderStatus", b =>
                {
                    b.Property<byte>("Id")
                        .HasColumnType("tinyint");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("VARCHAR(200)");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("ModifiedBy")
                        .HasColumnType("VARCHAR(200)");

                    b.Property<DateTime>("ModifiedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .HasColumnType("VARCHAR(50)");

                    b.HasKey("Id");

                    b.ToTable("OrderStatus");

                    b.HasData(
                        new
                        {
                            Id = (byte)1,
                            CreatedOn = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            ModifiedOn = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "Pending"
                        },
                        new
                        {
                            Id = (byte)2,
                            CreatedOn = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            ModifiedOn = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "Rejected"
                        },
                        new
                        {
                            Id = (byte)3,
                            CreatedOn = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            ModifiedOn = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "Completed"
                        },
                        new
                        {
                            Id = (byte)4,
                            CreatedOn = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            ModifiedOn = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "Failed"
                        },
                        new
                        {
                            Id = (byte)5,
                            CreatedOn = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            ModifiedOn = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "Paid"
                        },
                        new
                        {
                            Id = (byte)6,
                            CreatedOn = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            ModifiedOn = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "ProcessingPayment"
                        });
                });

            modelBuilder.Entity("RestaurantManagement.Domain.DBModel.Restaurant", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Address")
                        .HasMaxLength(2000)
                        .HasColumnType("nvarchar(2000)");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("VARCHAR(200)");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasMaxLength(2000)
                        .HasColumnType("nvarchar(2000)");

                    b.Property<string>("ModifiedBy")
                        .HasColumnType("VARCHAR(200)");

                    b.Property<DateTime>("ModifiedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(500)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("varchar(13)");

                    b.Property<byte?>("StatusId")
                        .HasColumnType("tinyint");

                    b.Property<string>("WebsiteUrl")
                        .HasColumnType("varchar(250)");

                    b.HasKey("Id");

                    b.HasIndex("StatusId");

                    b.ToTable("Restaurants");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Address = "43a Nehru place, delhi, India - 174890",
                            CreatedBy = "System",
                            CreatedOn = new DateTime(2023, 4, 19, 7, 22, 24, 14, DateTimeKind.Utc).AddTicks(733),
                            Description = "This is dummy restaurant created by default by system for demo purporse only.",
                            ModifiedBy = "System",
                            ModifiedOn = new DateTime(2023, 4, 19, 7, 22, 24, 14, DateTimeKind.Utc).AddTicks(733),
                            Name = "Demo Restaurant",
                            PhoneNumber = "+919123456780",
                            StatusId = (byte)1,
                            WebsiteUrl = "www.restaurant.com"
                        });
                });

            modelBuilder.Entity("RestaurantManagement.Domain.DBModel.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("CreatedBy")
                        .HasColumnType("VARCHAR(200)");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("ModifiedBy")
                        .HasColumnType("VARCHAR(200)");

                    b.Property<DateTime>("ModifiedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(500)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("varchar(500)");

                    b.Property<byte?>("StatusId")
                        .HasColumnType("tinyint");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("varchar(500)");

                    b.HasKey("Id");

                    b.HasIndex("StatusId");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CreatedOn = new DateTime(2023, 4, 19, 7, 22, 24, 14, DateTimeKind.Utc).AddTicks(766),
                            ModifiedOn = new DateTime(2023, 4, 19, 7, 22, 24, 14, DateTimeKind.Utc).AddTicks(766),
                            Name = "Admin User",
                            Password = "admin",
                            StatusId = (byte)1,
                            UserName = "admin"
                        });
                });

            modelBuilder.Entity("RestaurantManagement.Domain.Status", b =>
                {
                    b.Property<byte>("Id")
                        .HasColumnType("tinyint");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("VARCHAR(200)");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("ModifiedBy")
                        .HasColumnType("VARCHAR(200)");

                    b.Property<DateTime>("ModifiedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .HasColumnType("VARCHAR(50)");

                    b.HasKey("Id");

                    b.ToTable("Status");

                    b.HasData(
                        new
                        {
                            Id = (byte)1,
                            CreatedOn = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            ModifiedOn = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "Active"
                        },
                        new
                        {
                            Id = (byte)2,
                            CreatedOn = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            ModifiedOn = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "InActive"
                        });
                });

            modelBuilder.Entity("RestaurantManagement.Domain.DBModel.Category", b =>
                {
                    b.HasOne("RestaurantManagement.Domain.Status", "Status")
                        .WithMany()
                        .HasForeignKey("StatusId");

                    b.Navigation("Status");
                });

            modelBuilder.Entity("RestaurantManagement.Domain.DBModel.Menu", b =>
                {
                    b.HasOne("RestaurantManagement.Domain.DBModel.Category", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RestaurantManagement.Domain.DBModel.Restaurant", "Restaurant")
                        .WithMany("Menus")
                        .HasForeignKey("RestaurantId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RestaurantManagement.Domain.Status", "Status")
                        .WithMany()
                        .HasForeignKey("StatusId");

                    b.Navigation("Category");

                    b.Navigation("Restaurant");

                    b.Navigation("Status");
                });

            modelBuilder.Entity("RestaurantManagement.Domain.DBModel.Order", b =>
                {
                    b.HasOne("RestaurantManagement.Domain.DBModel.OrderStatus", "OrderStatus")
                        .WithMany()
                        .HasForeignKey("OrderStatusId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("OrderStatus");
                });

            modelBuilder.Entity("RestaurantManagement.Domain.DBModel.OrderDetail", b =>
                {
                    b.HasOne("RestaurantManagement.Domain.DBModel.Menu", "Menu")
                        .WithMany()
                        .HasForeignKey("MenuId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RestaurantManagement.Domain.DBModel.Order", "Order")
                        .WithMany("OrderDetails")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Menu");

                    b.Navigation("Order");
                });

            modelBuilder.Entity("RestaurantManagement.Domain.DBModel.Restaurant", b =>
                {
                    b.HasOne("RestaurantManagement.Domain.Status", "Status")
                        .WithMany()
                        .HasForeignKey("StatusId");

                    b.Navigation("Status");
                });

            modelBuilder.Entity("RestaurantManagement.Domain.DBModel.User", b =>
                {
                    b.HasOne("RestaurantManagement.Domain.Status", "Status")
                        .WithMany()
                        .HasForeignKey("StatusId");

                    b.Navigation("Status");
                });

            modelBuilder.Entity("RestaurantManagement.Domain.DBModel.Order", b =>
                {
                    b.Navigation("OrderDetails");
                });

            modelBuilder.Entity("RestaurantManagement.Domain.DBModel.Restaurant", b =>
                {
                    b.Navigation("Menus");
                });
#pragma warning restore 612, 618
        }
    }
}
