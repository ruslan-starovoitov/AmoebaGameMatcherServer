﻿using System.Collections.Generic;
using DataLayer.Tables;
using NetworkLibrary.NetworkLibrary.Http;

namespace Services.Services.Lootbox
{
    /// <summary>
    /// Случайно создаёт маленький лутбокс с NumberOfPrizes призами.
    /// </summary>
    public class SmallLootboxDataFactory
    {
        private const int NumberOfPrizes = 3;
        private readonly LootboxResourcesFactory lootboxResourcesFactory;

        public SmallLootboxDataFactory(LootboxResourcesFactory lootboxResourcesFactory)
        {
            this.lootboxResourcesFactory = lootboxResourcesFactory;
        }
        
        public LootboxModel Create(List<WarshipDbDto> warships)
        {
            LootboxModel result = new LootboxModel
            {
                Prizes = new List<ResourceModel>(NumberOfPrizes)
            };
            for (int i = 0; i < NumberOfPrizes; i++)
            {
                ResourceModel prize = lootboxResourcesFactory.Create(warships);
                if (prize != null)
                {
                    result.Prizes.Add(prize);
                }
            }

            return result;
        }
    }
}