using System.Diagnostics;
using AdminPanel.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Services.LobbyInitialization;

namespace AdminPanel.Controllers
{
    public class AdminPanelController:Controller
    {
        private readonly AccountMetadataReaderService metadataReaderService;

        public AdminPanelController(AccountMetadataReaderService metadataReaderService)
        {
            this.metadataReaderService = metadataReaderService;
        }

        public IActionResult Index()
        {
            if(User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Home");
            }
            return RedirectToAction("LogIn", "Auth");
        }
        
        [HttpGet, Authorize]
        public IActionResult Home()
        {
            var accountsViewModel = metadataReaderService.GetAllAccountsMetadata();
            return View(accountsViewModel);
        }
        
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}