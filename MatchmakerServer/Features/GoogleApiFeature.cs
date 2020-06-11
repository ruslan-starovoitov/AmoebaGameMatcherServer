﻿using AmoebaGameMatcherServer.Services.GoogleApi;
using Microsoft.Extensions.DependencyInjection;

namespace AmoebaGameMatcherServer
{
    public class GoogleApiFeature:ServiceFeature
    {
        public override void Add(IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<PurchasesValidatorService>();
            serviceCollection.AddSingleton<CustomGoogleApiAccessTokenService>();
            serviceCollection.AddSingleton<IpAppProductsService>();
        }
    }
}