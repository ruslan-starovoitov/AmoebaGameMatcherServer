using Microsoft.AspNetCore.Mvc;
using NetworkLibrary.NetworkLibrary.Http;

namespace NebulaWarsAdminPanel.Controllers
{
    public class AccountController : Controller
    {
        [HttpGet]
        public IActionResult Index([FromQuery] int? accountId)
        {
            if (accountId == null)
            {
                ViewData["ErrorMessage"] = "Не указан id";
                return View();
            }
            
            AccountDto accountDto = new AccountDto
            {
                Username = "igor",
                AccountId = accountId.Value
            };
            return View(accountDto);
        }
    }
}