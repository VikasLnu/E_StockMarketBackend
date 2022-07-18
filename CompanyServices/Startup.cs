using AutoMapper;
using BusinessLayer;
using BusinessLayer.Interfaces;
using DataLayer.DBContext;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UItility;

namespace CompanyServices
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

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "CompanyServices", Version = "v1" });
            });
            services.Configure<MongoDBSettings>(Configuration.GetSection("MongoDB"));

            services.AddTransient<IDbContext, DbContext>();
            services.AddTransient<ICompanyServices, BusinessLayer.CompanyServices>();
            //services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            var mapperConfig = new MapperConfiguration(mc => {
                mc.AddProfile(new AutoMapperProfile());
            });
            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
          //  loggerFactory.AddFile("Logs/{Date}.txt");
        
            if (env.IsDevelopment())
            {
                //  app.UseDeveloperExceptionPage();
                app.UseMiddleware<ExceptionMiddleware>();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "CompanyServices v1"));
            }
            else
            {
                app.UseMiddleware<ExceptionMiddleware>();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
