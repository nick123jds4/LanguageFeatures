using Filters.Infrastructure;
using Filters.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Filters
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
            /*все фильтры для работы с одиночным запросом, получают один объект*/
            /*это основа для разделения данных контекста между фильтрами*/
            //services.AddScoped<IFilterDiagnostics, DefaultFilterDiagnostics>();
            /*для аттрибута [ServiceFilter], который применяет поставщик служб для создания объекта фильтра*/
            //services.AddSingleton<IFilterDiagnostics, DefaultFilterDiagnostics>();
            //services.AddSingleton<TimerFilter>();
            /*Создание глобального фильтра*/
            services.AddScoped<IFilterDiagnostics, DefaultFilterDiagnostics>();
            services.AddScoped<TimeFilter>();
            services.AddScoped<ViewResultDiagnostics>();
            services.AddScoped<DiagnosticsFilter>(); 
            
            services.AddMvc(options =>
            { 
                options.EnableEndpointRouting = false;
                //options.Filters.AddService(typeof(ViewResultDiagnostics));
                options.Filters.AddService(typeof(DiagnosticsFilter));
                options.Filters.Add(new MessageAttribute("This is the Globally-scoped filter"));
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStatusCodePages();
            app.UseStaticFiles();
            app.UseMvcWithDefaultRoute();
        }
    }
}
