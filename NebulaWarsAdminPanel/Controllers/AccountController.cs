using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Services.Services.LobbyInitialization;

namespace NebulaWarsAdminPanel.Controllers
{
    public class AccountController : Controller
    {
        //
        // private IDbWarshipsStatisticsReader reader;
        //
        // public AccountController(IDbWarshipsStatisticsReader reader)
        // {
        //     this.reader = reader;
        //     if (reader==null)
        //     {
        //         throw new Exception("no reader");
        //     }
        // }
        
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