﻿using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using MagazijnMedewerker.Database;

namespace MagazijnMedewerker.Database.Migrations
{
    [DbContext(typeof(MagazijnMedewerkerContext))]
    partial class MagazijnMedewerkerContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.1")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("MagazijnMedewerker.Database.Artikel", b =>
                {
                    b.Property<int>("Id");

                    b.Property<string>("Beschrijving");

                    b.Property<string>("Leverancier");

                    b.Property<DateTime?>("LeverbaarTot");

                    b.Property<DateTime>("LeverbaarVanaf");

                    b.Property<string>("Naam");

                    b.Property<decimal>("Prijs");

                    b.Property<int>("Voorraad");

                    b.HasKey("Id");

                    b.ToTable("Artikelen");
                });

            modelBuilder.Entity("MagazijnMedewerker.Database.BestelArtikel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Aantal");

                    b.Property<int?>("ArtikelId");

                    b.Property<int?>("BestellingId");

                    b.HasKey("Id");

                    b.HasIndex("ArtikelId");

                    b.HasIndex("BestellingId");

                    b.ToTable("BestelArtikelSet");
                });

            modelBuilder.Entity("MagazijnMedewerker.Database.Bestelling", b =>
                {
                    b.Property<int>("Id");

                    b.Property<DateTime>("BestelDatum");

                    b.Property<int?>("KlantId");

                    b.Property<int>("Status");

                    b.HasKey("Id");

                    b.HasIndex("KlantId");

                    b.ToTable("Bestellingen");
                });

            modelBuilder.Entity("MagazijnMedewerker.Database.Klant", b =>
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

            modelBuilder.Entity("MagazijnMedewerker.Database.BestelArtikel", b =>
                {
                    b.HasOne("MagazijnMedewerker.Database.Artikel", "Artikel")
                        .WithMany()
                        .HasForeignKey("ArtikelId");

                    b.HasOne("MagazijnMedewerker.Database.Bestelling", "Bestelling")
                        .WithMany()
                        .HasForeignKey("BestellingId");
                });

            modelBuilder.Entity("MagazijnMedewerker.Database.Bestelling", b =>
                {
                    b.HasOne("MagazijnMedewerker.Database.Klant", "Klant")
                        .WithMany()
                        .HasForeignKey("KlantId");
                });
        }
    }
}
