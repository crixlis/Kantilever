using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using FactuurService.Database;

namespace FactuurService.Database.Migrations
{
    [DbContext(typeof(FactuurServiceContext))]
    partial class FactuurServiceContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.0-rtm-22752");

            modelBuilder.Entity("FactuurService.Database.Artikel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("FactuurId");

                    b.Property<string>("Naam");

                    b.Property<decimal>("Prijs");

                    b.HasKey("Id");

                    b.HasIndex("FactuurId");

                    b.ToTable("Artikel");
                });

            modelBuilder.Entity("FactuurService.Database.Factuur", b =>
                {
                    b.Property<int>("Id");

                    b.Property<DateTime>("HuidigeDatum");

                    b.Property<int?>("KlantId");

                    b.Property<decimal>("Totaal");

                    b.HasKey("Id");

                    b.HasIndex("KlantId");

                    b.ToTable("Facturen");
                });

            modelBuilder.Entity("FactuurService.Database.Klant", b =>
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

            modelBuilder.Entity("FactuurService.Database.Artikel", b =>
                {
                    b.HasOne("FactuurService.Database.Factuur")
                        .WithMany("Artikelen")
                        .HasForeignKey("FactuurId");
                });

            modelBuilder.Entity("FactuurService.Database.Factuur", b =>
                {
                    b.HasOne("FactuurService.Database.Klant", "Klant")
                        .WithMany()
                        .HasForeignKey("KlantId");
                });
        }
    }
}
