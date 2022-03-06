using AmadeusScanner.Model.Settings;
using AmadeusScanner.Repository.Airport;
using AmadeusScanner.Repository.Common;
using AmadeusScanner.Repository.Common.Airport;
using AmadeusScanner.Repository.Common.Currency;
using AmadeusScanner.Repository.Common.Flight;
using AmadeusScanner.Repository.Data;
using AmadeusScanner.Repository.Flight;
using AmadeusScanner.Repository.Repositories;
using AmadeusScanner.Repository.Repositories.Currency;
using AmadeusScanner.Service.Airport;
using AmadeusScanner.Service.Amadeus;
using AmadeusScanner.Service.Clients;
using AmadeusScanner.Service.Common.Airport;
using AmadeusScanner.Service.Common.Amadeus;
using AmadeusScanner.Service.Common.Currency;
using AmadeusScanner.Service.Common.Flight;
using AmadeusScanner.Service.Currency;
using AmadeusScanner.Service.Flight;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace AmadeusScanner.API
{
    public class Startup
    {
        private readonly IConfiguration config;

        public Startup(IConfiguration config)
        {
            this.config = config;
        }


        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<AppSettings>(options => config.Bind(options));
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "AmadeusScanner.API", Version = "v1" });
            });

            services.AddDbContext<DataContext>(o => 
            {
                o.UseSqlite(config.GetConnectionString("DefaultConnection"));
            });

            services.AddHttpClient<AmadeusHttpClient>();

            #region Automapper

            var path = AppDomain.CurrentDomain.BaseDirectory;
            var assemblyPatterns = new[] { "AmadeusScanner.*.dll" };
            var assembliesToScan = new List<IList<Assembly>>();

            foreach (var pattern in assemblyPatterns)
            {
                var assemblies = Directory.GetFiles(path, pattern).Select(Assembly.LoadFrom).ToList();
                assembliesToScan.Add(assemblies);
            }

            services.AddAutoMapper(assembliesToScan.SelectMany(p => p).ToArray());

            #endregion Automapper

            
            services.AddScoped<ICurrencyRepository, CurrencyRepository>();
            services.AddScoped<IAirportRepository, AirportRepository>();
            services.AddScoped<IFlightRepository, FlightRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<IAirportService, AirportService>();
            services.AddScoped<IFlightSearchService, FlightSearchService>();
            services.AddScoped<IFlightService, FlightService>();
            services.AddScoped<ICurrencyService, CurrencyService>();
            services.AddScoped<IAmadeusService, AmadeusService>();


            services.AddMediatR(typeof(AmadeusList).Assembly);

            services.AddCors(opt =>
            {
                opt.AddPolicy("CorsPolicy", policy =>
                {
                    policy.AllowAnyMethod().AllowAnyHeader().WithOrigins("http://localhost:3000");
                });
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "AmadeusScanner.API v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors("CorsPolicy");

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
