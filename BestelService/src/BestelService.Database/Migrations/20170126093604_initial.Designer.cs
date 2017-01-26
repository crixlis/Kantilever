using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using BestelService.Database;

namespace BestelService.Database.Migrations
{
    [DbContext(typeof(BestelServiceContext))]
    [Migration("20170126093604_initial")]
    partial class initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.1");

            modelBuilder.Entity("BestelService.Database.Artikel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Aantal");

                    b.Property<string>("Beschrijving");

                    b.Property<int?>("BestellingId");

                    b.Property<string>("Leverancier");

                    b.Property<string>("Naam");

                    b.Property<decimal>("Prijs");

                    b.HasKey("Id");

                    b.HasIndex("BestellingId");

                    b.ToTable("Artikel");
                });

            modelBuilder.Entity("BestelService.Database.Bestelling", b =>
                {
                    b.Property<int>("Id");

                    b.Property<DateTime>("BestelDatum");

                    b.Property<int?>("KlantId");

                    b.Property<int>("Status");

                    b.HasKey("Id");

                    b.HasIndex("KlantId");

                    b.ToTable("Bestelling");
                });

            modelBuilder.Entity("BestelService.Database.Klant", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Achternaam");

                    b.Property<string>("Adres");

                    b.Property<string>("Plaatsnaam");

                    b.Property<string>("Postcode");

                    b.Property<int>("Telefoonnummer");

                    b.Property<string>("Voornaam");

                    b.HasKey("Id");

                    b.ToTable("Klant");
                });

            modelBuilder.Entity("BestelService.Database.Artikel", b =>
                {
                    b.HasOne("BestelService.Database.Bestelling")
                        .WithMany("Artikelen")
                        .HasForeignKey("BestellingId");
                });

            modelBuilder.Entity("BestelService.Database.Bestelling", b =>
                {
                    b.HasOne("BestelService.Database.Klant", "Klant")
                        .WithMany()
                        .HasForeignKey("KlantId");
                });
        }
    }
}
