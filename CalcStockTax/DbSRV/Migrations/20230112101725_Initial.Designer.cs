﻿// <auto-generated />
using System;
using DbSRV.DB;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DbSRV.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    [Migration("20230112101725_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("DbSRV.Models.Investment", b =>
                {
                    b.Property<int>("InvestmentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("BuyDate")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("BuyPrice")
                        .HasColumnType("int");

                    b.Property<int>("CurrentPrice")
                        .HasColumnType("int");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<string>("StockName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("InvestmentId");

                    b.ToTable("Investments");
                });

            modelBuilder.Entity("DbSRV.Models.Tariff", b =>
                {
                    b.Property<int>("TariffId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("TaxId")
                        .HasColumnType("int");

                    b.Property<int>("Value")
                        .HasColumnType("int");

                    b.HasKey("TariffId");

                    b.ToTable("Tariffs");
                });

            modelBuilder.Entity("DbSRV.Models.Tax", b =>
                {
                    b.Property<int>("TaxId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("BankFee")
                        .HasColumnType("int");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("SumTax")
                        .HasColumnType("int");

                    b.Property<int>("TariffId")
                        .HasColumnType("int");

                    b.Property<int>("TaxValue")
                        .HasColumnType("int");

                    b.HasKey("TaxId");

                    b.ToTable("Taxs");
                });

            modelBuilder.Entity("DbSRV.Models.TaxAmount", b =>
                {
                    b.Property<int>("TaxAmountId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("Amount")
                        .HasColumnType("int");

                    b.HasKey("TaxAmountId");

                    b.ToTable("TaxAmounts");
                });
#pragma warning restore 612, 618
        }
    }
}
