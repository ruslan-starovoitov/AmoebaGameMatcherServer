using Microsoft.Extensions.DependencyInjection;
using Services.Services.Experimental;

namespace Services.Features
{
    public class WarshipUpgradeFeature:ServiceFeature
    {
        public override void Add(IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<WarshipImprovementFacadeService, WarshipImprovementFacadeService>();
        }
    }
}