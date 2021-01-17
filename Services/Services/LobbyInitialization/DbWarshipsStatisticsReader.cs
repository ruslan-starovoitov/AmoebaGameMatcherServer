﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using DataLayer.Tables;
using Npgsql;

namespace Services.Services.LobbyInitialization
{
    public class DbWarshipsStatisticsReader : IDbWarshipsStatisticsReader
    {
        private readonly NpgsqlConnection connection;

        private readonly string sql = @"
--информация про корабли аккаунта
select a.*, w.*, wt.*, wcr.*,
(
    select coalesce(
       (
            select coalesce(sum(I.""Amount""),0)
            from ""Increments"" I
                 inner join ""IncrementTypes"" IT on I.""IncrementTypeId"" = IT.""Id""
            where I.""WarshipId"" = w.""Id"" and IT.""Name""='WarshipRating'
       )
   -
       (
            select coalesce(sum(D.""Amount""),0)
            from ""Decrements"" D
                inner join ""DecrementTypes"" DT on D.""DecrementTypeId"" = DT.""Id""
            where D.""WarshipId"" = w.""Id"" and DT.""Name""='WarshipRating'
       )
        ,0
    ) as ""WarshipRating""
),
(
    select coalesce(
          (
              select sum(I.""Amount"")
              from ""Increments"" I
                       inner join ""IncrementTypes"" IT on I.""IncrementTypeId"" = IT.""Id""
              where I.""WarshipId"" = w.""Id"" and IT.""Name""='WarshipPowerPoints'
          )
        
          -
              (
                  select coalesce(sum(D.""Amount""),0)
                  from ""Decrements"" D
                           inner join ""DecrementTypes"" DT on D.""DecrementTypeId"" = DT.""Id""
                  where D.""WarshipId"" = w.""Id"" and DT.""Name""='WarshipPowerPoints'
              )
          ,0
    ) as ""WarshipPowerPoints"" 
),
(
    select coalesce(
          (
              select max(I.""Amount"")
              from ""Increments"" I
                       inner join ""IncrementTypes"" IT on I.""IncrementTypeId"" = IT.""Id""
              where I.""WarshipId"" = w.""Id"" and IT.""Name""='WarshipLevel'
          )
          ,0
    ) as ""WarshipLevel"" 
)

from ""Accounts"" A
         join ""Warships"" w on a.""Id"" = w.""AccountId""
         join ""WarshipTypes"" wt on w.""WarshipTypeId"" = wt.""Id""
         join ""WarshipCombatRoles"" wcr on wt.""WarshipCombatRoleId"" = wcr.""Id""
where a.""ServiceId"" = @serviceIdPar
group by a.""Id"", w.""Id"", wt.""Id"", wcr.""Id"";
            ";

        public DbWarshipsStatisticsReader(NpgsqlConnection connection)
        {
            this.connection = connection;
        }
        
        public async Task<AccountDbDto> ReadAsync(string serviceId)
        {
            var parameters = new {serviceIdPar = serviceId};
            //accountId + account
            Dictionary<int, AccountDbDto> lookup = new Dictionary<int, AccountDbDto>();
            await connection
                .QueryAsync<AccountDbDto, WarshipDbDto,WarshipType, WarshipCombatRole, WarshipStatistics, AccountDbDto>(sql,
                    (accountDbDto, warshipDbDto, warshipTypeArg, warshipCombatRole, warshipStatistics) =>
                    {
                        //Если такого аккаунта ещё не было
                        if (!lookup.TryGetValue(accountDbDto.Id, out AccountDbDto account))
                        {
                            //Положить аккаунт в словарь
                            lookup.Add(accountDbDto.Id, account = accountDbDto);
                        }

                        //Попытаться достать корабль c таким id из коллекции
                        WarshipDbDto warship = account.Warships
                            .Find(wArg => wArg.Id == warshipDbDto.Id);
                        //Этот корабль уже есть в коллекции?
                        if (warship == null)
                        {
                            warship = warshipDbDto;
                            warship.WarshipType = warshipTypeArg;
                            warship.WarshipRating = warshipStatistics.WarshipRating;
                            warship.WarshipPowerPoints = warshipStatistics.WarshipPowerPoints;
                            warship.WarshipType.WarshipCombatRole = warshipCombatRole;
                            warship.Id = warshipDbDto.Id;
                            warship.WarshipPowerLevel = warshipStatistics.WarshipLevel;
                            warship.WarshipTypeId = warshipDbDto.WarshipTypeId;
                            
                            account.Warships.Add(warship);
                            account.Rating += warship.WarshipRating;
                        }

                        Console.WriteLine(" " + accountDbDto);
                        Console.WriteLine("\t\t " + warshipDbDto);
                        Console.WriteLine("\t\t\t " + warshipStatistics);
                        return account;
                    }, parameters, splitOn:"Id,WarshipRating");
            
            switch (lookup.Count)
            {
                case 0:
                    return null;
                case 1 :
                    return lookup.Single().Value;
                default:
                    throw new Exception($"По serviceId = {serviceId} найдено {lookup.Count} аккаунтов.");
            }
        }
    }
}