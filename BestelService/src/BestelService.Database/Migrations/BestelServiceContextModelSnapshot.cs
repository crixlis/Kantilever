using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using BestelService.Database;

namespace BestelService.Database.Migrations
{
    [DbContext(typeof(BestelServiceContext))]
    partial class BestelServiceContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.1");

            modelBuilder.Entity("Artikel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Beschrijving");

                    b.Property<int?>("BestellingId");

                    b.Property<string>("Leverancier");

                    b.Property<string>("Naam");

                    b.Property<decimal>("Prijs");

                    b.Property<int>("Voorraad");

                    b.HasKey("Id");

                    b.HasIndex("BestellingId");

                    b.ToTable("Artikel");
                });

            modelBuilder.Entity("Bestelling", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("KlantId");

                    b.HasKey("Id");

                    b.HasIndex("KlantId");

                    b.ToTable("Bestelling");
                });

            modelBuilder.Entity("Klant", b =>
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

            modelBuilder.Entity("Artikel", b =>
                {
                    b.HasOne("Bestelling")
                        .WithMany("Artikelen")
                        .HasForeignKey("BestellingId");
                });

            modelBuilder.Entity("Bestelling", b =>
                {
                    b.HasOne("Klant", "Klant")
                        .WithMany()
                        .HasForeignKey("KlantId");
                });
        }
    }
}
