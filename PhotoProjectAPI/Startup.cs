﻿using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using PhotoProjectAPI.Data;

namespace PhotoProjectAPI
{
    public class Startup
    {
        // Dodaje komentarz zeby sprawdzic commita

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSwaggerGen(o=>o.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
            {
                Title="MyAPI",
                Version="v1",
                Description = "Des1"
            }));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        { 
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1"));

            // Do appDbSeedera 
            var serviceProvider = app.ApplicationServices;
            using (var scope = serviceProvider.CreateScope())
            {
                var seeder = scope.ServiceProvider.GetRequiredService<AppDbSeeder>();
                seeder.SeedDatabase();
                seeder.SeedRolesAsync().Wait();
                seeder.SeedUsersAsync().Wait();
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
