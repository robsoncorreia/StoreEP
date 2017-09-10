using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SpaServices.Webpack;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using StoreEP.Models;
using Microsoft.AspNetCore.Http;

namespace StoreEP
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
            services.AddScoped<Cart>(sp => SessionCart.GetCart(sp));
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddMvc();
            services.AddNodeServices();
            services.AddMemoryCache();
            services.AddSession();
            services.AddDbContext<StoreEPContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("StoreEPContext")));
            services.AddTransient<IOrderRepository, EFOrderRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseWebpackDevMiddleware(new WebpackDevMiddlewareOptions
                {
                    HotModuleReplacement = true
                });
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();
            app.UseSession();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: null,
                    template: "{category}/Page{page:int}",
                    defaults: new { controller = "Produtos", action = "List" });
                routes.MapRoute(
                    name: null,
                    template: "Page{page:int}",
                    defaults: new { controller = "Produtos", action = "List"});
                routes.MapRoute(
                    name: null,
                    template: "{category}",
                    defaults: new { controller = "Produtos", action = "List"});
                routes.MapRoute(
                    name: null,
                    template: "",
                    defaults: new { controller = "Produtos", action = "List" });
                routes.MapRoute(
                    name: null,
                    template: "{controller=Produtos}/{action=List}/{id?}");
                routes.MapRoute(
                    name: null,
                    template: "",
                    defaults: new { controller = "Produtos", action = "List" });
            });
        }
    }
}
