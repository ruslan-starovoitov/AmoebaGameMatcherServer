using System.Threading.Tasks;

namespace Services.Services.MatchFinishing
{
    /// <summary>
    /// Отвечает за дописывание результатов матча для батл рояль режима.
    /// </summary>
    public interface IBattleRoyaleMatchFinisherService
    {
        Task FinishMatchAndWriteToDbAsync(int matchId);
        Task<bool> UpdatePlayerMatchResultInDbAsync(int accountId, int placeInBattle, int matchId);
    }
}