using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NebulaWarsAdminPanel.Services;
using Services.Features;

namespace NebulaWarsAdminPanel
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });


            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_3_0);
            services.AddTransient<IPassCheckerService, PasswordCheckerService>();
            
            
            // services.AddFeature(new GoogleApiFeature());
            services.AddFeature(new DatabaseFeature());
            services.AddFeature(new LobbyInitializeFeature());
            services.AddFeature(new PlayerQueueingFeature());
            // services.AddFeature(new MatchCreationInitiationFeature());
            services.AddFeature(new MatchCreationFeature());
            services.AddFeature(new MatchFinishingFeature());
            services.AddFeature(new GameServerNegotiationFeature());
            services.AddFeature(new LootboxFeature());
            services.AddFeature(new PurchasingFeature());
            services.AddFeature(new WarshipUpgradeFeature());
            services.AddFeature(new ShopFeature());

            
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options => 
                {
                    options.LoginPath = new PathString("/Auth/LogIn");
                });
            
        }
        
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/AdminPanel/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            
            app.UseRouting();
            
            app.UseAuthentication(); 
            
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });
        }
    }
}