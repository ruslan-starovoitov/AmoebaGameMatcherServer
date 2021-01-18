using System.Collections.Generic;
using System.Threading.Tasks;
using DataLayer.Tables;
using Microsoft.AspNetCore.Mvc;
using Services.Services.LobbyInitialization;

namespace NebulaWarsAdminPanel.Controllers
{
    public class AccountController : Controller
    {
        private readonly AccountDbReaderService accountReader;
        private readonly AccountTransactionsReaderService transactionsReaderService;

        public AccountController(AccountDbReaderService accountReader, 
            AccountTransactionsReaderService transactionsReaderService)
        {
            this.accountReader = accountReader;
            this.transactionsReaderService = transactionsReaderService;
        }

        [HttpGet]
        public async Task<IActionResult> Index([FromQuery] string playerServiceId)
        {
            if (playerServiceId == null)
            {
                ViewData["ErrorMessage"] = "Не указан id";
                return View();
            }

            var account = await accountReader.ReadAccountAsync(playerServiceId);
            var transactions = transactionsReaderService.GetAllTransactions(playerServiceId);

            var viewModel = new AccountViewModel
            {
                Transactions = transactions,
                AccountDbDto = account
            };
            return View(viewModel);
        }
    }

    public class AccountViewModel
    {
        public AccountDbDto AccountDbDto { get; set; }
        public List<Transaction> Transactions { get; set; }
    }
}