using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace MagazijnMedewerker.Database.Migrations
{
    public partial class test : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Artikelen",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    Beschrijving = table.Column<string>(nullable: true),
                    Leverancier = table.Column<string>(nullable: true),
                    LeverbaarTot = table.Column<DateTime>(nullable: false),
                    LeverbaarVanaf = table.Column<DateTime>(nullable: false),
                    Naam = table.Column<string>(nullable: true),
                    Prijs = table.Column<decimal>(nullable: false),
                    Voorraad = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Artikelen", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Bestellingen",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    BestelDatum = table.Column<DateTime>(nullable: false),
                    Status = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bestellingen", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BestelArtikelSet",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Aantal = table.Column<int>(nullable: false),
                    ArtikelenId = table.Column<int>(nullable: true),
                    BestellingId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BestelArtikelSet", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BestelArtikelSet_Artikelen_ArtikelenId",
                        column: x => x.ArtikelenId,
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
                name: "IX_BestelArtikelSet_ArtikelenId",
                table: "BestelArtikelSet",
                column: "ArtikelenId");

            migrationBuilder.CreateIndex(
                name: "IX_BestelArtikelSet_BestellingId",
                table: "BestelArtikelSet",
                column: "BestellingId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BestelArtikelSet");

            migrationBuilder.DropTable(
                name: "Artikelen");

            migrationBuilder.DropTable(
                name: "Bestellingen");
        }
    }
}
