using System.Linq;
using DataLayer;
using Services.Experimental;

namespace Services.Services.Database.Seeding.Seaders
{
    public class AccountSeeder
    {
        public void Seed(ApplicationDbContext dbContext)
        {
            if (!dbContext.Accounts.Any())
            {
                var service = new DefaultAccountFactoryService(dbContext);
                service.CreateDefaultAccountAsync("serviceId").Wait();
                
            }

        }
    }
}