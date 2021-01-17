using Services.Experimental;

namespace Services.Services.PlayerQueueing
{
    public class GameServersRoutingDataService
    {
        public MatchRoutingData GetGameServerAddress()
        {
            MatchRoutingData result = 
                new MatchRoutingData(Globals.DefaultGameServerIp, Globals.DefaultGameServerUdpPort);
                
            return result;
        }
    }
}