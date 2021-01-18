using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NebulaWarsAdminPanel.Services;
using Services.Features;
using Services.Services;
using Services.Services.Queues;

namespace GameAdminPanel
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_3_0);
            services.AddControllersWithViews();
            services.AddRazorPages();
            services.AddTransient<IPassCheckerService, PasswordCheckerService>();
            
            
            
            services.AddFeature(new GoogleApiFeature());
            services.AddFeature(new DatabaseFeature());
            services.AddFeature(new LobbyInitializeFeature());
            services.AddFeature(new PlayerQueueingFeature());
            services.AddFeature(new MatchCreationInitiationFeature());
            services.AddFeature(new MatchCreationFeature());
            services.AddFeature(new MatchFinishingFeature());
            services.AddFeature(new GameServerNegotiationFeature());
            services.AddFeature(new LootboxFeature());
            services.AddFeature(new PurchasingFeature());
            services.AddFeature(new WarshipUpgradeFeature());
            services.AddFeature(new ShopFeature());
            
            services.AddTransient<CurrentSkinChangingService>();
            
            
            services.AddTransient<BundleVersionService>();
            
            //Общие очереди игроков
            services.AddSingleton<IBattleRoyaleQueueSingletonService, BattleRoyaleQueueSingletonService>();
            services.AddSingleton<IBattleRoyaleUnfinishedMatchesSingletonService, BattleRoyaleUnfinishedMatchesSingletonService>();
            //Общие очереди игроков
            
            
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie();
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
                    "default",
                    "{controller=AdminPanel}/{action=Index}");
            });
        }
    }
}
