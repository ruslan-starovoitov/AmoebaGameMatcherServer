﻿using DataLayer;
using Services.Services.Database.Seeding.Seaders;

namespace Services.Services.Database.Seeding
{
    public class DataSeeder
    {
        public void Seed(ApplicationDbContext dbContext)
        {
            
            new WarshipImprovementTrigger().Seed(dbContext);
            #warning Этот класс должен вызываться раньше , чем GameModeSeeder
            
            new GameModeSeeder().Seed(dbContext);
            new WarshipCombatRoleSeeder().Seed(dbContext);
            new WarshipTypesSeeder().Seed(dbContext);
            new TransactionTypesSeeder().Seed(dbContext);
            new IncrementTypeSeeder().Seed(dbContext);
            new MatchRewardTypeSeeder().Seed(dbContext);
            new DecrementTypeSeeder().Seed(dbContext);
            new SkinTypesSeeder().Seed(dbContext);
            new AccountSeeder().Seed(dbContext);
        }
    }
}