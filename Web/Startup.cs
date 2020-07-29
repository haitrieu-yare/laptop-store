using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace laptop_store
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            // Allow program to use MVC
            services.AddMvc(); 
            // Allow program to use Session
            services.AddSession(options => {
                options.IdleTimeout = TimeSpan.FromMinutes(30);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            // Allows program to use static files (html, css, js...)
            app.UseStaticFiles(); 
            app.UseSession();
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                // Default routing
                endpoints.MapDefaultControllerRoute(); 
            });
        }
    }
}
