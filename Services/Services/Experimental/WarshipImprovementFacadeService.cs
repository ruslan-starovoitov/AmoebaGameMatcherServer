﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataLayer;
using DataLayer.Entities.Transactions.Decrement;
using DataLayer.Tables;
using JetBrains.Annotations;
using NetworkLibrary.NetworkLibrary.Http;
using Services.Services.LobbyInitialization;

namespace Services.Services.Experimental
{
    public class WarshipImprovementFacadeService
    {
        private readonly ApplicationDbContext dbContext;
        private readonly AccountDbReaderService accountDbReaderService;
        private readonly WarshipImprovementCostChecker warshipImprovementCostChecker;
        
        public WarshipImprovementFacadeService(AccountDbReaderService accountDbReaderService,
            ApplicationDbContext dbContext, WarshipImprovementCostChecker warshipImprovementCostChecker)
        {
            this.dbContext = dbContext;
            this.warshipImprovementCostChecker = warshipImprovementCostChecker;
            this.accountDbReaderService = accountDbReaderService;
        }

        public async Task<bool> TryBuyLevel([NotNull] string serviceId, int warshipId)
        {
            //Аккаунт существует?
            AccountDbDto accountDbDto = await accountDbReaderService.ReadAccountAsync(serviceId);
            if (accountDbDto == null)
            {
                throw new Exception("Такого аккаунта не существует");
            }
            
            //Корабль существует?
            WarshipDbDto warshipDbDto = accountDbDto.Warships.SingleOrDefault(dto => dto.Id == warshipId);
            if (warshipDbDto == null)
            {
                throw new Exception("Этому аккаунту не принаждлежит этот корабль");
            }
            
            bool canAPurchaseBeMade = warshipImprovementCostChecker
                .CanAPurchaseBeMade(accountDbDto.SoftCurrency, warshipDbDto.WarshipPowerLevel, warshipDbDto.WarshipPowerPoints, out var faultReason );
            if (!canAPurchaseBeMade)
            {
                throw new Exception("Невозможно осуществить покупку улучшения для корабля по причине "+faultReason);
            }

            WarshipImprovementModel improvementModel = warshipImprovementCostChecker.GetImprovementModel(warshipDbDto.WarshipPowerLevel);

            Console.WriteLine("текущий  wpp "+warshipDbDto.WarshipPowerLevel);
            //Записать транзакцию
            Transaction transaction = new Transaction
            {
                AccountId = accountDbDto.Id,
                DateTime = DateTime.UtcNow,
                TransactionTypeId = TransactionTypeEnum.WarshipImprovement,
                WasShown = false,
                Increments = new List<Increment>
                {
                    new Increment
                    {
                        IncrementTypeId = IncrementTypeEnum.WarshipLevel,
                        Amount = warshipDbDto.WarshipPowerLevel+1,
                        WarshipId = warshipId
                    }
                },
                Decrements = new List<Decrement>
                {
                    new Decrement
                    {
                        DecrementTypeId = DecrementTypeEnum.SoftCurrency,
                        Amount = improvementModel.SoftCurrencyCost
                    },
                    new Decrement
                    {
                        DecrementTypeId = DecrementTypeEnum.WarshipPowerPoints,
                        Amount = improvementModel.PowerPointsCost,
                        WarshipId = warshipDbDto.Id
                    }
                }
            };

            await dbContext.Transactions.AddAsync(transaction);
            await dbContext.SaveChangesAsync();
            
            return true;
        }
    }
}