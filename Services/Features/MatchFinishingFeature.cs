using Microsoft.Extensions.DependencyInjection;
using Services.Services.MatchFinishing;

namespace Services.Features
{
    public class MatchFinishingFeature:ServiceFeature
    {
        public override void Add(IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<IBattleRoyaleMatchFinisherService, BattleRoyaleMatchFinisherService>();
            serviceCollection.AddTransient<IBattleRoyaleMatchRewardCalculatorService, BattleRoyaleMatchRewardCalculatorService>();
            serviceCollection.AddTransient<IPlayerMatchResultDbReaderService, PlayerMatchResultDbReaderService>();
            serviceCollection.AddTransient<IWarshipRatingReaderService, WarshipRatingReaderService>();
        }
    }
}