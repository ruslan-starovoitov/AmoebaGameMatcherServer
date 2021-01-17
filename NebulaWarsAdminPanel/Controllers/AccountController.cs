using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Services.Services.LobbyInitialization;

namespace NebulaWarsAdminPanel.Controllers
{
    public class AccountController : Controller
    {
        private readonly AccountDbReaderService accountReader;
        public AccountController(AccountDbReaderService accountReader)
        {
            this.accountReader = accountReader;
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
            return View(account);
        }
    }
}