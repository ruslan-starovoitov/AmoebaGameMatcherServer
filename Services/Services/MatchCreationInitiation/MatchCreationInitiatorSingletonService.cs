using System;
using Services.Experimental;
using Services.Services.MatchCreation;

namespace Services.Services.MatchCreationInitiation
{
    public class MatchCreationInitiatorSingletonService : MatchCreationInitiator
    {
        private readonly PeriodicTaskExecutor periodicTaskExecutor;

        public MatchCreationInitiatorSingletonService(BattleRoyaleMatchCreatorService battleRoyaleMatchCreatorService,
            IPlayerTimeoutManager playerTimeoutManager) 
            : base(battleRoyaleMatchCreatorService, playerTimeoutManager)
        {
            TimeSpan delay = TimeSpan.FromSeconds(1);
            periodicTaskExecutor = new PeriodicTaskExecutor(TryCreateBattleRoyaleMatch, delay);
        }
        
        public void StartThread()
        {
            periodicTaskExecutor.StartThread();
        }
    }
}