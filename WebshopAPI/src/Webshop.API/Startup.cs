﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using rabbitmq_demo;
using RabbitMQ.Client;
using Microsoft.EntityFrameworkCore;
using MySQL.Data.EntityFrameworkCore.Extensions;
using System;
using Webshop.Database;

namespace Webshop.API
{
    public class Startup
    {
        private IHostingEnvironment _env { get; set; }

        public Startup(IHostingEnvironment env)
        {
            _env = env;
            var options = new DbContextOptions<WebshopContext>();
            if(env.IsDevelopment())
            {
                options = new DbContextOptionsBuilder<WebshopContext>().UseInMemoryDatabase("DevelopmentTesting").Options; 
            }
            else
            {
                options = new DbContextOptionsBuilder<WebshopContext>()
               .UseMySql(Environment.GetEnvironmentVariable("MYSQL_CONNECTION"))
               .Options;

                using (var context = new WebshopContext(options))
                {
                    context.Database.Migrate();
                }
            }

            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.

            var connection = new ConnectionFactory()
                .FromEnvironment();
            services.AddSingleton<ISender>(s => new Sender(connection, "Kantilever"));
            services.AddMvc();
            services.AddCors();
            services.AddSwaggerGen();           

            if (_env.IsDevelopment())
            {
                services.AddDbContext<WebshopContext>(options => options.UseInMemoryDatabase("DevelopmentTesting"));
            }
            else
            {
                services.AddDbContext<WebshopContext>(options => options
                    .UseMySql(Environment.GetEnvironmentVariable("MYSQL_CONNECTION")));
            }
            services.AddScoped<IWebshopContext, WebshopContext>(p =>
                p.GetService<WebshopContext>()
            );
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseCors(builder =>
            {
                builder.AllowAnyHeader();
                builder.AllowAnyOrigin();
                builder.AllowAnyMethod();
            });

            app.UseMvc();
            app.UseSwagger();
            app.UseSwaggerUi();
        }
    }
}
