using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace CommercieelManager.Database.Migrations
{
    public partial class Commercieelmanager : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Artikelen",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    Naam = table.Column<string>(nullable: true),
                    Prijs = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Artikelen", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Klant",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Achternaam = table.Column<string>(nullable: true),
                    Adres = table.Column<string>(nullable: true),
                    Plaatsnaam = table.Column<string>(nullable: true),
                    Postcode = table.Column<string>(nullable: true),
                    Telefoonnummer = table.Column<int>(nullable: false),
                    Voornaam = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Klant", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Bestellingen",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    BestelDatum = table.Column<DateTime>(nullable: false),
                    KlantId = table.Column<int>(nullable: true),
                    Status = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bestellingen", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Bestellingen_Klant_KlantId",
                        column: x => x.KlantId,
                        principalTable: "Klant",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BestelArtikelSet",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Aantal = table.Column<int>(nullable: false),
                    ArtikelId = table.Column<int>(nullable: true),
                    BestellingId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BestelArtikelSet", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BestelArtikelSet_Artikelen_ArtikelId",
                        column: x => x.ArtikelId,
                        principalTable: "Artikelen",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BestelArtikelSet_Bestellingen_BestellingId",
                        column: x => x.BestellingId,
                        principalTable: "Bestellingen",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BestelArtikelSet_ArtikelId",
                table: "BestelArtikelSet",
                column: "ArtikelId");

            migrationBuilder.CreateIndex(
                name: "IX_BestelArtikelSet_BestellingId",
                table: "BestelArtikelSet",
                column: "BestellingId");

            migrationBuilder.CreateIndex(
                name: "IX_Bestellingen_KlantId",
                table: "Bestellingen",
                column: "KlantId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BestelArtikelSet");

            migrationBuilder.DropTable(
                name: "Artikelen");

            migrationBuilder.DropTable(
                name: "Bestellingen");

            migrationBuilder.DropTable(
                name: "Klant");
        }
    }
}
