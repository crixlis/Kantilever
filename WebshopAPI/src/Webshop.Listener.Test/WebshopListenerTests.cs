﻿using Xunit;
using NSubstitute;
using Microsoft.EntityFrameworkCore;
using System;
using rabbitmq_demo;
using System.Linq;
using System.Data.SqlClient;
using MySQL.Data.EntityFrameworkCore.Extensions;
using System.IO;
using Webshop.Database;

namespace Webshop.Listener.Test
{
    public class WebshopListenerTests
    {
        [Fact]
        public void IkWilEenArtikelAanCatalogusToegevoegdEventOpvangenEnOpslaanInDeDatabase()
        {
            var options = new DbContextOptionsBuilder<WebshopContext>()
                .UseInMemoryDatabase(databaseName: "ArtikelAanCatalogusToevoegen")
                .Options;

            using (var context = new WebshopContext(options))
            {
                //Arrange
                var sender = Substitute.For<ISender>();
                var service = new WebshopListenerService(sender, context, Environment.GetEnvironmentVariable("IMG_ROOT"));
                var artikelToegevoegd = new ArtikelAanCatalogusToegevoegd();

                //Act
                service.Execute(artikelToegevoegd);

                //Assert
                Assert.True(context.Artikelen.Any());
            }
        }

        [Fact]
        public void IkWilEenArtikelVoorraadBijgewerktEventOpvangenEnOpslaanInDeDatabase()
        {
            var options = new DbContextOptionsBuilder<WebshopContext>()
                .UseInMemoryDatabase(databaseName: "ArtikelVoorraadBijgewerktOpslaan")
                .Options;

            using (var context = new WebshopContext(options))
            {
                //Arrange
                var sender = Substitute.For<ISender>();
                var service = new WebshopListenerService(sender, context, Environment.GetEnvironmentVariable("IMG_ROOT"));
                var artikel = new ArtikelVoorraadBijgewerkt();

                //Act
                service.Execute(artikel);

                //Assert
                Assert.True(context.Artikelen.Any());
            }
        }

        [Fact]
        public void IkWilEenArtikelVoorraadBijgewerktEventUpdatenEnOpslaanInDeDatabase()
        {
            var options = new DbContextOptionsBuilder<WebshopContext>()
                .UseInMemoryDatabase(databaseName: "ArtikelVoorraadBijgewerktUpdaten")
                .Options;

            using (var context = new WebshopContext(options))
            {
                //Arrange
                var sender = Substitute.For<ISender>();
                var service = new WebshopListenerService(sender, context, Environment.GetEnvironmentVariable("IMG_ROOT"));

                var id = 12;
                context.Artikelen.Add(new Artikel { Id = id, Voorraad = 1 });
                context.SaveChanges();

                var artikelMetGeupdateVoorraad = context.Artikelen.Where(a => a.Id == id).SingleOrDefault();
                var nieuweVoorraad = artikelMetGeupdateVoorraad.Voorraad = 2;

                //Act
                service.Execute( new ArtikelAanCatalogusToegevoegd { Id = id, Voorraad = nieuweVoorraad });

                //Assert
                Assert.NotEmpty(context.Artikelen.Where(a => a.Voorraad == 2));
            }
        }

        [Fact]
        public void ZelfToevoegenVanIdVanArtikelAanCatalogusToegevoegdAanDB()
        {
            var options = new DbContextOptionsBuilder<WebshopContext>()
                .UseInMemoryDatabase(databaseName: "ZelfArtikelAanCatalogusToevoegen")
                .Options;

            using (var context = new WebshopContext(options))
            {
                //Arrange
                var id = 34;
                var sender = Substitute.For<ISender>();
                var service = new WebshopListenerService(sender, context, Environment.GetEnvironmentVariable("IMG_ROOT"));

                //Act
                context.Artikelen.Add(new Artikel { Id = id });
                context.SaveChanges();

                //Assert
                Assert.NotEmpty(context.Artikelen.Where(a => a.Id == id));
            }
        }
        
        [Fact]
        public void AlsDePropertyAfbeeldingVanHetInkomendeBerichtLeegIsMoetDeByteArrayNietNaarFileGeschrevenWorden()
        {
            var options = new DbContextOptionsBuilder<WebshopContext>()
                .UseInMemoryDatabase(databaseName: "ZelfArtikelAanCatalogusToevoegen")
                .Options;

            using (var context = new WebshopContext(options))
            {
                var sender = Substitute.For<ISender>();
                Environment.SetEnvironmentVariable("IMG_ROOT", @".\img");
                var service = new WebshopListenerService(sender, context, Environment.GetEnvironmentVariable("IMG_ROOT"));

                service.Execute(new ArtikelAanCatalogusToegevoegd { Id = 10, Afbeelding = null});
                
                Assert.False(File.Exists(Path.Combine(Environment.GetEnvironmentVariable("IMG_ROOT"), "10.txt")));
            }
        }

        [Fact]
        public void AlsDePropertyAfbeeldingVanHetInkomendeBerichtEenLegeStringIsMoetDeByteArrayNietNaarFileGeschrevenWorden()
        {
            var options = new DbContextOptionsBuilder<WebshopContext>()
                .UseInMemoryDatabase(databaseName: "ZelfArtikelAanCatalogusToevoegen")
                .Options;

            using (var context = new WebshopContext(options))
            {
                var sender = Substitute.For<ISender>();
                Environment.SetEnvironmentVariable("IMG_ROOT", @".\img");
                var service = new WebshopListenerService(sender, context, Environment.GetEnvironmentVariable("IMG_ROOT"));

                service.Execute(new ArtikelAanCatalogusToegevoegd { Id = 765, Afbeelding =  Array.Empty<byte>() });

                Assert.False(File.Exists(Path.Combine(Environment.GetEnvironmentVariable("IMG_ROOT"), "10.txt")));
            }
        }
    }
}
