﻿using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Webshop.Database.Migrations
{
    public partial class Secondmigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Artikelen",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Beschrijving = table.Column<string>(nullable: true),
                    Leverancier = table.Column<string>(nullable: true),
                    LeverancierCode = table.Column<string>(nullable: true),
                    LeverbaarTot = table.Column<DateTime>(nullable: true),
                    LeverbaarVanaf = table.Column<DateTime>(nullable: false),
                    Naam = table.Column<string>(nullable: true),
                    Prijs = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Artikelen", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Artikelen");
        }
    }
}
