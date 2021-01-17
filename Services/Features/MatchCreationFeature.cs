using Microsoft.Extensions.DependencyInjection;
using Services.Services.MatchCreation;

namespace Services.Features
{
    public class MatchCreationFeature:ServiceFeature
    {
        public override void Add(IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<BattleRoyaleMatchCreatorService>();
            serviceCollection.AddTransient<BattleRoyaleBotFactoryService>();
            serviceCollection.AddTransient<MatchDbWriterService>();
        }
    }
}