﻿// <auto-generated />
using System;
using AmadeusScanner.Repository.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace AmadeusScanner.Repository.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20220224160602_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.13");

            modelBuilder.Entity("AmadeusScanner.Repository.Entities.AirportEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("AirportTypeId")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("DateUpdated")
                        .HasColumnType("TEXT");

                    b.Property<string>("IataCode")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("AirportTypeId");

                    b.ToTable("Airport");
                });

            modelBuilder.Entity("AmadeusScanner.Repository.Entities.AirportTypeEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Abrv")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("DateUpdated")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("AirportType");
                });

            modelBuilder.Entity("AmadeusScanner.Repository.Entities.CurrencyEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Abrv")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("DateUpdated")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Currency");
                });

            modelBuilder.Entity("AmadeusScanner.Repository.Entities.FlightEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("CurrencyId")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("DateUpdated")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("DepartureDate")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("DestinationAirportId")
                        .HasColumnType("TEXT");

                    b.Property<int>("NumberOfTransversDeparture")
                        .HasColumnType("INTEGER");

                    b.Property<int>("NumberOfTransversReturn")
                        .HasColumnType("INTEGER");

                    b.Property<Guid>("OriginAirportId")
                        .HasColumnType("TEXT");

                    b.Property<decimal>("PricePerPerson")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("ReturnDate")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("CurrencyId");

                    b.HasIndex("DestinationAirportId");

                    b.HasIndex("OriginAirportId");

                    b.ToTable("Flight");
                });

            modelBuilder.Entity("AmadeusScanner.Repository.Entities.FlightSearchEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("DateUpdated")
                        .HasColumnType("TEXT");

                    b.Property<string>("FlightHash")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("FlightId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("FlightId");

                    b.ToTable("FlightSearch");
                });

            modelBuilder.Entity("AmadeusScanner.Repository.Entities.AirportEntity", b =>
                {
                    b.HasOne("AmadeusScanner.Repository.Entities.AirportTypeEntity", "AirportType")
                        .WithMany("Airports")
                        .HasForeignKey("AirportTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AirportType");
                });

            modelBuilder.Entity("AmadeusScanner.Repository.Entities.FlightEntity", b =>
                {
                    b.HasOne("AmadeusScanner.Repository.Entities.CurrencyEntity", "Currency")
                        .WithMany()
                        .HasForeignKey("CurrencyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AmadeusScanner.Repository.Entities.AirportEntity", "DestinationAirport")
                        .WithMany()
                        .HasForeignKey("DestinationAirportId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AmadeusScanner.Repository.Entities.AirportEntity", "OriginAirport")
                        .WithMany()
                        .HasForeignKey("OriginAirportId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Currency");

                    b.Navigation("DestinationAirport");

                    b.Navigation("OriginAirport");
                });

            modelBuilder.Entity("AmadeusScanner.Repository.Entities.FlightSearchEntity", b =>
                {
                    b.HasOne("AmadeusScanner.Repository.Entities.FlightEntity", "Flight")
                        .WithMany()
                        .HasForeignKey("FlightId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Flight");
                });

            modelBuilder.Entity("AmadeusScanner.Repository.Entities.AirportTypeEntity", b =>
                {
                    b.Navigation("Airports");
                });
#pragma warning restore 612, 618
        }
    }
}
