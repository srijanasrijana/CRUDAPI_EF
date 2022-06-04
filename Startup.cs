using CRUDWEPAPI_EF.Config;
using CRUDWEPAPI_EF.Repositories;
using CRUDWEPAPI_EF.Repositories.EmployeeService;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRUDWEPAPI_EF
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
            //services.AddControllers(); 
            services.AddControllers().AddNewtonsoftJson(x =>
x.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

            services.AddDbContext<ApplicationDBContext>(item => item.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<ICustomer, CustomerService>();
            services.AddScoped<IOrder, OrderService>();

            services.AddScoped<IEmployee, EmployeeServices>();

            services.AddSwaggerGen();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

               
            }
            //else
            //{
            //    app.UseExceptionHandler(
            //        options =>
            //        {
            //            options.Run(
            //                async context =>
            //                {

            //                }
            //                )
            //        }
            //        )
            //}

            //if (!app.Environment.IsDevelopment())
            //{
            //    app.UseExceptionHandler("/error");
            //}
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
                //options.RoutePrefix = string.Empty;
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
