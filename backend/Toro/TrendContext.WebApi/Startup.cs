using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using TrendContext.Domain.Data;
using TrendContext.Domain.Data.Interfaces;
using TrendContext.Domain.Entities;
using TrendContext.Domain.Repository.Implementations;
using TrendContext.Domain.Repository.Interfaces;
using TrendContext.Shared.Repository;

namespace TrendContext.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<InMemoryAppContext>(opt => opt.UseInMemoryDatabase(databaseName: "TrendContextTest"));
            services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
            services.AddTransient<ITrendRepository, TrendRepository>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();

            var assembly = AppDomain.CurrentDomain.Load("TrendContext.Domain");
            services.AddMediatR(assembly);

            services.AddControllers();

            services.AddSwaggerGen(c => {
                c.SwaggerDoc("v1",
                    new OpenApiInfo
                    {
                        Title = "Trends API v1",
                        Version = "v1",
                        Description = "API to manager trends",
                        Contact = new OpenApiContact
                        {
                            Name = "Jefferson Luís Nascimento",
                            Url = new Uri("https://github.com/jefferson-luis-nascimento"),
                        }
                    });

                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });

            services.AddCors();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                //app.UseDeveloperExceptionPage();
                app.UseExceptionHandler("/error-local-development");
            }
            else
            {
                app.UseExceptionHandler("/error");
            }

            var options = new DbContextOptionsBuilder<InMemoryAppContext>()
               .UseInMemoryDatabase(databaseName: "TrendContextTest")
               .Options;

            using (var context = new InMemoryAppContext(options))
            {
                AddDefaultData(context);
            }          

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseSwagger();

            app.UseSwaggerUI(c => {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Trends API v1");
            });

            var option = new RewriteOptions();
            option.AddRedirect("^$", "swagger");

            app.UseRewriter(option);

            app.UseCors(builder =>
            {
                builder.WithOrigins("http://localhost:3000");
                builder.AllowAnyHeader();
                builder.WithExposedHeaders("Token-Expired");
                builder.AllowAnyMethod();
                builder.AllowCredentials();
                builder.Build();
            });

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private static void AddDefaultData(InMemoryAppContext context)
        {
            var rebeca = new User
            {
                CheckingAccountAmount = 234M,
                CPF = "69686332804",
                Name = "Rebeca Carolina Fabiana Peixoto",
            };

            var priscila = new User
            {
                CheckingAccountAmount = 451,
                CPF = "73060084122",
                Name = "Priscila Vanessa Vitória Ferreira",
            };

            context.Users.Add(rebeca);
            context.Users.Add(priscila);

            var petr4 = new Trend
            {
                Symbol = "PETR4",
                CurrentPrice = 28.44M,
            };

            var mglu3 = new Trend
            {
                Symbol = "MGLU3",
                CurrentPrice = 25.91M,
            };

            var vvar3 = new Trend
            {
                Symbol = "VVAR3",
                CurrentPrice = 25.91M,
            };

            var sanb11 = new Trend
            {
                Symbol = "SANB11",
                CurrentPrice = 40.77M,
            };

            var toro4 = new Trend
            {
                Symbol = "TORO4",
                CurrentPrice = 115.98M,
            };

            context.Trends.Add(petr4);
            context.Trends.Add(mglu3);
            context.Trends.Add(vvar3);
            context.Trends.Add(sanb11);
            context.Trends.Add(toro4);

            var order1 = new Order
            {
                UserId = rebeca.Id,
                TrendId = petr4.Id,
                Amount = 2,
            };

            var order2 = new Order
            {
                UserId = rebeca.Id,
                TrendId = sanb11.Id,
                Amount = 3,
            };

            context.Orders.Add(order1);
            context.Orders.Add(order2);

            context.SaveChanges();
        }
    }
}
