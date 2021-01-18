using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NebulaWarsAdminPanel.Services;

namespace GameAdminPanel.Controllers
{
    public class AuthController:Controller
    {
        private readonly IPassCheckerService passCheckerService;

        public AuthController(IPassCheckerService passCheckerService)
        {
            this.passCheckerService = passCheckerService;
        }
        
        [HttpGet, AllowAnonymous]
        public IActionResult LogIn()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Home", "AdminPanel");
            }

            return View();
        }

        [HttpPost, AllowAnonymous, ValidateAntiForgeryToken]
        public async Task<ActionResult> LogIn(string password)
        {
            if (!passCheckerService.IsPasswordCorrect(password))
            {
                Console.WriteLine("\n\n\nWrong pass\n\n\n");
                ViewData["ErrorMessage"] = "Неправильний пароль.";
                return View();
            }

            await AddAuthCookieAsync();
            return RedirectToAction("Home", "AdminPanel");
        }

        
        [HttpGet, Authorize]
        public async Task<IActionResult> LogOut()
        {
            await RemoveAuthCookieAsync();
            return RedirectToAction("Index","AdminPanel");
        }

        private async Task AddAuthCookieAsync()
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultRoleClaimType, "Admin")
            };
            var id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, 
                ClaimsIdentity.DefaultRoleClaimType);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }

        private async Task RemoveAuthCookieAsync()
        {
            await HttpContext.SignOutAsync();
        }
    }
}