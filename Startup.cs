using BotDetect.Web;
using CopegeMVC.Database;
using CopegeMVC.Configurations;
using CopegeMVC.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using System;

namespace CopegeMVC
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IHostingEnvironment hostingEnvironment)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(hostingEnvironment.ContentRootPath)
                .AddJsonFile("appsettings.json", true, true)
                .AddJsonFile($"appsettings.{hostingEnvironment.EnvironmentName}.json", true, true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            #region (4) Classe Configurations.DependencyInjectionConfig
            services.ResolveDependencies();
            #endregion
            #region (3) Classe Configurations.SmtpConfig
            services.AddSmtpConfiguration(Configuration);
            #endregion

            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => false; // Setar propriedade para false o padrão é true
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
            
            services.AddMemoryCache();
            services.AddSession();
            #region (1) Classe Configurations.MvcConfig
            services.AddMvcConfiguration();
            #endregion
            // Add Session services.
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(20);
                options.Cookie.IsEssential = true;
            });
            #region Adicionando o DbContext (Mysql)
            /******************************************************************************************************
             * AddDbContext utilizando arquivo appsettings (Produção/Desenvolvimento/Homologação) para resgatar a 
             * connectionstring apontando para o banco de dados mysql.
             ******************************************************************************************************/
            services.AddDbContext<CopegeMVCContext>(options => options.UseMySql(Configuration.GetConnectionString("MeuDbContext"),
                mySqlOptions =>
                {
                    mySqlOptions.ServerVersion(new Version(5, 5, 34), ServerType.MySql);
                }));
            #endregion
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseStatusCodePagesWithReExecute("/Error/{0}");
                app.UseExceptionHandler("/Error/Error500");
                app.UseHsts();
            }

            app.UseDefaultFiles();
            app.UseCookiePolicy();
            app.UseSession();
            app.UseCaptcha(Configuration);
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            #region (2) Classe Configurations.GlobalizationConfig
            app.UseGlobalizationConfiguration();
            #endregion

            #region (1) Classe Configurations.MvcConfig
            app.UseMvc(routes =>
            {
                routes.MapRoute(name: "areas", template: "{area:exists}/{controller=Home}/{action=Index}/{id:int?}");
                routes.MapRoute(name: "default", template: "{controller=Home}/{action=Index}/{id:int?}");
            });
            #endregion

        }
    }
}
