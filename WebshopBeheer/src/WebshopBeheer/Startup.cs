using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MySQL.Data.EntityFrameworkCore.Extensions;
using WebshopBeheer.Data;
using WebshopBeheer.Models;
using WebshopBeheer.Services;

namespace WebshopBeheer
{
    public class Startup
    {
        private IHostingEnvironment _env { get; set; }

        public Startup(IHostingEnvironment env)
        {
            _env = env;
            var builder = new ConfigurationBuilder()
            .SetBasePath(env.ContentRootPath)
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

            if (env.IsDevelopment())
            {
                // For more details on using the user secret store see http://go.microsoft.com/fwlink/?LinkID=532709
                builder.AddUserSecrets();
            }

            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //Add framework services.
            if (_env.IsDevelopment())
            {
                services.AddDbContext<ApplicationDbContext>(options => options.UseInMemoryDatabase("DevelopmentTesting"));

                services.AddDbContext<CommercieelManager.Database.CommercieelManagerContext>(options => options.UseInMemoryDatabase("DevelopmentTesting2"));
                services.AddDbContext<MagazijnMedewerker.Database.MagazijnMedewerkerContext>(options => options.UseInMemoryDatabase("DevelopmentTesting3"));
            }
            else
            {
                services.AddDbContext<ApplicationDbContext>(options =>
                    options.UseMySQL(Configuration.GetConnectionString("DefaultConnection")));

                services.AddDbContext<CommercieelManager.Database.CommercieelManagerContext>(options =>
                    options.UseMySQL(Configuration.GetConnectionString("DefaultConnection")));
                services.AddDbContext<MagazijnMedewerker.Database.MagazijnMedewerkerContext>(options =>
                    options.UseMySQL(Configuration.GetConnectionString("DefaultConnection")));
            }

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.AddMvc();

            // Add application services.
            services.AddTransient<IEmailSender, AuthMessageSender>();
            services.AddTransient<ISmsSender, AuthMessageSender>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/CommercieelManager/Error");
            }

            app.UseStaticFiles();

            app.UseIdentity();


            if (!env.IsDevelopment())
            {
                using (var context = app.ApplicationServices.GetService<ApplicationDbContext>())
                {
                    context.Database.Migrate();
                }

                using (var context = app.ApplicationServices.GetService<CommercieelManager.Database.CommercieelManagerContext>())
                {
                    context.Database.Migrate();
                }

                using (var context = app.ApplicationServices.GetService<MagazijnMedewerker.Database.MagazijnMedewerkerContext>())
                {
                    context.Database.Migrate();
                }
            }

            // Add external authentication middleware below. To configure them please see http://go.microsoft.com/fwlink/?LinkID=532715

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=CommercieelManager}/{action=Index}/{id?}");
            });
        }
    }
}
