using Microsoft.Extensions.DependencyInjection;
using Services.Services.GoogleApi;
using Services.Services.GoogleApi.AccessTokenUtils;
using Services.Services.GoogleApi.UrlFactories;

namespace Services.Features
{
    public class GoogleApiFeature:ServiceFeature
    {
        public override void Add(IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<PurchaseAcknowledgeUrlFactory>();
            serviceCollection.AddTransient<PurchaseValidateUrlFactory>();
            serviceCollection.AddTransient<AllProductsUrlFactory>();
            
            serviceCollection.AddTransient<GoogleApiProfileStorageService>();
            serviceCollection.AddTransient<PackageNameStorageService>();
            
            serviceCollection.AddTransient<GoogleApiPurchasesWrapperService>();
            serviceCollection.AddTransient<GoogleApiPurchaseAcknowledgeService>();
            serviceCollection.AddTransient<PurchaseRegistrationService>();
            serviceCollection.AddSingleton<CustomGoogleApiAccessTokenService>();
        }
    }
}