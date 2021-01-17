using System;
using Services.Experimental;
using Services.Services.Queues;

namespace Services.Services.MatchCreationInitiation
{
     
    public class PlayerTimeoutManagerService:IPlayerTimeoutManager
    {
        private readonly IBattleRoyaleQueueSingletonService battleRoyaleQueueService;

        public PlayerTimeoutManagerService(IBattleRoyaleQueueSingletonService battleRoyaleQueueService)
        {
            this.battleRoyaleQueueService = battleRoyaleQueueService;
        }

        /// <summary>
        /// Есть ли игрок, который ждёт слишком долго?
        /// </summary>
        /// <returns></returns>
        public bool IsWaitingTimeExceeded()
        {
            
            DateTime? oldestRequestTime = battleRoyaleQueueService.GetOldestRequestTime();
            if (oldestRequestTime == null)
            {
                return false;
            }
            var deltaTime = DateTime.UtcNow - oldestRequestTime.Value;
            return deltaTime.TotalSeconds > Globals.MaxStandbyTimeSec;
        }
    }
}