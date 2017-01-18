using Microsoft.EntityFrameworkCore;
using System;

public class WebshopContext: DbContext
{
    public WebshopContext(DbContextOptions<WebshopContext> options) : base(options) { }
}