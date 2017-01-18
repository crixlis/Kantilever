using Microsoft.EntityFrameworkCore;
using System;

public class WebshopContext: DbContext, IWebshopContext
{
    public WebshopContext(DbContextOptions<WebshopContext> options) : base(options) { }

    public DbSet<Artikel> Artikelen { get; set; }
}