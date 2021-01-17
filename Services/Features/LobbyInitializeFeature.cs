using Microsoft.Extensions.DependencyInjection;
using Services.Experimental;
using Services.Services.Experimental;
using Services.Services.LobbyInitialization;

namespace Services.Features
{
    public class LobbyInitializeFeature:ServiceFeature
    {
        public override void Add(IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<LobbyModelFacadeService>();
            serviceCollection.AddTransient<WarshipRatingScale>();
            serviceCollection.AddTransient<DefaultAccountFactoryService>();
            serviceCollection.AddTransient<AccountFacadeService>();
            serviceCollection.AddTransient<AccountDbReaderService>();
            serviceCollection.AddTransient<AccountRegistrationService>();
            serviceCollection.AddTransient<NotShownRewardsReaderService>();
            serviceCollection.AddTransient<IDbWarshipsStatisticsReader, DbWarshipsStatisticsReader>();
            serviceCollection.AddTransient<WarshipsCharacteristicsService>();
            serviceCollection.AddTransient<AccountMapperService>();
            serviceCollection.AddTransient<WarshipImprovementCostChecker>();
            serviceCollection.AddTransient<ISkinsDbReaderService, SkinsDbReaderService>();
            serviceCollection.AddTransient<IDbAccountWarshipReaderService, DbAccountWarshipReaderService>();
            serviceCollection.AddTransient<UsernameChangingService>();
            serviceCollection.AddTransient<UsernameValidatorService>();
            serviceCollection.AddTransient<AccountResourcesDbReader>();
        }
    }
}