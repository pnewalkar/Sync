using System.Collections.Generic;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Maintel.Icon.Portal.Sync.HighlightAPI
{
    /// <summary>
    /// The startup class, called from Program.cs
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// The configuration property
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// The connection string property
        /// </summary>
        public static string HighlightDBEndpoint { get;  private set;  }  

        /// <summary>
        /// The Highlight connection string property
        /// </summary>
        public static string HighlightConnectionString { get; private set; }  

        /// <summary>
        /// The allowed origins property
        /// </summary>
        public static string AllowedOrigins {  
            get;  
            private set;  
        }  

        /// <summary>
        /// Public method to return the highlight api endpoint
        /// </summary>
        /// <remarks>The highlight api endpoint is stored in the appsettings.json file</remarks>
        public static string GetHighlightDBEndpoint()  
        {  
            return Startup.HighlightDBEndpoint;  
        }

        /// <summary>
        /// Public method to return the highlight connection string
        /// </summary>
        /// <remarks>The highlight connection string is stored in the appsettings.json file</remarks>
        public static string GetHighlightConnectionString()  
        {  
            return Startup.HighlightConnectionString;  
        }

        /// <summary>
        /// Class initialisation
        /// </summary>
        public Startup(IHostingEnvironment env) {  
            Configuration = new ConfigurationBuilder().SetBasePath(env.ContentRootPath).AddJsonFile("appSettings.json").Build();  
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        /// <summary>
        /// The main configuration of the startup class
        /// </summary>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            //Set the connection string to the associated value in appsettings.json
            HighlightDBEndpoint = Configuration["Endpoints:HighlightDBEndpoint"];

            HighlightConnectionString = Configuration["ConnectionStrings:HighlightDB"];
            
            //Set the allowed origins for calls to this service from the appsettings.json
            AllowedOrigins = Configuration["AllowedOrigins"];
            
            app.UseCors(
                //options => options.WithOrigins("http://localhost:8080").AllowAnyMethod().AllowAnyHeader()
                options => options.WithOrigins().AllowAnyMethod().AllowAnyHeader().AllowAnyOrigin()
            );

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    
    }
}
