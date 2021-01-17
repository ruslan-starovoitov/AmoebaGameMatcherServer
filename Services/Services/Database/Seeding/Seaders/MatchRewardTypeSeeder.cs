﻿using System;
using System.Collections.Generic;
using System.Linq;
using DataLayer;
using DataLayer.Tables;
using Libraries.NetworkLibrary.Experimental;

namespace Services.Services.Database.Seeding.Seaders
{
    public class MatchRewardTypeSeeder
    {
        public void Seed(ApplicationDbContext dbContext)
        {
            if (!dbContext.MatchRewardTypes.Any())
            {
                List<MatchRewardType> matchRewardTypes = new List<MatchRewardType>
                {
                    new MatchRewardType
                    {
                        Id = MatchRewardTypeEnum.RankingReward,
                        Name = MatchRewardTypeEnum.RankingReward.ToString()
                    },
                    new MatchRewardType
                    {
                        Id = MatchRewardTypeEnum.DoubleLootboxPoints,
                        Name = MatchRewardTypeEnum.DoubleLootboxPoints.ToString()
                    }
                };
                dbContext.MatchRewardTypes.AddRange(matchRewardTypes);
                dbContext.SaveChanges();
            }
            
            if (dbContext.MatchRewardTypes.Count() != Enum.GetNames(typeof(MatchRewardTypeEnum)).Length)
            {
                throw new ArgumentOutOfRangeException();
            }
        }
    }
}