using System.Linq;
using DataLayer;

namespace Services.Services.LobbyInitialization
{
    public class AccountMetadataReaderService
    {
        private readonly ApplicationDbContext dbContext;

        public AccountMetadataReaderService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public AccountsViewModel GetAllAccountsMetadata()
        {
            var list = dbContext.Accounts.Select(acc => new AccountShortViewModel
            {
                Username = acc.Username,
                AccountId = acc.Id,
                RegistrationDate = acc.RegistrationDateTime,
                ServiceId = acc.ServiceId
            }).ToList();

            return new AccountsViewModel
            {
                Acccounts = list
            };
        }
    }
}