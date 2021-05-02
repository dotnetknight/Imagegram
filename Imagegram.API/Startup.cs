using Autofac;
using Autofac.Extensions.DependencyInjection;
using FluentValidation.AspNetCore;
using Imagegram.API.Filters;
using Imagegram.IoC;
using Imagegram.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Filters;
using System;
using System.IO;
using System.Reflection;

namespace Imagegram.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddHttpCacheHeaders((expirationModelOptions) =>
            {
                expirationModelOptions.MaxAge = 60;
                expirationModelOptions.CacheLocation = Marvin.Cache.Headers.CacheLocation.Public;
            },
            (validationModelOptions) =>
            {
                validationModelOptions.MustRevalidate = true;
            });

            services.AddResponseCaching();

            services.AddAutoMapper(typeof(Startup));

            services.AddControllers(options =>
            {
                options.ReturnHttpNotAcceptable = true;

                options.Filters.Add<ValidationFilter>();
                options.Filters.Add<ApiExceptionAttribute>();
                options.Filters.Add(new ProducesAttribute("application/json", "application/xml", "application/vnd.marvin.hateoas+json"));
                options.Filters.Add(new ConsumesAttribute("application/json", "application/xml", "multipart/form-data"));
            })
               .AddFluentValidation(fvc => fvc.RegisterValidatorsFromAssemblyContaining<Startup>())
               .AddNewtonsoftJson(options =>
               {
                   options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                   options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
               })
               .AddXmlDataContractSerializerFormatters();

            services.AddSwaggerGen(x =>
            {
                x.ExampleFilters();

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                x.IncludeXmlComments(xmlPath);

                x.OperationFilter<HeadersParameterFilter>();
            });

            services.AddSwaggerExamplesFromAssemblyOf<Startup>();

            services.AddDbContext<ImagegramDbContext>(options => options
            .UseLazyLoadingProxies()
            .UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            var container = new ContainerBuilder();
            IoCService.RegisterServices(container, services);

            container.Populate(services);

            return new AutofacServiceProvider(container.Build());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Imagegram v1"));
            }

            app.UseHttpsRedirection();

            //app.UseStaticFiles();
            //app.UseStaticFiles(new StaticFileOptions()
            //{
            //    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), @"Resources")),
            //    RequestPath = new PathString("/Resources")
            //});

            app.UseHttpCacheHeaders();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
