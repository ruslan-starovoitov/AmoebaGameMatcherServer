using Microsoft.Extensions.DependencyInjection;
using Services.Services.Shop.Sales;
using Services.Services.Shop.Sales.TransactionCreation;
using Services.Services.Shop.Sales.TransactionCreation.DecrementCreation;
using Services.Services.Shop.Sales.TransactionCreation.IncrementCreation;
using Services.Services.Shop.ShopModel;
using Services.Services.Shop.ShopModel.DeleteMeShopSectionFactories;
using Services.Services.Shop.ShopModel.ShopModelCreation;
using Services.Services.Shop.ShopModel.ShopModelDbReading;
using Services.Services.Shop.ShopModel.ShopModelDbWriting;

namespace Services.Features
{
    public class ShopFeature : ServiceFeature
    {
        public override void Add(IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<ShopService>();
            serviceCollection.AddTransient<DailyDealsSectionFactory>();
            // serviceCollection.AddTransient<PrizeFactoryService>();
            serviceCollection.AddTransient<WarshipPowerPointsProductsFactoryService>();
            serviceCollection.AddTransient<SellerService>();
            serviceCollection.AddTransient<ShopTransactionFactory>();
            serviceCollection.AddTransient<ShopModelDbReader>();
            serviceCollection.AddTransient<ShopFactoryService>();
            serviceCollection.AddTransient<ShopWriterService>();
            serviceCollection.AddTransient<IncrementFactoryService>();
            serviceCollection.AddTransient<DecrementFactoryService>();
            serviceCollection.AddTransient<HardCurrencySectionFactory>();
            serviceCollection.AddTransient<SoftCurrencySectionFactory>();
        }
    }
}