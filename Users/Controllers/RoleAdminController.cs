using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Users.Extentions;
using Users.Models;

namespace Users.Controllers
{
    /// <summary>
    /// Класс администрирования ролями
    /// </summary>
    [Authorize(Roles = "Admins")]
    public class RoleAdminController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<AppUser> _userManager;

        public RoleAdminController(RoleManager<IdentityRole> roleManager, UserManager<AppUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            return View(_roleManager.Roles);
        }

        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create([Required] string name)
        {
            if (ModelState.IsValid)
            {
                var result = await _roleManager.CreateAsync(new IdentityRole(name));
                if (result.Succeeded)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddErrorsFromResult(result);
                }
            }

            return View(name);
        }
         
        /// <summary>
        /// Удалить роль из списка Ролей
        /// </summary>
        /// <param name="id">Идентификатор роли</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            if (role != null)
            {
                IdentityResult result = await _roleManager.DeleteAsync(role);
                if (result.Succeeded)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddErrorsFromResult(result);
                }
            }
            else
            {
                ModelState.AddModelError("", "No role found");
            }

            return View(nameof(Index), _roleManager.Roles); 
        }

        /// <summary>
        /// Отобразить страницу выбранной роли и всех пользователей, которые состоят в ней и тех что не состоят.
        /// </summary>
        /// <param name="id">Идентификатор роли</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Edit(string id) {
            var role = await _roleManager.FindByIdAsync(id);
            var members = new List<AppUser>();
            var nonMembers = new List<AppUser>();
            foreach (var user in _userManager.Users)
            {
                var list = await _userManager.IsInRoleAsync(user, role.Name)
                    ?members:nonMembers;
                list.Add(user); 
            }
            var roleEdit = new RoleEditModel {
                Role = role,
                Members = members,
                NonMembers = nonMembers
            };

            return View(roleEdit);
        }

        /// <summary>
        /// Работа с ролями осуществляется через их имена, а не их идентификаторы
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Edit(RoleModificationModel model) {
            IdentityResult result;
            if (ModelState.IsValid) {
                foreach (var userId in model.IdsToAdd??new string[] { })
                {
                    var user = await _userManager.FindByIdAsync(userId);
                    if (user != null) {
                        result = await _userManager.AddToRoleAsync(user, model.RoleName);
                        if (!result.Succeeded) {
                            ModelState.AddErrorsFromResult(result);
                        }
                    } 
                }

                foreach (var userId in model.IdsToDelete?? new string[] { })
                {
                    var user = await _userManager.FindByIdAsync(userId);
                    if (user != null) {
                        result = await _userManager.RemoveFromRoleAsync(user, model.RoleName);
                        if (!result.Succeeded)
                            ModelState.AddErrorsFromResult(result);
                    }
                }
            }

            if (ModelState.IsValid)
            {
                return RedirectToAction(nameof(Index));
            }
            else {
                return await Edit(model.RoleId);
            }
        }

    }
}
