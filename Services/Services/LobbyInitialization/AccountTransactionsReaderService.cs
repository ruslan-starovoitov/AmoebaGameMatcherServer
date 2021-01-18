using System.Collections.Generic;
using System.Linq;
using DataLayer;
using DataLayer.Tables;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;

namespace Services.Services.LobbyInitialization
{
    public class AccountTransactionsReaderService
    {
        private readonly ApplicationDbContext dbContext;

        public AccountTransactionsReaderService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public List<Transaction> GetAllTransactions([NotNull] string playerServiceId)
        {
            var transactions = dbContext.Transactions
                .Include(trans=>trans.TransactionType)
                .Include(trans => trans.Decrements)
                    .ThenInclude(decr => decr.DecrementType)
                .Include(trans => trans.Increments)
                    .ThenInclude(incr => incr.IncrementType)
                .Include(trans => trans.Increments)
                    .ThenInclude(incr => incr.SkinType)
                .Include(trans => trans.Account)
                .Where(trans => trans.Account.ServiceId == playerServiceId)
                .ToList();

            return transactions;
        }
    }
}