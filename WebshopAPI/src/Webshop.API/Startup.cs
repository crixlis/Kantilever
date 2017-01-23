using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using rabbitmq_demo;
using RabbitMQ.Client;
using Microsoft.EntityFrameworkCore;
using MySQL.Data.EntityFrameworkCore.Extensions;

namespace Webshop.API
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var options = new DbContextOptions<WebshopContext>();
            if(env.IsDevelopment())
            {
                options = new DbContextOptionsBuilder<WebshopContext>().UseInMemoryDatabase("DevelopmentTesting").Options; 
            }
            else
            {
                options = new DbContextOptionsBuilder<WebshopContext>()
               .UseMySQL(Configuration.GetConnectionString("DefaultConnection"))
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

            services.AddScoped<IWebshopContext, WebshopContext>(p =>
                p.GetService<WebshopContext>()
            );

            services.AddDbContext<WebshopContext>(options => options
                .UseSqlServer(@"server=lmf-webfrontend.api.database;userid=root;pwd=my-secret-pw;port=3306;database=ArtikelenKantilever;sslmode=none;"));
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
