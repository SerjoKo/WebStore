using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using WebStore.DAL.Context;
using WebStore.DAL.Context.WebStore.DAL.Context;
using WebStore.Data;
using WebStore.Inftastructure.MidleWare;
using WebStore.Servicess.InMemory;
using WebStore.Servicess.InSQL;
using WebStore.Servicess.Interfaces;

namespace WebStore
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration Configuration)
        {
            this.Configuration = Configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddScoped<ITestService, TestService>();
            //services.AddScoped<IPrinter, DebugPrinter>();

            services.AddDbContext<WebStoreDB>(opt =>
                opt.UseSqlServer(
                    Configuration.GetConnectionString("WSDBSQL")));

            services.AddTransient<WSDBInitializer>();

            //services.AddDbContext<WebStoreDB>(opt => 
            //    opt.UseSqlServer(Configuration.GetConnectionString("WSDBSQL")));

            services.AddSingleton<IEmployeesData, InMemoryEmployeesData>();

            // оставить на всякий случай
            //services.AddSingleton<IProductData, InMemoryProductData>();
            //services.AddScoped<IProductData, SqlProductData>();

            if (Configuration["ProductsDataSource"] == "db")
                services.AddScoped<IProductData, SqlProductData>();
            else
                services.AddSingleton<IProductData, InMemoryProductData>();
            //services.AddScoped<IEmployeesData, InMemoryEmployeesData>();

            //services.AddTransient<IEmployeesData, InMemoryEmployeesData>();

            services.AddControllersWithViews().AddRazorRuntimeCompilation();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider services)
        {
            //var test_service = services.GetRequiredService<ITestService>();

            //test_service.Test();

            using (var scope = services.CreateScope())
                scope.ServiceProvider.GetRequiredService<WSDBInitializer>().Initialize();
                

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseStaticFiles();

            app.UseMiddleware<TestMidleWare>();

            app.UseWelcomePage("/WelcomePage");

            //var greetings = Configuration["Greetings"];
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/greetings", async context =>
                {
                    //await context.Response.WriteAsync(greetings);
                    await context.Response.WriteAsync(Configuration["Greetings"]);
                });

                //endpoints.MapDefaultControllerRoute();
                endpoints.MapControllerRoute(
                    "default",
                    "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }

    //interface ITestService
    //{
    //    void Test();
    //}

    //class TestService : ITestService
    //{
    //    private IPrinter _Printer;

    //    public TestService(IPrinter Printer)
    //    {
    //        _Printer = Printer;
    //    }

    //    public void Test()
    //    {
    //        _Printer.Printer("Запуск теста");
    //        //Debug.WriteLine("Запуск теста");
    //    }
    //}

    //interface IPrinter
    //{
    //    void Printer(string str);
    //}

    //class DebugPrinter : IPrinter
    //{
    //    public DebugPrinter()
    //    {

    //    }

    //    public void Printer(string str)
    //    {
    //        Debug.WriteLine(str);
    //    }
    //}
}