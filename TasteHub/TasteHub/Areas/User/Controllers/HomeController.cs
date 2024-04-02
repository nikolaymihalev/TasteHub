﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TasteHub.Core.Contracts;
using TasteHub.Core.Models.Admin;
using TasteHub.Core.Models.User;

namespace TasteHub.Areas.User.Controllers
{
    public class HomeController : BaseController
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly IAdminService adminService;

        public HomeController(
            UserManager<IdentityUser> _userManager,
            SignInManager<IdentityUser> _signInManager,
            IAdminService _adminService)
        {
            userManager = _userManager;
            signInManager = _signInManager;
            adminService = _adminService;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register()
        {
            if (User?.Identity?.IsAuthenticated ?? false)
            {
                return RedirectToAction("Index", "Home", new { area = "" });
            }

            var model = new RegisterViewModel();

            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = new IdentityUser()
            {
                Email = model.Email,
                UserName = model.UserName
            };

            var result = await userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                return RedirectToAction("Login", "Home");
            }

            foreach (var item in result.Errors)
            {
                ModelState.AddModelError("", item.Description);
            }

            return View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            if (User?.Identity?.IsAuthenticated ?? false)
            {
                return RedirectToAction("Index", "Home", new { area = "" });
            }

            var model = new LoginViewModel();

            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await userManager.FindByEmailAsync(model.Email);

            if (user != null)
            {
                var result = await signInManager.PasswordSignInAsync(user, model.Password, false, false);

                if (result.Succeeded)
                {
                    if (await userManager.IsInRoleAsync(user, "Admin")) 
                    {
                        return RedirectToAction("Admin", "Home", new { area = "" });
                    }
                    return RedirectToAction("Index", "Home", new { area = "" });
                }
            }

            ModelState.AddModelError("", "Invalid login");

            return View(model);
        }
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();

            return RedirectToAction("Index", "Home", new { area = ""});
        }

        [HttpGet]
        public IActionResult BecomeAdmin()
        {
            var model = new QueryFormModel()
            {
                UserId = User.Id()
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> BecomeAdmin(QueryFormModel model)
        {
            model.UserId = User.Id();

            if (ModelState.IsValid == false)
            {
                return BadRequest();
            }

            if (await adminService.UserExistsAsync(model.UserId))
            {
                return BadRequest();
            }

            var user = await userManager.FindByIdAsync(model.UserId);

            if (await userManager.IsInRoleAsync(user, "Admin")) 
            {
                return BadRequest();
            }

            await adminService.AddAsync(model);

            return RedirectToAction("AllRecipes", "Recipe");
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult AboutUs()
        {
            return View();
        }
    }
}
