using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Webshop.Database.Migrations
{
    public partial class fileNameExtension : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                "USE ArtikelenKantilever;UPDATE Artikelen SET ImagePath = CONCAT(ImagePath, \".png\");");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
