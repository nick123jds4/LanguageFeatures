using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Users.Models;

namespace Users.Controllers
{
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
                var user = await _userManager.FindByEmailAsync(loginModel.Email);
                if (user != null) {
                    //аннулирует любой имеющийся у пользователся сеанс
                    await _signInManager.SignOutAsync();
                    //аунтентификация пользователся
                    var result = await _signInManager.PasswordSignInAsync(user, loginModel.Password, isPersistent:false, lockoutOnFailure:false);
                    if (result.Succeeded) {
                        return Redirect(returnUrl??"/");
                    }
                }
                ModelState.AddModelError(nameof(loginModel.Email), "Invalid user or password");
            }

            return View(loginModel);
        }
    }
}
