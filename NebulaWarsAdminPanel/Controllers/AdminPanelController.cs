using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NebulaWarsAdminPanel.Models;

namespace NebulaWarsAdminPanel.Controllers
{
    public class AdminPanelController:Controller
    {
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
            var accountsViewModel = new AccountsViewModel()
            {
                Acccounts = new List<AccountShortViewModel>
                {
                    new AccountShortViewModel
                    {
                        Username = "opa",
                        RegistrationDate = new DateTime(2000, 07, 19, 0, 0, 0)
                    }
                }
            };
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