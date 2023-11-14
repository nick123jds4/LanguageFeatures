using DependencyInjection.Data;
using DependencyInjection.Infrastructure;
using DependencyInjection.Interfaces;
using DependencyInjection.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Host = Microsoft.AspNetCore.Hosting;

namespace DependencyInjection
{
    public class Startup
    {

        public Startup(IConfiguration configuration, Host.IHostingEnvironment hostEnv)
        {
            _hostEnv = hostEnv;
            Configuration = configuration;
        }

        private Host.IHostingEnvironment _hostEnv;

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            TypeBroker.SetRepositoryType<AlternateRepository>();
            //services.AddScoped<IRepository, MemoryRepository>();
            services.AddSingleton<IRepository, MemoryRepository>();

            //сообщает поставщику служб о том что он должен создавать новый экземпл€р типа реализации вс€кий раз, когда нужно распознать зависимость.
            //services.AddTransient<IRepository>(provider=> {
            //    if (_hostEnv.IsDevelopment()) {
            //        var x = provider.GetService<MemoryRepository>();

            //        return x;
            //    }
            //    else
            //    {
            //        return new AlternateRepository();
            //    }
            //});
            //services.AddTransient<MemoryRepository>(); 
            //services.AddTransient<IRepository, MemoryRepository>();
            services.AddTransient<IModelStorage, DictionaryStorage>();
            services.AddTransient<ProductTotalizer>();
            services.AddMvc(options=>options.EnableEndpointRouting=false);
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
            app.UseStaticFiles();
            app.UseStatusCodePages();
            app.UseMvcWithDefaultRoute();
             
        }
    }
}
