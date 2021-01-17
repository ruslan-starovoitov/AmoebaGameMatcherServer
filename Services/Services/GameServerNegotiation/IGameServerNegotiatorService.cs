using System.Threading.Tasks;
using NetworkLibrary.NetworkLibrary.Http;

namespace Services.Services.GameServerNegotiation
{
    public interface IGameServerNegotiatorService
    {
        Task SendRoomDataToGameServerAsync(BattleRoyaleMatchModel model);
    }
}