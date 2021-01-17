﻿using System;
using System.Linq;
using AmoebaGameMatcherServer.Controllers;
using AmoebaGameMatcherServer.Controllers.ProfileServer.Lobby;
using AmoebaGameMatcherServer.Services;
using Dapper;
using DataLayer;
using DataLayer.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;
using Services.Features;
using Services.Services;
using Services.Services.Database.Seeding;
using Services.Services.GoogleApi.AccessTokenUtils;
using Services.Services.MatchCreationInitiation;
using Services.Services.Queues;

namespace AmoebaGameMatcherServer
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
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
        }
        
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, 
            MatchCreationInitiatorSingletonService matchCreationInitiator, 
            CustomGoogleApiAccessTokenService googleApiAccessTokenManagerService,
            ApplicationDbContext dbContext, NpgsqlConnection npgsqlConnection)
        {
            matchCreationInitiator.StartThread();
            googleApiAccessTokenManagerService.Initialize().Wait();

         
            // //Заполнение данными
            new DataSeeder().Seed(dbContext);
            
            
            app.UseMvc();
        }
    }
}