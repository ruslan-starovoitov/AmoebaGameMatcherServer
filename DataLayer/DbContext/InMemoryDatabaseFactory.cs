﻿using System;
using Microsoft.EntityFrameworkCore;

namespace DataLayer
{
    public class InMemoryDatabaseFactory:IDbContextFactory
    {
        private readonly string databaseName;
        private readonly string guidString;

        public InMemoryDatabaseFactory(string databaseName)
        {
            this.databaseName = databaseName;
            guidString = Guid.NewGuid().ToString();
        }
        
        public ApplicationDbContext Create()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(guidString+" "+databaseName)
                .Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            return dbContext;
        }
    }
}