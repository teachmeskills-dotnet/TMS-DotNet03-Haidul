using System;
using System.Threading.Tasks;
using EventMaker.BLL.Interfaces;
using EventMaker.BLL.Services;
using EventMaker.Common.Exceptions;
using EventMaker.Common.Resources;
using EventMaker.DAL.Entities;
using EventMaker.Web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EventMaker.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountManager _accountManager;
        private readonly IEmailService _emailService;
        private readonly SignInManager<ApplicationUser> _signInManager;

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
            if (model != null)
            {
                if (ModelState.IsValid)
                {
                    (IdentityResult result, ApplicationUser user, string code) result = await _accountManager.SignUpAsync(model.Email, model.Username, model.Password);
                    if (result.result.Succeeded)
                    {
                        var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = result.user.Id, result.code }, protocol: HttpContext.Request.Scheme);
                        await _emailService.SendEmailAsync(model.Email, EmailResource.SubjectConfirm, $"{EmailResource.MessageConfirm}: " +
                                                                                              $"<a href='{callbackUrl}'>link</a>");
                        await _signInManager.SignInAsync(result.user, false);
                        return Content(EmailResource.ReturnContent);
                    }
                    foreach (var error in result.result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
                return View(model);
            }
            else
            {
                throw new OtherException<string>(ExceptionResource.NotCreated);
            }
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ConfirmEmail(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return View(ModelErrorsResource.UserNotFound);
            }
            var result = await _accountManager.ConfirmEmailAsync(userId, code);
            if (result.Succeeded)
                return RedirectToAction("Index", "Home");
            else
                return View(ModelErrorsResource.EmailNotConfirmed);
        }

        [HttpGet]
        public IActionResult LogIn(string returnUrl = null)
        {
            return View(new LogInViewModel { ReturnUrl = returnUrl });
        }

        [HttpPost]
        public async Task<IActionResult> LogIn(LogInViewModel model)
        {
            if (model != null)
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
                        ModelState.AddModelError(string.Empty, ModelErrorsResource.LogInError);
                    }
                }
                return View(model);
            }
            throw new OtherException<string>(ExceptionResource.UserNotFound);
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
            if (model != null)
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
                        ModelState.AddModelError(string.Empty, ModelErrorsResource.UserNotFound);
                    }
                }
                return RedirectToAction("Index", "Home");
            }
            throw new OtherException<string>(ExceptionResource.NotChanged);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult ResetPassword(string code = null)
        {
            return code == null ? View(ModelErrorsResource.OtherError) : View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (model != null)
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }
                var result = await _accountManager.ResetPasswordAsync(model.Email, model.Code, model.Password);
                if (result.Succeeded)
                {
                    return View("ResetPasswordConfirmation");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                    return View(model);
                }
            }
            throw new OtherException<string>(ExceptionResource.NotChanged);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (model != null)
            {
                if (ModelState.IsValid)
                {
                    (ApplicationUser user, string code, bool isExist) = await _accountManager.ForgotPasswordAsync(model.Email); ///TODO : Refactor it;
                    if (!isExist)
                    {
                        return View("ForgotPasswordConfirmation");
                    }
                    var callbackUrl = Url.Action("ResetPassword", "Account", new { userId = user.Id, code }, protocol: HttpContext.Request.Scheme);
                    EmailService emailService = new EmailService();
                    await emailService.SendEmailAsync(model.Email, EmailResource.SubjectReset,
                        $"{EmailResource.MessageReset}: <a href='{callbackUrl}'>link</a>");
                    return View("ForgotPasswordConfirmation");
                }
                return View(model);
            }
            throw new OtherException<string>(ExceptionResource.NotChanged);
        }
    }
}
