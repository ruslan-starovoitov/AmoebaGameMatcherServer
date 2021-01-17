using System.Linq;
using System.Threading.Tasks;
using DataLayer;
using DataLayer.Tables;
using JetBrains.Annotations;

namespace Services.Services.LobbyInitialization
{
    /// <summary>
    /// Во время загрузки данных в лобби достаёт аккаунт из БД.
    /// Если такого аккаунта нет, то вернёт null.
    /// </summary>
    public class AccountDbReaderService
    {
        private readonly AccountResourcesDbReader accountResourcesDbReader;
        private readonly IDbAccountWarshipReaderService dbAccountWarshipReaderService;

        public AccountDbReaderService(IDbAccountWarshipReaderService dbAccountWarshipReaderService,
            AccountResourcesDbReader accountResourcesDbReader)
        {
            this.dbAccountWarshipReaderService = dbAccountWarshipReaderService;
            this.accountResourcesDbReader = accountResourcesDbReader;
        }


        [ItemCanBeNull]
        public async Task<AccountDbDto> ReadAccountAsync([NotNull] string playerServiceId)
        {
            AccountDbDto accountDbDto = await dbAccountWarshipReaderService.ReadAsync(playerServiceId);
            if (accountDbDto == null)
            {
                return null;
            }
            
            AccountResources accountResources = await accountResourcesDbReader.ReadAsync(playerServiceId);
            accountDbDto.HardCurrency = accountResources.HardCurrency;
            accountDbDto.SoftCurrency = accountResources.SoftCurrency;
            accountDbDto.LootboxPoints = accountResources.LootboxPoints;
            return accountDbDto;
        }
    }

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
                RegistrationDate = acc.RegistrationDateTime
            }).ToList();

            return new AccountsViewModel()
            {
                Acccounts = list
            };
        }
    }
}