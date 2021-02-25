using System.Reflection;
using Autofac;
using FigoparaCaseStudyApi.Entities.Db;
using FigoparaCaseStudyApi.Filters;
using FigoparaCaseStudyApi.Modules;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Serilog;

namespace FigoparaCaseStudyApi
{
    public class Startup
    {
        private readonly IWebHostEnvironment Environment;
        private readonly IConfiguration Configuration;

        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            Environment = environment;

            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var assemblies = Assembly.GetExecutingAssembly();
            services.AddOptions();

            services.AddControllers()
                     //(options => options.Filters.Add<ValidateModelStateAttribute>())
                    .AddFluentValidation(fv => fv.RegisterValidatorsFromAssembly(assemblies));

            services.AddDbContext<UserDbContext>(options => options.UseSqlite(Configuration.GetConnectionString("Db")));

            services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new OpenApiInfo {Title = "Figopara Case Study Api", Version = "v1"}); });

            services.Configure<ApiBehaviorOptions>(options => { options.SuppressModelStateInvalidFilter = true; });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "FigoparaCaseStudyApi v1"));
            }

            app.UseSerilogRequestLogging();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterModule(new ApiServiceModule());
        }
    }
}