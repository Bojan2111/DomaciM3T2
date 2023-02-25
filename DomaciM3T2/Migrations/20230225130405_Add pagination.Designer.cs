﻿// <auto-generated />
using System;
using DomaciM3T2.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DomaciM3T2.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20230225130405_Add pagination")]
    partial class Addpagination
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.17")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("DomaciM3T2.Models.Festival", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DatumOdrzavanja")
                        .HasColumnType("datetime2");

                    b.Property<int>("MaksimumPosetilaca")
                        .HasColumnType("int");

                    b.Property<string>("Mesto")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Naziv")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<int>("Ocena")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Festivals");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            DatumOdrzavanja = new DateTime(2023, 2, 26, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            MaksimumPosetilaca = 6000,
                            Mesto = "Mokrin",
                            Naziv = "Svetsko prvenstvo u nadmetanju guskova",
                            Ocena = 4
                        },
                        new
                        {
                            Id = 2,
                            DatumOdrzavanja = new DateTime(2023, 8, 4, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            MaksimumPosetilaca = 150000,
                            Mesto = "Nis",
                            Naziv = "Nisville",
                            Ocena = 5
                        },
                        new
                        {
                            Id = 3,
                            DatumOdrzavanja = new DateTime(2023, 8, 18, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            MaksimumPosetilaca = 100000,
                            Mesto = "Beograd",
                            Naziv = "Beer Fest",
                            Ocena = 4
                        });
                });

            modelBuilder.Entity("DomaciM3T2.Models.Karta", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<double>("Cena")
                        .HasColumnType("float");

                    b.Property<DateTime>("DatumKupovine")
                        .HasColumnType("datetime2");

                    b.Property<int>("FestivalId")
                        .HasColumnType("int");

                    b.Property<string>("Kupac")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Preuzeta")
                        .HasColumnType("bit");

                    b.Property<int>("TipKarteId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("FestivalId");

                    b.HasIndex("TipKarteId");

                    b.ToTable("Kartas");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Cena = 1000.0,
                            DatumKupovine = new DateTime(2023, 2, 23, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            FestivalId = 1,
                            Kupac = "Pera Peric",
                            Preuzeta = true,
                            TipKarteId = 1
                        },
                        new
                        {
                            Id = 2,
                            Cena = 2000.0,
                            DatumKupovine = new DateTime(2023, 2, 21, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            FestivalId = 1,
                            Kupac = "Djoka Djokic",
                            Preuzeta = false,
                            TipKarteId = 2
                        },
                        new
                        {
                            Id = 3,
                            Cena = 1800.0,
                            DatumKupovine = new DateTime(2023, 2, 20, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            FestivalId = 2,
                            Kupac = "Marko Markovic",
                            Preuzeta = false,
                            TipKarteId = 1
                        },
                        new
                        {
                            Id = 4,
                            Cena = 2600.0,
                            DatumKupovine = new DateTime(2023, 3, 13, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            FestivalId = 2,
                            Kupac = "Lara Laric",
                            Preuzeta = true,
                            TipKarteId = 2
                        },
                        new
                        {
                            Id = 5,
                            Cena = 1560.0,
                            DatumKupovine = new DateTime(2023, 4, 28, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            FestivalId = 3,
                            Kupac = "Mika Mikic",
                            Preuzeta = true,
                            TipKarteId = 1
                        },
                        new
                        {
                            Id = 6,
                            Cena = 2820.0,
                            DatumKupovine = new DateTime(2023, 1, 25, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            FestivalId = 3,
                            Kupac = "Ana Ancic",
                            Preuzeta = false,
                            TipKarteId = 2
                        });
                });

            modelBuilder.Entity("DomaciM3T2.Models.TipKarte", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Naziv")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.ToTable("TipKartes");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Naziv = "Redovna karta"
                        },
                        new
                        {
                            Id = 2,
                            Naziv = "VIP karta"
                        });
                });

            modelBuilder.Entity("DomaciM3T2.Models.Karta", b =>
                {
                    b.HasOne("DomaciM3T2.Models.Festival", "Festival")
                        .WithMany("Kartas")
                        .HasForeignKey("FestivalId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DomaciM3T2.Models.TipKarte", "TipKarte")
                        .WithMany("Kartas")
                        .HasForeignKey("TipKarteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Festival");

                    b.Navigation("TipKarte");
                });

            modelBuilder.Entity("DomaciM3T2.Models.Festival", b =>
                {
                    b.Navigation("Kartas");
                });

            modelBuilder.Entity("DomaciM3T2.Models.TipKarte", b =>
                {
                    b.Navigation("Kartas");
                });
#pragma warning restore 612, 618
        }
    }
}
