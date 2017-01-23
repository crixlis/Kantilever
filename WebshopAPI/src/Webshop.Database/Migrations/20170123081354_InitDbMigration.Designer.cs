using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using ;

namespace Webshop.Database.Migrations
{
    [DbContext(typeof(WebshopContext))]
    [Migration("20170123081354_InitDbMigration")]
    partial class InitDbMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.0-rtm-22752");

            modelBuilder.Entity("Artikel", b =>
                {
                    b.Property<int>("Id");

                    b.Property<string>("Beschrijving");

                    b.Property<string>("Leverancier");

                    b.Property<string>("LeverancierCode");

                    b.Property<DateTime?>("LeverbaarTot");

                    b.Property<DateTime>("LeverbaarVanaf");

                    b.Property<string>("Naam");

                    b.Property<decimal>("Prijs");

                    b.Property<int>("Voorraad");

                    b.HasKey("Id");

                    b.ToTable("Artikelen");
                });
        }
    }
}
