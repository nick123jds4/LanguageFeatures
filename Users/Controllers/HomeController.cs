using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Users.Models;

namespace Users.Controllers
{ 
    [Authorize]
    public class HomeController : Controller
    {
        private readonly UserManager<AppUser> _userManager; 
        public HomeController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public IActionResult Index() => View(GetData(nameof(Index)));

        //[Authorize(Roles = "Admins, Users")]
        [Authorize(Policy ="DCUsers")]
        public IActionResult OtherAction() => View("Index", GetData(nameof(OtherAction)));

        /// <summary>
        /// Применяется политика, с требованием, которое дает доступ для всех пользователей, кроме Bob
        /// </summary>
        /// <returns></returns>
        [Authorize(Policy = "NotBob")]
        public IActionResult NotBob() => View(nameof(Index), GetData(nameof(NotBob)));

        private Dictionary<string, object> GetData(string actionName) =>
            new Dictionary<string, object> {
            ["Action"] =actionName,
            ["User"] = HttpContext.User.Identity.Name,
            ["Authenticated"] = HttpContext.User.Identity.IsAuthenticated,
            ["Auth Type"] = HttpContext.User.Identity.AuthenticationType,
            ["In Users Role"] = HttpContext.User.IsInRole("Users"),
            ["City"] = CurrentUser.Result.City,
            ["Qualification"] = CurrentUser.Result.Qualifications,
            };

        private Task<AppUser> CurrentUser =>
            _userManager.FindByNameAsync(HttpContext.User.Identity.Name);
         
        public async Task<IActionResult> UserProps() {
            return View(await CurrentUser);
        }

        [HttpPost]
        public async Task<IActionResult> UserProps(
            [Required]Cities city, 
            [Required]QualificationLevels qualifications) {
            if (ModelState.IsValid) {
                var user = await CurrentUser;
                user.City = city;
                user.Qualifications = qualifications;
                await _userManager.UpdateAsync(user);

                return RedirectToAction(nameof(Index));
            }

            return View(await CurrentUser);
        }
    }
}
