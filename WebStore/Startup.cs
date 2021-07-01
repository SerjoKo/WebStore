using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using WebStore.Inftastructure.MidleWare;
using WebStore.Servicess;
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

            services.AddSingleton<IEmployeesData, InMemoryEmployeesData>();

            services.AddSingleton<IProductData, InMemoryProductData>();

            //services.AddScoped<IEmployeesData, InMemoryEmployeesData>();

            //services.AddTransient<IEmployeesData, InMemoryEmployeesData>();

            services.AddControllersWithViews().AddRazorRuntimeCompilation();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider services)
        {
            //var test_service = services.GetRequiredService<ITestService>();

            //test_service.Test();

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