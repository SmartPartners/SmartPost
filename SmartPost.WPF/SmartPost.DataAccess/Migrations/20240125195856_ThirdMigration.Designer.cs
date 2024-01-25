﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using SmartPost.DataAccess.Data;

#nullable disable

namespace SmartPost.DataAccess.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20240125195856_ThirdMigration")]
    partial class ThirdMigration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("SmartPost.Domain.Entities.Brands.Brand", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.ToTable("Brands");
                });

            modelBuilder.Entity("SmartPost.Domain.Entities.CancelOrders.CancelOrder", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<string>("Action")
                        .HasColumnType("text");

                    b.Property<string>("BarCode")
                        .HasColumnType("text");

                    b.Property<string>("CanceledBy")
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<long>("PCode")
                        .HasColumnType("bigint");

                    b.Property<decimal>("Price")
                        .HasColumnType("numeric");

                    b.Property<string>("ProductName")
                        .HasColumnType("text");

                    b.Property<decimal>("Quantity")
                        .HasColumnType("numeric");

                    b.Property<string>("Reason")
                        .HasColumnType("text");

                    b.Property<DateTime>("ReturnDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("SaleBy")
                        .HasColumnType("text");

                    b.Property<decimal>("TotalPrice")
                        .HasColumnType("numeric");

                    b.Property<string>("TransNo")
                        .HasColumnType("text");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.ToTable("CancelOrders");
                });

            modelBuilder.Entity("SmartPost.Domain.Entities.Cards.Card", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<string>("BarCode")
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<decimal>("DiscPercent")
                        .HasColumnType("numeric");

                    b.Property<long>("PCode")
                        .HasColumnType("bigint");

                    b.Property<decimal>("Price")
                        .HasColumnType("numeric");

                    b.Property<string>("ProductName")
                        .HasColumnType("text");

                    b.Property<decimal>("Quantity")
                        .HasColumnType("numeric");

                    b.Property<string>("Status")
                        .HasColumnType("text");

                    b.Property<decimal>("TotalPrice")
                        .HasColumnType("numeric");

                    b.Property<string>("TransNo")
                        .HasColumnType("text");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Cards");
                });

            modelBuilder.Entity("SmartPost.Domain.Entities.Categories.Category", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("SmartPost.Domain.Entities.InventoryLists.InventoryList", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<string>("BarCode")
                        .HasColumnType("text");

                    b.Property<long>("BrandId")
                        .HasColumnType("bigint");

                    b.Property<long>("CategoryId")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<long>("PCode")
                        .HasColumnType("bigint");

                    b.Property<decimal>("Price")
                        .HasColumnType("numeric");

                    b.Property<string>("ProductName")
                        .HasColumnType("text");

                    b.Property<decimal>("Quantity")
                        .HasColumnType("numeric");

                    b.Property<decimal>("TotalPrice")
                        .HasColumnType("numeric");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("BrandId");

                    b.HasIndex("CategoryId");

                    b.ToTable("InventoryLists");
                });

            modelBuilder.Entity("SmartPost.Domain.Entities.StokProducts.StokProduct", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<string>("BarCode")
                        .HasColumnType("text");

                    b.Property<long>("BrandId")
                        .HasColumnType("bigint");

                    b.Property<long>("CategoryId")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<long>("PCode")
                        .HasColumnType("bigint");

                    b.Property<decimal>("Price")
                        .HasColumnType("numeric");

                    b.Property<string>("ProductName")
                        .HasColumnType("text");

                    b.Property<decimal>("Quantity")
                        .HasColumnType("numeric");

                    b.Property<string>("Status")
                        .HasColumnType("text");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("BrandId");

                    b.HasIndex("CategoryId");

                    b.HasIndex("UserId");

                    b.ToTable("StokProducts");
                });

            modelBuilder.Entity("SmartPost.Domain.Entities.StorageProducts.StorageProduct", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<string>("BarCode")
                        .HasColumnType("text");

                    b.Property<long>("BrandId")
                        .HasColumnType("bigint");

                    b.Property<long>("CategoryId")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<long>("PCode")
                        .HasColumnType("bigint");

                    b.Property<decimal>("Price")
                        .HasColumnType("numeric");

                    b.Property<string>("ProductName")
                        .HasColumnType("text");

                    b.Property<decimal>("Quantity")
                        .HasColumnType("numeric");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("BrandId");

                    b.HasIndex("CategoryId");

                    b.ToTable("StorageProducts");
                });

            modelBuilder.Entity("SmartPost.Domain.Entities.Users.User", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("FirstName")
                        .HasColumnType("text");

                    b.Property<string>("LastName")
                        .HasColumnType("text");

                    b.Property<string>("Password")
                        .HasColumnType("text");

                    b.Property<string>("PasswordSalt")
                        .HasColumnType("text");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("text");

                    b.Property<int>("Role")
                        .HasColumnType("integer");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("SmartPost.Domain.Entities.Cards.Card", b =>
                {
                    b.HasOne("SmartPost.Domain.Entities.Users.User", "Users")
                        .WithMany("Cards")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Users");
                });

            modelBuilder.Entity("SmartPost.Domain.Entities.InventoryLists.InventoryList", b =>
                {
                    b.HasOne("SmartPost.Domain.Entities.Brands.Brand", "Brand")
                        .WithMany("InventoryLists")
                        .HasForeignKey("BrandId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SmartPost.Domain.Entities.Categories.Category", "Category")
                        .WithMany("InventoryLists")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Brand");

                    b.Navigation("Category");
                });

            modelBuilder.Entity("SmartPost.Domain.Entities.StokProducts.StokProduct", b =>
                {
                    b.HasOne("SmartPost.Domain.Entities.Brands.Brand", "Brand")
                        .WithMany("StokProducts")
                        .HasForeignKey("BrandId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SmartPost.Domain.Entities.Categories.Category", "Category")
                        .WithMany("StokProducts")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SmartPost.Domain.Entities.Users.User", "Users")
                        .WithMany("StokProducts")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Brand");

                    b.Navigation("Category");

                    b.Navigation("Users");
                });

            modelBuilder.Entity("SmartPost.Domain.Entities.StorageProducts.StorageProduct", b =>
                {
                    b.HasOne("SmartPost.Domain.Entities.Brands.Brand", "Brand")
                        .WithMany("StorageProducts")
                        .HasForeignKey("BrandId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SmartPost.Domain.Entities.Categories.Category", "Category")
                        .WithMany("StorageProducts")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Brand");

                    b.Navigation("Category");
                });

            modelBuilder.Entity("SmartPost.Domain.Entities.Brands.Brand", b =>
                {
                    b.Navigation("InventoryLists");

                    b.Navigation("StokProducts");

                    b.Navigation("StorageProducts");
                });

            modelBuilder.Entity("SmartPost.Domain.Entities.Categories.Category", b =>
                {
                    b.Navigation("InventoryLists");

                    b.Navigation("StokProducts");

                    b.Navigation("StorageProducts");
                });

            modelBuilder.Entity("SmartPost.Domain.Entities.Users.User", b =>
                {
                    b.Navigation("Cards");

                    b.Navigation("StokProducts");
                });
#pragma warning restore 612, 618
        }
    }
}
