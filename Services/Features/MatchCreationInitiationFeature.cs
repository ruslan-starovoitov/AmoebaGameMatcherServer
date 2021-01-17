using Microsoft.Extensions.DependencyInjection;
using Services.Services.MatchCreationInitiation;

namespace Services.Features
{
    public class MatchCreationInitiationFeature:ServiceFeature
    {
        public override void Add(IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<MatchCreationInitiatorSingletonService>();
            serviceCollection.AddTransient<IPlayerTimeoutManager, PlayerTimeoutManagerService>();
        }
    }
}