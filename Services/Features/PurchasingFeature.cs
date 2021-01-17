using Microsoft.Extensions.DependencyInjection;
using Services.Services.GoogleApi;

namespace Services.Features
{
    public class PurchasingFeature:ServiceFeature
    {
        public override void Add(IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<RealPurchaseTransactionFactoryService>();
            serviceCollection.AddTransient<HardCurrencyTransactionFactory>();
            serviceCollection.AddTransient<PurchasesValidatorService>();
        }
    }
}