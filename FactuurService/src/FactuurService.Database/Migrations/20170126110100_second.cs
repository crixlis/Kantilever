using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FactuurService.Database.Migrations
{
    public partial class second : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Bestellingen",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    KlantId = table.Column<int>(nullable: true)
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

            migrationBuilder.AddColumn<int>(
                name: "BestellingId",
                table: "Artikel",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Artikel_BestellingId",
                table: "Artikel",
                column: "BestellingId");

            migrationBuilder.CreateIndex(
                name: "IX_Bestellingen_KlantId",
                table: "Bestellingen",
                column: "KlantId");

            migrationBuilder.AddForeignKey(
                name: "FK_Artikel_Bestellingen_BestellingId",
                table: "Artikel",
                column: "BestellingId",
                principalTable: "Bestellingen",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Artikel_Bestellingen_BestellingId",
                table: "Artikel");

            migrationBuilder.DropIndex(
                name: "IX_Artikel_BestellingId",
                table: "Artikel");

            migrationBuilder.DropColumn(
                name: "BestellingId",
                table: "Artikel");

            migrationBuilder.DropTable(
                name: "Bestellingen");
        }
    }
}
