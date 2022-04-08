using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

namespace Practice___Two
{
    public class Startup
    {
        public Startup(IWebHostEnvironment environment)
        {
            var builder = new ConfigurationBuilder().SetBasePath(environment.ContentRootPath)
            .AddJsonFile("appsettings.json", reloadOnChange: true, optional: false)
            .AddJsonFile($"appsettings.{environment.EnvironmentName}.json", reloadOnChange: true, optional: false)
            .AddEnvironmentVariables();
     
            Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();

            services.AddSwaggerGen(options =>
            {
                var groupName = "V1.0";

                options.SwaggerDoc(groupName, new OpenApiInfo
                {
                    Title = $"{Configuration.GetSection("Application").GetSection("Tittle").Value} {groupName}",
                    Version = groupName,
                    Description = "Practica 2",
                    Contact = new OpenApiContact
                    {
                        Name = "Douglas Company",
                        Email = string.Empty,
                        Url = new Uri("https://MyfirstWA.com/"),
                    }
                });
            });

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(options => options.SwaggerEndpoint("/swagger/v1/swagger.json", "Practice-Two v1"));
            }

            app.UseHttpsRedirection();

            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "My first webApi V1.0");
            });

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
