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

            services.AddMediatR(AppDomain.CurrentDomain.Load("TrendContext.Domain"));

            services.AddControllers();

            services.AddSwaggerGen(c =>
            {
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
                app.UseDeveloperExceptionPage();
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

            app.UseSwaggerUI(c =>
            {
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
                Id = Guid.Parse("4786FC4D-1B0C-4959-8459-0E871B104D3C"),
                CheckingAccountAmount = 234M,
                CPF = "69686332804",
                Name = "Rebeca Carolina Fabiana Peixoto",
            };

            var priscila = new User
            {
                Id = Guid.Parse("08211958-1DB0-48B4-886E-125079D98836"),
                CheckingAccountAmount = 451,
                CPF = "73060084122",
                Name = "Priscila Vanessa Vitória Ferreira",
            };

            context.Users.Add(rebeca);
            context.Users.Add(priscila);

            var petr4 = new Trend
            {
                Id = Guid.Parse("08211958-1DB0-48B4-886E-125079D98836"),
                Symbol = "PETR4",
                CurrentPrice = 28.44M,
            };

            var mglu3 = new Trend
            {
                Id = Guid.Parse("CB1F9520-9DEE-41B4-849F-FACD6D046BF3"),
                Symbol = "MGLU3",
                CurrentPrice = 25.91M,
            };

            var vvar3 = new Trend
            {
                Id = Guid.Parse("3D97A2F8-1D6B-4CA8-96E8-969DA6B64E38"),
                Symbol = "VVAR3",
                CurrentPrice = 25.91M,
            };

            var sanb11 = new Trend
            {
                Id = Guid.Parse("0A490622-F0BC-41B4-9EDB-1F638B76AD11"),
                Symbol = "SANB11",
                CurrentPrice = 40.77M,
            };

            var toro4 = new Trend
            {
                Id = Guid.Parse("D8912C3A-5780-4EF8-A15D-6A2E291B92E7"),
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
                Id = Guid.Parse("C16D2DB5-A50A-40F0-BD77-F9FBB77D819B"),
                UserId = rebeca.Id,
                TrendId = petr4.Id,
                Amount = 2,
            };

            var order2 = new Order
            {
                Id = Guid.Parse("DAEC7DD6-E4B2-4034-A297-95D4C5D2A87C"),
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
