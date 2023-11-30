using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Users.Models;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

namespace Users.Controllers
{

    /// <summary>
    /// Класс авторизации по логину и паролю
    /// </summary>
    [Authorize]
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [AllowAnonymous]
        public IActionResult Login(string returnUrl) {
            ViewBag.returnUrl = returnUrl;

            return View();
        }

        /*
         Как часть процесса аутентификации система Identity добавляет к ответу сооkiенабор,
который браузер затем включает в любые последующие запросы, чтобы идентифицировать
сеанс пользователя и ассоциированную с ним учетную запись. Вы не
обязаны создавать или управлять этим сооkiе-набором напрямую, т.к. он поддерживается
автоматически промежуточным ПО Identlty.
         */
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel loginModel, string returnUrl)
        {
            if (ModelState.IsValid) {
                AppUser user = await _userManager.FindByEmailAsync(loginModel.Email);
                if (user != null) {
                    //аннулирует любой имеющийся у пользователся сеанс
                    await _signInManager.SignOutAsync();
                    //аунтентификация пользователся
                    SignInResult result = await _signInManager.PasswordSignInAsync(user, loginModel.Password, isPersistent:false, lockoutOnFailure:false);
                    if (result.Succeeded) {
                        return Redirect(returnUrl??"/");
                    }
                }
                ModelState.AddModelError(nameof(loginModel.Email), "Invalid user or password");
            }

            return View(loginModel);
        }

        /// <summary>
        /// Выйти из программы
        /// </summary>
        /// <returns></returns>
        [Authorize]
        public async Task<IActionResult> Logout() {
            await _signInManager.SignOutAsync();

            return RedirectToAction("Index", "Home");
        }

        /// <summary>
        /// Если авториз. пользователь кликает по ссылкам, к которым у него нет прав,
        /// происходит редирект AccessDenied
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        public IActionResult AccessDenied() => View();

        [AllowAnonymous]
        public IActionResult GoogleLogin(string returnUrl) {
            string redirectUrl = Url.Action("GoogleResponse", "Account",
                new { ReturnUrl = returnUrl });
            var properties = _signInManager
                .ConfigureExternalAuthenticationProperties("Google", redirectUrl);
            return new ChallengeResult("Google", properties);
        }

        [AllowAnonymous]
        public async Task<IActionResult> GoogleResponse(string returnUrl = "/") {
            ExternalLoginInfo info = await _signInManager.GetExternalLoginInfoAsync();
            if (info == null)
                return RedirectToAction(nameof(Login));

            SignInResult signResult = await _signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, false);
            if (signResult.Succeeded)
            {
                return Redirect(returnUrl);
            }
            else {
                var user = new AppUser {
                    Email = info.Principal.FindFirst(ClaimTypes.Email).Value,
                    UserName = info.Principal.FindFirst(ClaimTypes.Email).Value,
                };
                IdentityResult identiryResult = await _userManager.CreateAsync(user);
                if (identiryResult.Succeeded) {
                    identiryResult = await _userManager.AddLoginAsync(user, info);
                    if (identiryResult.Succeeded) {
                        await _signInManager.SignInAsync(user, false);

                        return Redirect(returnUrl);
                    }
                }

                return AccessDenied();
            }
        }
    }
}
