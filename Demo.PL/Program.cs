using Demo.BLL.Interfaces;
using Demo.BLL.Repositories;
using Demo.DAL.Data;
using Demo.DAL.Models;
using Demo.PL.Helpers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.PL
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var Builder = WebApplication.CreateBuilder(args);

            #region Configure Services that allow Dependancy injection
            Builder.Services.AddControllersWithViews();


            //services.AddSingleton<AppDbContext>();//life time per Application
            //services.AddTransient<AppDbContext>();//per operation
            //services.AddScoped<AppDbContext>(); // life time per request

            Builder.Services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(Builder.Configuration.GetConnectionString("DefaultConnection"));
            }/*,ServiceLifetime.Singleton*/)/*.AddApplicationServices()*/; // default =>AddScoped to change =>ServiceLifetime.000

            //ApplicationServicesExtentions.AddApplicationServices(services); // static method
            //services.AddApplicationServices(); //Extentions Method
            Builder.Services.AddAutoMapper(M => M.AddProfile(new MappingProfiles())); // per operation =>AddTransient
            Builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

            //services.AddScoped<UserManager<ApplicationUser>>();
            //services.AddScoped<SignInManager<ApplicationUser>>();
            //services.AddScoped<RoleManager<IdentityRole>>();

            Builder.Services.AddIdentity<ApplicationUser, IdentityRole>(c =>
            {
                //c.Password.RequiredUniqueChars = 2;
                //c.Password.RequireDigit = true;
                //c.Password.RequireNonAlphanumeric = true;
                //c.Password.RequireUppercase = true;
                //c.Password.RequireLowercase = true;
                //c.User.RequireUniqueEmail = true;
                //c.Lockout.MaxFailedAccessAttempts = 3;
                //c.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(10);
                //c.Lockout.AllowedForNewUsers = true;
            }).AddEntityFrameworkStores<AppDbContext>()
              .AddDefaultTokenProviders();
            Builder.Services.ConfigureApplicationCookie(config =>
            {
                config.LoginPath = "/Account/SignIn";
                config.ExpireTimeSpan = TimeSpan.FromMinutes(10);
            });

            //services.AddAuthentication("")
            //        .AddCookie("Hamada", conf =>
            //        {
            //            conf.LogoutPath = "/Account/SignIn";
            //            conf.AccessDeniedPath = "/Home/Error";
            //        });
            #endregion

            #region Configure Http Request piplines
            var app = Builder.Build();
            if (Builder.Environment.IsDevelopment())
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
            app.UseAuthentication();
            app.UseAuthorization();
            

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
            #endregion

            app.Run();

        }

        //public static IHostBuilder CreateHostBuilder(string[] args) =>
        //    Host.CreateDefaultBuilder(args)
        //        .ConfigureWebHostDefaults(webBuilder =>
        //        {
        //            webBuilder.UseStartup<Startup>();
        //        });
    }
}
