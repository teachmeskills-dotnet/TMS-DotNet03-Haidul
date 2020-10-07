using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventMaker.BLL.Interfaces;
using EventMaker.DAL.Entities;
using EventMaker.Web.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EventMaker.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountManager _accountManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        public AccountController(IAccountManager accountManager , SignInManager<ApplicationUser> signInManager)
        {
            _accountManager = accountManager ?? throw new ArgumentNullException(nameof(accountManager));
            _signInManager = signInManager ?? throw new ArgumentNullException(nameof(signInManager));
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _accountManager.RegisterAsync(model.Email, model.Username, model.Password);
                if (result.Succeeded)
                {
                    // установка куки
                    await _signInManager.SignInAsync(user, false);
                    return RedirectToAction("Index", "Home");
                }
                else
                { 
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            return View(model);
        }
    }
}
