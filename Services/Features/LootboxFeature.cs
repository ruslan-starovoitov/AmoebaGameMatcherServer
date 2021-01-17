using Microsoft.Extensions.DependencyInjection;
using Services.Services.Lootbox;

namespace Services.Features
{
    public class LootboxFeature:ServiceFeature
    {
        public override void Add(IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<LootboxFacadeService>();
            serviceCollection.AddTransient<SmallLootboxDataFactory>();
            serviceCollection.AddTransient<LootboxDbWriterService>();
            serviceCollection.AddTransient<LootboxResourcesFactory>();
            serviceCollection.AddTransient<LootboxResourceTypeFactory>();
        }
    }
}