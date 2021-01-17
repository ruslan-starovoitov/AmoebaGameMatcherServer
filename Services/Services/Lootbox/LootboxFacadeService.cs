﻿using System;
using System.Threading.Tasks;
using DataLayer.Tables;
using JetBrains.Annotations;
using NetworkLibrary.NetworkLibrary.Http;
using Services.Services.LobbyInitialization;

namespace Services.Services.Lootbox
{
    /// <summary>
    /// Управляет открытием лутбокса.
    /// </summary>
    public class LootboxFacadeService
    {
        private readonly LootboxDbWriterService lootboxDbWriterService;
        private readonly AccountDbReaderService accountDbReaderService;
        private readonly SmallLootboxDataFactory smallLootboxModelFactory;
        
        public LootboxFacadeService(
            SmallLootboxDataFactory smallLootboxModelFactory,
            LootboxDbWriterService lootboxDbWriterService,
             AccountDbReaderService accountDbReaderService)
        {
        
            this.smallLootboxModelFactory = smallLootboxModelFactory;
            this.lootboxDbWriterService = lootboxDbWriterService;
            this.accountDbReaderService = accountDbReaderService;
        }

        [ItemCanBeNull]
        public async Task<LootboxModel> CreateLootboxModelAsync([NotNull] string playerServiceId)
        {
            //Достать аккаунт    
            AccountDbDto accountDbDto = await accountDbReaderService.ReadAccountAsync(playerServiceId);

            if (accountDbDto == null)
            {
                Console.WriteLine("попытка купить лутбокс для аккаунта, которого не существует.");
                return null;
            }
            
            //Ресурсов для покупки хватает?
            if (accountDbDto.LootboxPoints < 100)
            {
                Console.WriteLine("Не хватает ресурсов для покупки лутбокса");
                return null;
            }

            //Создать лутбокс
            LootboxModel lootboxModel = smallLootboxModelFactory.Create(accountDbDto.Warships);
            
            //Сохранить лутбокс 
            await lootboxDbWriterService.WriteAsync(playerServiceId, lootboxModel);
            
            return lootboxModel;
        }
    }
}