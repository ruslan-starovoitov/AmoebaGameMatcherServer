﻿using System.Threading.Tasks;
using DataLayer.Tables;
using JetBrains.Annotations;

namespace Services.Services.LobbyInitialization
{
    /// <summary>
    /// Достаёт из БД данные про корабли аккаунта.
    /// </summary>
    public interface IDbWarshipsStatisticsReader
    {
        Task<AccountDbDto> ReadAsync([NotNull] string serviceId);
    }
}