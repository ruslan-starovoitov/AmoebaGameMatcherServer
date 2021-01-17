using Microsoft.Extensions.DependencyInjection;
using Services.Services.PlayerQueueing;

namespace Services.Features
{
    public class PlayerQueueingFeature:ServiceFeature
    {
        public override void Add(IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<IMatchmakerFacadeService, MatchmakerFacadeService>();
            serviceCollection.AddTransient<IQueueExtenderService, QueueExtenderService>();
            serviceCollection.AddTransient<MatchRoutingDataService>();
            serviceCollection.AddTransient<GameServersRoutingDataService>();
        }
    }
}