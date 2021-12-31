using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheaterDAL;
using TheaterDAL.Entities;
using TheaterLogic.Abstraction.IMappers;
using TheaterLogic.Abstraction.IServices;
using TheaterLogic.Implementation.Mappers;
using TheaterLogic.Implementation.Services;
using TheaterLogic.Models;

namespace TheaterWeb
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
            services.AddControllersWithViews();

            services.AddTransient<TheaterContext>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddTransient<IMapper<PlaceEntity, Place>, PlaceMapper>();
            services.AddTransient<IMapper<AuthorEntity, Author>, AuthorMapper>();
            services.AddTransient<IMapper<ShowEntity, Show>, ShowMapper>();
            services.AddTransient<IMapper<TicketEntity, Ticket>, TicketMapper>();
            services.AddTransient<ITicketService, TicketService>();
            services.AddTransient<IShowService, ShowService>();
            services.AddTransient<IAuthorService, AuthorService>();


            services.AddTransient<TicketService>();
            services.AddTransient<ShowService>();
            services.AddTransient<AuthorService>();

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
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
