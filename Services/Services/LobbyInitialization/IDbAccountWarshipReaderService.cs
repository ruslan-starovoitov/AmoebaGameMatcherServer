using System.Threading.Tasks;
using DataLayer.Tables;
using JetBrains.Annotations;

namespace Services.Services.LobbyInitialization
{
    /// <summary>
    /// Читает всю информацию про корабли аккаунта.
    /// </summary>
    public interface IDbAccountWarshipReaderService
    {
        [ItemCanBeNull]
        Task<AccountDbDto> ReadAsync(string playerServiceId);
    }
}