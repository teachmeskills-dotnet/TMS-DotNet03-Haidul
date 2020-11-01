using EventMaker.BLL.Interfaces;
using EventMaker.DAL.Entities;
using EventMaker.Web.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NETCore.MailKit.Core;
using System;
using System.Threading.Tasks;

namespace EventMaker.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountManager _accountManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IEmailService _emailService;

        public AccountController(IAccountManager accountManager,
                                 IEmailService emailService,
                                 SignInManager<ApplicationUser> signInManager)
        {
            _accountManager = accountManager ?? throw new ArgumentNullException(nameof(accountManager));
            _emailService = emailService ?? throw new ArgumentNullException(nameof(emailService));
            _signInManager = signInManager ?? throw new ArgumentNullException(nameof(signInManager));
        }

        [HttpGet]
        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(SignUpViewModel model)
        {
            if (ModelState.IsValid)
            {
                (IdentityResult result, ApplicationUser user) result = await _accountManager.SignUpAsync(model.Email, model.Username, model.Password);
                if (result.result.Succeeded)
                {
                    _emailService.Send(model.Email, "EventMaker app", "You registered at EventMakerApp");
                    await _signInManager.SignInAsync(result.user, false);
                    return RedirectToAction("Index", "Home");
                }
                foreach (var error in result.result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult LogIn(string returnUrl = null)
        {
            return View(new LogInViewModel { ReturnUrl = returnUrl });
        }

        [HttpPost]
        public async Task<IActionResult> LogIn(LogInViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.Username, model.Password, model.RememberMe, false);
                if (result.Succeeded)
                {                   
                    if (!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
                    {
                        return Redirect(model.ReturnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Incorrect email or (and) password."); /// TODO : rework this exceptions
                }
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SignOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var userId = await _accountManager.GetUserIdByNameAsync(User.Identity.Name);
                (IdentityResult result, ApplicationUser user) result = await _accountManager.ChangePasswordAsync(userId, model.OldPassword, model.NewPassword);
                if (result.user != null)
                {
                    if (result.result.Succeeded)
                    {
                        await SignOut();
                    }
                    else
                    {
                        foreach (var error in result.result.Errors)
                        {
                            ModelState.AddModelError(string.Empty, error.Description);
                        }
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "User didn't found"); /// TODO : rework this exceptions
                }
            }
            return RedirectToAction("Index", "Home");
        }
    }
}