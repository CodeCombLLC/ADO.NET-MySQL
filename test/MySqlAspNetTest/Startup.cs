using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using MySqlAspNetTest.Models;

namespace MySqlAspNetTest
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(); 
            services.AddDbContext<MyContext>(x => x.UseMySql("server=localhost;database=aspnetmysql;uid=root;pwd=19931101"));
            //services.AddDbContext<MyContext>(x => x.UseNpgsql("User ID=postgres;Password=123456;Host=localhost;Port=5432;Database=aspnetpg;"));
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            //loggerFactory.AddConsole();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            var DB = app.ApplicationServices.GetRequiredService<MyContext>();
            DB.Database.EnsureCreated();

            app.UseMvcWithDefaultRoute();
        }
    }
}
