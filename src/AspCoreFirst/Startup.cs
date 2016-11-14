using AspCoreFirst.Context;
using AspCoreFirst.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using AspCoreFirst.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Globalization;
using System.Collections.Generic;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Routing;

namespace AspCoreFirst
{
    public class Startup
    {
        public Startup()
        {
            //config = new ConfigurationBuilder().AddInMemoryCollection(new Dictionary<string, string>() { {"name","Eugeny"} }).Build();
            config =
                new ConfigurationBuilder()
                    .SetBasePath(System.IO.Directory.GetCurrentDirectory())
                    .AddJsonFile("config.json")
                    .Build();

        }
        public void ConfigureServices(IServiceCollection services)
        {
            var connection = config.GetConnectionString("DefaultConnection");
            services.AddLocalization(x => x.ResourcesPath = "Resources");
            services.AddDbContext<MyDbContext>(options => options.UseSqlServer(connection));
            services.AddMvc().AddDataAnnotationsLocalization().AddViewLocalization();
            services.AddTransient(typeof (IService), typeof (ServiceSayHello));
            services.AddIdentity<ApplicationUser, IdentityRole<Guid>>(option=> 
            {
                option.Password.RequireNonAlphanumeric = false;
                option.Password.RequireLowercase = false;
                option.Password.RequiredLength = 0;
                option.Password.RequireUppercase = false;
                option.Password.RequireDigit = false;
                option.SignIn.RequireConfirmedEmail = true;
                option.User.RequireUniqueEmail = true;
                option.Cookies.ApplicationCookie.AccessDeniedPath = @"/Home/Index";
                option.Cookies.ApplicationCookie.LoginPath = @"/Home/Index";
                option.Cookies.ApplicationCookie.ExpireTimeSpan = new TimeSpan(24, 0, 0);

            })
                .AddEntityFrameworkStores<MyDbContext, Guid>()
                .AddDefaultTokenProviders();
            services.AddTransient(typeof(IEmailService), typeof(EmailService));
        }

        private IConfiguration config;
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            var languages = new List<CultureInfo>() { new CultureInfo("en"), new CultureInfo("ru") };
            var options = new RequestLocalizationOptions()
            {
                DefaultRequestCulture = new Microsoft.AspNetCore.Localization.RequestCulture("en"),
                SupportedCultures = languages,
                SupportedUICultures = languages
            };
            options.RequestCultureProviders.RemoveAt(2);
            options.RequestCultureProviders.Insert(0, new CustomRequestCultureProvider(async context =>
            {
                var values = context.Request.Path.ToString().Split('/')[1];
                return new ProviderCultureResult(values);
            }));
            app.UseRequestLocalization(options);
            app.UseIdentity();
            app.UseStatusCodePages();
            loggerFactory.AddConsole();
            app.UseStaticFiles();
            app.UseMvc(routes =>
            {
                routes.MapRoute("default", "{culture}/{controller=Home}/{action=Index}/{id?}");
                routes.MapRoute("withoutCulture", "{controller=Home}/{action=Index}/{id?}");
            });

            //app.Use(async (context, tsk) =>
            //{
            //    await context.Response.WriteAsync($"Hello {config["name"]} {config["lastname"]} ");
            //    await tsk.Invoke();
            //});
            //app.Map("/help", appb => appb.Use(async (tsc, cte) =>
            //{
            //    await tsc.Response.WriteAsync("Help<br>");
            //    await cte.Invoke();
            //}));
            //app.Run(async (context) =>
            //{
            //    await context.Response.WriteAsync("Good buy World!<br>");
            //});
        }
    }
}
