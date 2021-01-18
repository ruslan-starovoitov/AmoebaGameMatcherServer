using System;
using System.Collections.Generic;
using System.Diagnostics;
using GameAdminPanel.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Services.LobbyInitialization;

namespace GameAdminPanel.Controllers
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
            // var accountsViewModel = new AccountsViewModel()
            // {
            //     Acccounts = new List<AccountShortViewModel>()
            //     {
            //         new AccountShortViewModel()
            //         {
            //             Username = "sdads",
            //             AccountId = 134,
            //             RegistrationDate = DateTime.Now,
            //             ServiceId = "asojbvpoasbvapou"
            //         }
            //     }
            // };
            return View(accountsViewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}