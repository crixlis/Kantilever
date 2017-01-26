using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace MagazijnMedewerker.Database.Migrations
{
    public partial class Magazijnmedewerker : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BestelArtikelSet_Artikelen_ArtikelenId",
                table: "BestelArtikelSet");

            migrationBuilder.DropIndex(
                name: "IX_BestelArtikelSet_ArtikelenId",
                table: "BestelArtikelSet");

            migrationBuilder.DropColumn(
                name: "ArtikelenId",
                table: "BestelArtikelSet");

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

            migrationBuilder.AddColumn<int>(
                name: "KlantId",
                table: "Bestellingen",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ArtikelId",
                table: "BestelArtikelSet",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Bestellingen_KlantId",
                table: "Bestellingen",
                column: "KlantId");

            migrationBuilder.CreateIndex(
                name: "IX_BestelArtikelSet_ArtikelId",
                table: "BestelArtikelSet",
                column: "ArtikelId");

            migrationBuilder.AlterColumn<DateTime>(
                name: "LeverbaarTot",
                table: "Artikelen",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_BestelArtikelSet_Artikelen_ArtikelId",
                table: "BestelArtikelSet",
                column: "ArtikelId",
                principalTable: "Artikelen",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Bestellingen_Klant_KlantId",
                table: "Bestellingen",
                column: "KlantId",
                principalTable: "Klant",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BestelArtikelSet_Artikelen_ArtikelId",
                table: "BestelArtikelSet");

            migrationBuilder.DropForeignKey(
                name: "FK_Bestellingen_Klant_KlantId",
                table: "Bestellingen");

            migrationBuilder.DropIndex(
                name: "IX_Bestellingen_KlantId",
                table: "Bestellingen");

            migrationBuilder.DropIndex(
                name: "IX_BestelArtikelSet_ArtikelId",
                table: "BestelArtikelSet");

            migrationBuilder.DropColumn(
                name: "KlantId",
                table: "Bestellingen");

            migrationBuilder.DropColumn(
                name: "ArtikelId",
                table: "BestelArtikelSet");

            migrationBuilder.DropTable(
                name: "Klant");

            migrationBuilder.AddColumn<int>(
                name: "ArtikelenId",
                table: "BestelArtikelSet",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_BestelArtikelSet_ArtikelenId",
                table: "BestelArtikelSet",
                column: "ArtikelenId");

            migrationBuilder.AlterColumn<DateTime>(
                name: "LeverbaarTot",
                table: "Artikelen",
                nullable: false);

            migrationBuilder.AddForeignKey(
                name: "FK_BestelArtikelSet_Artikelen_ArtikelenId",
                table: "BestelArtikelSet",
                column: "ArtikelenId",
                principalTable: "Artikelen",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
