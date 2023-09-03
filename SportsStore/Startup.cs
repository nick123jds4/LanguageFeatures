using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SportsStore.Models;
using SportsStore.Models.Data;
using SportsStore.Models.Interfaces;
using SportsStore.Models.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsStore
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            //����������� ��������� ������ � ������
            services.AddMemoryCache();
            //������������ ������, ������������ ��� ������� � ������  ������
            services.AddSession();
            //����������� ������ - � ���������� ������� ��� ������ Cart
            //����� �������������� ����� �������� �������� SessionCart,
            //������� ������������� ���� ���� ��� ������ ������, ����� ��� ��������������.
            services.AddScoped(sp => SessionCart.GetCart(sp)); ;
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddDbContext<DataContext>((options)=> {
                options.UseSqlServer(_configuration["Data:SportsStore:ConnectionString"]);
            });
            services.AddDbContext<AppIdentityDbContext>(options=> {
                options.UseSqlServer(_configuration["Data:SportsStoreIdentity:ConnectionString"]);
            });
            services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<AppIdentityDbContext>()
                .AddDefaultTokenProviders();

            services.AddTransient<IProductRepository, EFProductRepository>();
            services.AddTransient<IOrderRepository, EFOrderRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseStatusCodePages();
            }
            else {
                app.UseExceptionHandler("/Error");
            }
            app.UseStatusCodePages();
            app.UseStaticFiles();
            //��������� ������� ������� ������������� ������������� ������� � ��������,
            //����� ��� ��������� �� �������.
            app.UseSession();
            //��� ��������� �����������, ������� ����� ������������� ������� � ������ ��� ���������
            //�������� ������������.
            app.UseAuthentication();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            { 
                //endpoints.MapGet("Test", async context=>await context.Response.WriteAsync("hello world"));

                //���������
                endpoints.MapControllerRoute(
                    name: null,
                    pattern: "{category}/Page{productPage:int}",
                    defaults: new { controller = "Product", action = "List" }
                    ); 
                //���������
                endpoints.MapControllerRoute(
                    name:null,
                    pattern: "Page{productPage:int}",
                    defaults: new { controller = "Product", action = "List", productPage = 1 }
                    );
                //���������
                endpoints.MapControllerRoute(
                    name: null,
                    pattern: "{category}",
                    defaults: new { controller = "Product", action = "List", productPage = 1 }
                    );
                //���������
                endpoints.MapControllerRoute(
                    name: null,
                    pattern: "",
                    defaults: new { controller = "Product", action = "List", productPage = 1 });

                endpoints.MapControllerRoute(
                    name:"default", 
                    pattern:"{controller=Product}/{action=List}/{id?}");
            });

            //SeedData.EnsurePopulated(app);
            //IdentitySeedData.EnsurePopulated(app);
        }
    }
}
