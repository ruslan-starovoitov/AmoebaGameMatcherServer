using Microsoft.Extensions.DependencyInjection;
using Services.Services.GameServerNegotiation;

namespace Services.Features
{
    public class GameServerNegotiationFeature:ServiceFeature
    {
        public override void Add(IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<IGameServerNegotiatorService, GameServerNegotiatorService>();
        }
    }
}