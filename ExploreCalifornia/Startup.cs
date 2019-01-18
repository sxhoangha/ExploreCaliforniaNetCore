using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExploreCalifornia.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ExploreCalifornia
{
    public class Startup
    {
        private readonly IConfiguration configuration;
        public Startup(IConfiguration config)
        {
            configuration = config;
            //configuration = new ConfigurationBuilder().AddEnvironmentVariables().Build();
        }
        
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<FeatureToggles>(x => new FeatureToggles {
                EnableDeveloperExceptions = configuration.GetValue<bool>("EnableDeveloperExceptions")
            });
            services.AddTransient<FormattingService>();
            services.AddTransient<SpecialsDataContext>();
            services.AddDbContext<BlogDataContext>(options =>
            {
                var connectionString = configuration.GetConnectionString("BlogDataContext");
                options.UseSqlServer(connectionString);
            });
            services.AddDbContext<IdentityDataContext>(options =>
            {
                var connectionString = configuration.GetConnectionString("IdentityDataContext");
                options.UseSqlServer(connectionString);
            });

            services.AddIdentity<IdentityUser, IdentityRole>().
                AddEntityFrameworkStores<IdentityDataContext>(); // inject identity service
            services.AddMvc();
            
        }
                
        

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, FeatureToggles feature)
        {
            //app.UseExceptionHandler("/error.html");

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseStatusCodePages();
            }
            else
            {
                app.UseExceptionHandler("/error.html");
            }

            //if (feature.EnableDeveloperExceptions)
            //{
            //    app.UseDeveloperExceptionPage();
            //}

            app.Use(async (context, next) =>
            { 
                if (context.Request.Path.Value.Contains("invalid"))
                {
                    throw new Exception("Error");
                    
                }
                await next();

            });

            app.UseIdentity();

            app.UseMvc(routes =>
            {
                routes.MapRoute("Default", "{controller=Home}/{action=Index}/{id?}");
            });
            
            app.UseFileServer();
            
        }
    }
}
