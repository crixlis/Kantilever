using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using WebshopBeheer.Database;

namespace WebshopBeheer.Database.Migrations
{
    [DbContext(typeof(WebshopBeheerContext))]
    [Migration("20170124120530_Besteldatum")]
    partial class Besteldatum
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.1")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("WebshopBeheer.Database.Artikel", b =>
                {
                    b.Property<int>("Id");

                    b.Property<string>("Beschrijving");

                    b.Property<int?>("BestellingId");

                    b.Property<string>("Leverancier");

                    b.Property<DateTime>("LeverbaarTot");

                    b.Property<DateTime>("LeverbaarVanaf");

                    b.Property<string>("Naam");

                    b.Property<decimal>("Prijs");

                    b.Property<int>("Voorraad");

                    b.HasKey("Id");

                    b.HasIndex("BestellingId");

                    b.ToTable("Artikel");
                });

            modelBuilder.Entity("WebshopBeheer.Database.Bestelling", b =>
                {
                    b.Property<int>("Id");

                    b.Property<DateTime>("BestelDatum");

                    b.Property<int?>("KlantId");

                    b.Property<int>("Status");

                    b.HasKey("Id");

                    b.HasIndex("KlantId");

                    b.ToTable("Bestellingen");
                });

            modelBuilder.Entity("WebshopBeheer.Database.Klant", b =>
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

            modelBuilder.Entity("WebshopBeheer.Database.Artikel", b =>
                {
                    b.HasOne("WebshopBeheer.Database.Bestelling")
                        .WithMany("Artikelen")
                        .HasForeignKey("BestellingId");
                });

            modelBuilder.Entity("WebshopBeheer.Database.Bestelling", b =>
                {
                    b.HasOne("WebshopBeheer.Database.Klant", "Klant")
                        .WithMany()
                        .HasForeignKey("KlantId");
                });
        }
    }
}
